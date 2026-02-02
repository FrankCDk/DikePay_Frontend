using System.Net.Http.Json;
using DikePay.Application.DTOs.Promocion.Response;
using DikePay.Application.Interfaces.Services;

namespace DikePay.Infrastructure.ApiService
{
    public class PromocionApiService : IPromocionApiService
    {
        private readonly HttpClient _httpClient;

        public PromocionApiService(IHttpClientFactory httpClientFactory)
        {
            // Obtenemos el cliente configurado por su nombre
            _httpClient = httpClientFactory.CreateClient("DikePayApi");
        }

        public async Task<List<PromocionDto>> GetPromocionFromApiAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<PromocionDto>>("promotions/all")
                   ?? new();
        }
    }
}
