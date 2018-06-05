namespace Abstracts.Managers
{
    public interface IHashManager
    {
        byte[] ComputeHash(byte[] buffer);
    }
}
