using System.Net.Http.Json;
using DikePay.DTOs.Articulos.Response;
using DikePay.Services.Interfaces;

namespace DikePay.Services.Implementations
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
