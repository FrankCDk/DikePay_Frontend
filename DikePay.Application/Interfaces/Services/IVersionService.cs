using DikePay.Application.DTOs.Version;

namespace DikePay.Application.Interfaces.Services
{
    public interface IVersionService
    {
        Task ValidarVersionAsync();
    }
}
