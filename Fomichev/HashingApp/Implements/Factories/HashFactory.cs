using System;
using System.Security.Cryptography;
using Abstracts.Factories;
using Crc32;
using HashAlgorithm = DataStructs.Types.HashAlgorithm;
using HashProvider = System.Security.Cryptography.HashAlgorithm;

namespace Implements.Factories
{
    public class HashFactory : IHashFactory
    {
        public HashProvider GetHashProvider(HashAlgorithm algo)
        {
            switch (algo)
            {
                case HashAlgorithm.Sha1:
                    return new SHA1Managed();
                case HashAlgorithm.Sha256:
                    return new SHA256Managed();
                case HashAlgorithm.Md5:
                    return new MD5CryptoServiceProvider();
                case HashAlgorithm.Crc32:
                    return new Crc32Algorithm();
                default:
                    throw new ArgumentException("Invalid hash algorithm.");
            }
        }
    }
}
