using System.Security.Cryptography;

namespace Abstracts.Factories
{
    public interface IHashFactory
    {
        HashAlgorithm GetHashProvider(DataStructs.Types.HashAlgorithm type);
    }
}
