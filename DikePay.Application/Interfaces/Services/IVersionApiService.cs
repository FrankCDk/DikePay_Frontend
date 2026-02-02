using DikePay.Application.DTOs.Version;

namespace DikePay.Application.Interfaces.Services
{
    public interface IVersionApiService
    {
        Task<VersionResponseDto> GetVersionInfoAsync(string plataforma, int buildActual);
    }
}
