using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstracts.Managers;
using Abstracts.Services;
using DataStructs;
using FluentValidation;

namespace Implements.Services
{
    public class LoggingCryptoService : AbstractCryptoService, ILoggingCryptoService
    {
        private readonly IHashManager _hashManager;
        private readonly ICollection<TimeSpan> _hashStat;
        private readonly ICollection<TimeSpan> _diskStat;

        public LoggingCryptoService(
            IValidator<HashOptions> validator,
            IHashManager hashManager) : base(validator, hashManager)
        {
            _hashManager = hashManager;
            _hashStat = new List<TimeSpan>();
            _diskStat = new List<TimeSpan>();
        }

        public override Task HashFileAsync(HashOptions opts)
        {
            _hashStat.Clear();
            _diskStat.Clear();

            return base.HashFileAsync(opts);
        }

        protected override async Task HashBytesAsync(Stream ifs, Stream ofs, int bufferSize)
        {
            var chunk = new byte[bufferSize];
            var sw = new Stopwatch();

            sw.Reset();
            sw.Start();
            await ifs.ReadAsync(chunk, 0, chunk.Length);
            sw.Stop();
            _diskStat.Add(sw.Elapsed);

            sw.Reset();
            sw.Start();
            var hash = _hashManager.ComputeHash(chunk);
            var hex = BitConverter.ToString(hash).Replace("-", "");
            sw.Stop();
            _hashStat.Add(sw.Elapsed);

            var bytes = Encoding.ASCII.GetBytes($"{hex}{Environment.NewLine}");
            await ofs.WriteAsync(bytes, 0, bytes.Length);
        }

        public (double avgRead, double avgHash, decimal read, decimal hash) GetStatistics(int blockSize)
        {
            var read = _diskStat
                           .Select(x => x.Milliseconds)
                           .Sum(x => (decimal) x) / 1000;
            var hash = _hashStat
                           .Select(x => x.Milliseconds)
                           .Sum(x => (decimal) x) / 1000;

            var fileSize = _diskStat.Count * blockSize / Math.Pow(2, 20);
            var avgRead = fileSize / Convert.ToDouble(read);
            var avgHash = fileSize / Convert.ToDouble(hash);

            return (avgRead, avgHash, read, hash);
        }
    }
}
