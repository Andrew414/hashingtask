using DataStructs.Types;

namespace DataStructs
{
    public class HashOptions : BaseOptions
    {
        public string InputPath { get; set; }
        public int BlockSize { get; set; }
        public HashAlgorithm HashAlgorithm { get; set; }
    }
}
