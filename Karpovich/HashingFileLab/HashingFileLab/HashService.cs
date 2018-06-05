using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Crc32;

namespace HashingFileLab
{
    public class HashService
    {
        private readonly object _readKey = new object();
        private readonly object _writeKey = new object();

        private readonly int _blockSize;
        private readonly string _inputPath;
        private readonly string _outputPath;
        private readonly ParallelOptions _options; 
        private long _fileSize;
        private readonly HashAlgorithm _algorithm;


        public HashService(string outputPath, string inputPath, int blockSize, bool async, AlgorithmType algo)
        {
            _outputPath = outputPath;
            _inputPath = inputPath;
            _blockSize = blockSize;
            _options = new ParallelOptions { MaxDegreeOfParallelism = async ? Environment.ProcessorCount : 1};
            _algorithm = GetAlgorithm(algo);
        }

        public StatisticsResult HashFile()
        {
            var wrWatch = new Stopwatch();
            var rWatch = new Stopwatch();
            var hWatch = new Stopwatch();

            hWatch.Start();

            using (var ifs = new FileStream(_inputPath, FileMode.Open, FileAccess.Read, FileShare.Read, _blockSize))
            using (var ofs = new FileStream(_outputPath, FileMode.Create, FileAccess.Write, FileShare.Write,
                _blockSize))
            {
                _fileSize = ifs.Length;
                var blocksAmount = ifs.Length / _blockSize + (ifs.Length % _blockSize == 0 ? 0 : 1);
                Parallel.For(0, blocksAmount, _options, _ =>
                {
                    var chunk = new byte[_blockSize];
                    lock (_readKey)
                    {
                        rWatch.Start();
                        ifs.Read(chunk, 0, chunk.Length);
                        rWatch.Stop();
                    }

                    var hash = _algorithm.ComputeHash(chunk);
                    var hex = BitConverter.ToString(hash).Replace("-", "");
                    var convertedHash = Encoding.ASCII.GetBytes($"{hex}{Environment.NewLine}");

                    lock (_writeKey)
                    {
                        wrWatch.Start();
                        ofs.WriteAsync(convertedHash, 0, convertedHash.Length);
                        wrWatch.Stop();
                    }
                });

                ifs.Close();
                ofs.Close();
            }

            hWatch.Stop();

            return new StatisticsResult(_fileSize, hWatch.Elapsed, wrWatch.Elapsed, rWatch.Elapsed);
        }

        private static HashAlgorithm GetAlgorithm(AlgorithmType algo)
        {
            switch (algo)
            {
                case AlgorithmType.Sha1:
                    return new SHA1Managed();
                case AlgorithmType.Sha256:
                    return new SHA256Managed();
                case AlgorithmType.Md5:
                    return new MD5CryptoServiceProvider();
                case AlgorithmType.Crc32:
                    return new Crc32Algorithm();
                default:
                    throw new ArgumentException("Invalid hash algorithm.");
            }
        }

    }
}
