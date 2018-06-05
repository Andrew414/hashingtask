using System;

namespace HashingFileLab
{
    public class StatisticsResult
    {
        private readonly double _read;
        private readonly double _write;
        private readonly double _hashing;
        private readonly decimal _size;

        public StatisticsResult(decimal size, TimeSpan hashing, TimeSpan write, TimeSpan read)
        {
            _size = size / 1000000;
            _hashing = hashing.TotalMilliseconds / 1000;
            _write = write.TotalMilliseconds / 1000;
            _read = read.TotalMilliseconds / 1000;
        }

        public override string ToString()
            => $"Reading: {_read:F} s\n" +
               $"Writing: {_write:F} s\n" +
               $"Total: {_hashing:F} s\n" +
               $"Reading speed: {_size / (decimal) _read:F} MB/s\n" +
               $"Writing speed: {_size / (decimal) _write:F} MB/s\n";
    }
}
