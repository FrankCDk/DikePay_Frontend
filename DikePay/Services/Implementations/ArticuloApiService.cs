using System.Net.Http.Json;
using DikePay.Models.DTOs.Articulos;
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
            return await _httpClient.GetFromJsonAsync<List<ArticuloDto>>("api/articulos")
                   ?? new();
        }
    }
}
