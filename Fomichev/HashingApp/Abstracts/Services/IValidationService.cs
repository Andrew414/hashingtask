using System.Threading.Tasks;
using DataStructs;

namespace Abstracts.Services
{
    public interface IValidationService<in T> where T : BaseModel
    {
        Task ThrowIfNotValidAsync(T item);
    }
}
