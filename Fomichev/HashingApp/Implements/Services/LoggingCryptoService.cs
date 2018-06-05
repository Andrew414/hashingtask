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

        protected override byte[] GetHash(byte[] buffer)
        {
            var sw = new Stopwatch();
            sw.Start();
            var hash = base.GetHash(buffer);
            sw.Stop();
            _hashStat.Add(sw.Elapsed);

            return hash;
        }

        protected override async Task<byte[]> ReadBytesAsync(Stream ifs, int length)
        {
            var sw = new Stopwatch();
            sw.Start();
            var chunk = await base.ReadBytesAsync(ifs, length);
            sw.Stop();
            _diskStat.Add(sw.Elapsed);

            return chunk;
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
