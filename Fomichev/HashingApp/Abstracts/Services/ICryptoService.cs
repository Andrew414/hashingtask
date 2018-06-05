using System.Threading.Tasks;
using DataStructs;

namespace Abstracts.Services
{
    public interface ICryptoService : IValidationService<HashOptions>
    {
        Task HashFileAsync(HashOptions opts);
    }
}
