using System.Net.Http.Json;
using DikePay.Application.DTOs.Articulos.Response;
using DikePay.Application.Interfaces.Services;

namespace DikePay.Infrastructure.ApiService
{
    public class ArticuloApiService : IArticuloApiService
    {
        private readonly HttpClient _httpClient;

        public ArticuloApiService(IHttpClientFactory httpClientFactory)
        {
            // Obtenemos el cliente configurado por su nombre
            _httpClient = httpClientFactory.CreateClient("DikePayApi");
        }

        public async Task<List<ArticuloDto>> GetArticulosFromApiAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ArticuloDto>>("products")
                   ?? new();
        }
    }
}
