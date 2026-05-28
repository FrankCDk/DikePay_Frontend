using DikePay.Application.DTOs.Api;
using DikePay.Application.DTOs.Articulos.Response;
using DikePay.Domain.Entities;

namespace DikePay.Application.Interfaces.Api
{
    public interface IArticuloApiService
    {
        Task<ApiResponse<List<ArticuloDto>>> GetAllAsync(); // Obtenemos los articulos de la API
        Task<ApiResponse<ArticuloDto>> SaveAsync(Articulo articulo); // Registrar articulo en la API
        Task<ApiResponse<ArticuloDto>> UpdateAsync(Articulo articulo); // Actualizar articulo en la API




    }
}
