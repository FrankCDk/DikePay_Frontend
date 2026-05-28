using System.Net.Http.Json;
using DikePay.Application.DTOs.Api;
using DikePay.Application.DTOs.Articulos.Response;
using DikePay.Application.Interfaces.Api;
using DikePay.Domain.Entities;

namespace DikePay.Infrastructure.ApiService
{
    public class ArticuloApiService : IArticuloApiService
    {
        private readonly HttpClient _httpClient;

        public ArticuloApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("DikePayApi");
        }

        #region Listar articulos

        public async Task<ApiResponse<List<ArticuloDto>>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<ApiResponse<List<ArticuloDto>>>("articulos")
                   ?? ApiResponse<List<ArticuloDto>>.Fail("ERROR", "Error al obtener los artículos");
        }


        #endregion

        #region Crear articulos

        public async Task<ApiResponse<ArticuloDto>> SaveAsync(Articulo articulo)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("articulos", articulo);
                
                if( response.IsSuccessStatusCode)
                {
                    var articuloCreado = await response.Content.ReadFromJsonAsync<ApiResponse<ArticuloDto>>();
                    return ApiResponse<ArticuloDto>.Ok("Artículo creado con éxito", "SUCCESS");
                }

                return ApiResponse<ArticuloDto>.Fail("ERROR", "Error al crear el artículo");

            }
            catch (HttpRequestException ex)
            {

                return ApiResponse<ArticuloDto>.Fail("ERROR", "Error de conexión con la API");
            }
            catch (Exception)
            {

                return ApiResponse<ArticuloDto>.Fail("ERROR", "Error inesperado");
            }

        }

        #endregion

        #region Actualizar articulo

        public async Task<ApiResponse<ArticuloDto>> UpdateAsync(Articulo articulo)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"articulos/{articulo.Codigo}", articulo);

                if (response.IsSuccessStatusCode)
                {
                    var articuloActualizado = await response.Content.ReadFromJsonAsync<ApiResponse<ArticuloDto>>();
                    return ApiResponse<ArticuloDto>.Ok("Artículo actualizado con éxito", "SUCCESS");
                }

                return ApiResponse<ArticuloDto>.Fail("ERROR", "Error al actualizar el artículo");

            }
            catch (HttpRequestException ex)
            {
                return ApiResponse<ArticuloDto>.Fail("ERROR", "Error de conexión con la API");
            }
            catch (Exception)
            {
                return ApiResponse<ArticuloDto>.Fail("ERROR", "Error inesperado");
            }
        }

        #endregion








    }
}
