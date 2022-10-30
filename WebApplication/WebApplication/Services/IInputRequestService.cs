using System.Threading.Tasks;
using WebApplication.Models.RequestBody.InputRequest;

namespace WebApplication.Services
{
    public interface IInputRequestService
    {
        Task<string> CreateInputRequestAsync(CreateRequestInputBody body);
    }
}
