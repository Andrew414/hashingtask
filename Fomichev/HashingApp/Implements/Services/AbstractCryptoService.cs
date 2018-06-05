using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Abstracts.Managers;
using Abstracts.Services;
using DataStructs;
using DataStructs.Types;
using FluentValidation;
using Implements.Static;
using ValidationException = DataStructs.Exceptions.ValidationException;

namespace Implements.Services
{
    public class AbstractCryptoService : ICryptoService
    {
        private readonly IValidator<HashOptions> _validator;
        private readonly IHashManager _hashManager;

        public AbstractCryptoService(
            IValidator<HashOptions> validator,
            IHashManager hashManager)
        {
            _validator = validator;
            _hashManager = hashManager;
        }

        public virtual async Task HashFileAsync(HashOptions opts)
        {
            await ThrowIfNotValidAsync(opts);
            var outputPath = BuildOutputPath(opts);
            var tasks = new List<Task>();
            var slimCount = opts.ProgramMode == ProgramMode.Sync ? 1 : Environment.ProcessorCount;

            using (var sempahore = new SemaphoreSlim(slimCount))
            using (var ifs = new FileStream(opts.InputPath, FileMode.Open, FileAccess.Read, FileShare.Read, opts.BlockSize))
            using (var ofs = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.Write, opts.BlockSize))
            {
                for (var offset = 0; offset < ifs.Length; offset += opts.BlockSize)
                {
                    await sempahore.WaitAsync();
                    tasks.Add(Task.Run(async () =>
                    {
                        try
                        {
                            var chunk = await ReadBytesAsync(ifs, opts.BlockSize);
                            var hash = GetHash(chunk);
                            await WriteBytesAsync(ofs, hash);
                        }
                        finally
                        {
                            sempahore.Release();
                        }
                    }));
                }

                await Task.WhenAll(tasks);

                ifs.Close();
                ofs.Close();
            }
        }

        protected virtual byte[] GetHash(byte[] buffer)
        {
            var hash = _hashManager.ComputeHash(buffer);
            var hex = BitConverter.ToString(hash).Replace("-", "");
            var convertedHash = Encoding.ASCII.GetBytes($"{hex}{Environment.NewLine}");

            return convertedHash;
        }

        protected virtual Task WriteBytesAsync(Stream ofs, byte[] buffer) 
            => ofs.WriteAsync(buffer, 0, buffer.Length);

        protected virtual async Task<byte[]> ReadBytesAsync(Stream ifs, int length)
        {
            var chunk = new byte[length];
            await ifs.ReadAsync(chunk, 0, chunk.Length);

            return chunk;
        }


        protected virtual string BuildOutputPath(HashOptions opts)
            => $"{opts.InputPath}.hashes_{opts.HashAlgorithm}";

        public async Task ThrowIfNotValidAsync(HashOptions item)
        {
            var validationContext = await _validator.ValidateAsync(item);
            if (!validationContext.IsValid)
            {
                throw new ValidationException(validationContext.Errors.AsString());
            }
        }
    }
}
