namespace Abstracts.Services
{
    public interface ILoggingCryptoService : ICryptoService
    {
        (double avgRead, double avgHash, decimal read, decimal hash) GetStatistics(int blockSize);
    }
}
