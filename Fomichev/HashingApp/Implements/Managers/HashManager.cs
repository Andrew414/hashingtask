using Abstracts.Managers;
using DataStructs.Types;
using Implements.Static;

namespace Implements.Managers
{
    public class HashManager : IHashManager
    {
        private readonly HashAlgorithm _algo;

        public HashManager(HashAlgorithm algo) => _algo = algo;

        public byte[] ComputeHash(byte[] buffer)
        {
            using (var provider = _algo.Create())
            {
                return provider.ComputeHash(buffer);
            }
        }
    }
}
