using System.Net.Http.Json;
using DikePay.Application.DTOs.Version;
using DikePay.Application.Interfaces.Maui;
using DikePay.Application.Interfaces.Services;

namespace DikePay.Infrastructure.ApiService
{
    public class VersionApiService : IVersionApiService
    {

        private readonly HttpClient _httpClient;
        private readonly IAppInfoService _app;

        public VersionApiService(IHttpClientFactory httpClientFactory, IAppInfoService appInfoService)
        {
            _httpClient = httpClientFactory.CreateClient("DikePayApi");
            _app = appInfoService;
        }

        public async Task<VersionResponseDto> GetVersionInfoAsync(string plataforma, int buildActual)
        {
            try
            {
                var url = $"versions/latest?platform={plataforma}&currentBuild={buildActual}";
                var response = await _httpClient.GetFromJsonAsync<VersionResponseDto>(url);

                return response ?? new VersionResponseDto { ActualizacionDisponible = false };
            }
            catch (Exception ex)
            {
                return new VersionResponseDto { ActualizacionDisponible = false };
            }
        }
    }
}
