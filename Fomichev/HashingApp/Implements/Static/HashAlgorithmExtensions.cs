using Abstracts.Factories;
using DataStructs.Types;
using Implements.Factories;
using HashProvider = System.Security.Cryptography.HashAlgorithm;

namespace Implements.Static
{
    public static class HashAlgorithmExtensions
    {
        private static readonly IHashFactory Factory = new HashFactory();

        public static HashProvider Create(this HashAlgorithm algo) => Factory.GetHashProvider(algo);
    }
}
