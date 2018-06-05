using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Crc32;

namespace Lab1
{
    internal class Program
    {
        private static readonly Dictionary<string, HashAlgorithm> Algorithms = new Dictionary<string, HashAlgorithm>
        {
            {"sha1", new SHA1Managed()},
            {"sha256", new SHA256Managed()},
            {"md5", new SHA256Managed()},
            {"crc32", new Crc32Algorithm()}
        };

        private static void Main(string[] args)
        {
            if (args.Length == 0 || args.Length == 1 && args[0] == "help")
            {
                Help();
                return;
            }

            var filePath = args.Length >= 1 ? args[0] : string.Empty;
            var blockSize = 1024;
            if (args.Length >= 2) int.TryParse(args[1], out blockSize);
            var algorithType = args.Length >= 3 ? args[2].ToLower() : "sha1";
            var mode = args.Length >= 4 ? args[3].ToLower() : "sync";

            Console.WriteLine($"Params: File:{filePath} Block:{blockSize} Algorithm: {algorithType} Mode: {mode}");



            if (!Algorithms.ContainsKey(algorithType))
            {
                Console.WriteLine($"Algorithm {algorithType} not found.");
                return;
            }

            if (mode != "sync" && mode != "async")
            {
                Console.WriteLine($"Mode {mode} not found. Mode should be sync or async");
                return;
            }


            Console.Write(" Starting....");
            Console.WriteLine($"{filePath} {blockSize} {algorithType} {mode}");

            HashFile(filePath, algorithType, blockSize, mode == "sync");
        }

        private static void Help()
        {
            Console.WriteLine("Lab for hashing files. \n");
            Console.WriteLine("Parms: ");
            Console.WriteLine(" 1) File path.");
            Console.WriteLine(" 2) Block size - integer form 1024 to 1048576 as 2^n");
            Console.Write(" 3) Algorithms. one of ");
            Algorithms.Keys.ToList().ForEach(item => Console.Write($"{item} "));
            Console.WriteLine();
            Console.WriteLine(" 4) Mode - sync or async");
        }

        private static void HashFile(string inputPath, string algorithType, int blockSize, bool isSync)
        {
            var readKey = new object();
            var writeKey = new object();

            var outputPath = $"{inputPath}.hashes_{algorithType}";
            var algorithm = Algorithms[algorithType];
            var threadsAmount = isSync ? 1 : Environment.ProcessorCount;

            var wrWatch = new Stopwatch();
            var rWatch = new Stopwatch();
            var hWatch = new Stopwatch();

            Console.Write("\r Processing....");

            hWatch.Start();

            using (var ifs = new FileStream(inputPath, FileMode.Open, FileAccess.Read, FileShare.Read, blockSize))
            using (var ofs = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.Write,
                blockSize))
            {
                var blocksAmount = ifs.Length / blockSize + (ifs.Length % blockSize == 0 ? 0 : 1);
                Parallel.For(0, blocksAmount, new ParallelOptions { MaxDegreeOfParallelism = threadsAmount }, i =>
                {
                    var chunk = new byte[blockSize];
                    lock (readKey)
                    {
                        rWatch.Start();
                        ifs.Read(chunk, 0, chunk.Length);
                        rWatch.Stop();
                    }

                    var hash = algorithm.ComputeHash(chunk);
                    var hex = BitConverter.ToString(hash).Replace("-", "");
                    var convertedHash = Encoding.ASCII.GetBytes($"{hex}{Environment.NewLine}");

                    lock (writeKey)
                    {
                        wrWatch.Start();
                        ofs.WriteAsync(convertedHash, 0, convertedHash.Length);
                        wrWatch.Stop();

                        Console.Write($"\r Processing {i}");
                    }
                });

                ifs.Close();
                ofs.Close();
            }

            hWatch.Stop();

            Console.Write("\r                                               ");
            Console.WriteLine("Finished. \n");

            Console.WriteLine($"Reading: {rWatch.Elapsed:g}");
            Console.WriteLine($"Writing: {wrWatch.Elapsed:g}");
            Console.WriteLine($"Total:   {hWatch.Elapsed:g}");
        }

    }
}
