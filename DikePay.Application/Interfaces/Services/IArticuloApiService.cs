using DikePay.Application.DTOs.Articulos.Response;

namespace DikePay.Application.Interfaces.Services
{
    public interface IArticuloApiService
    {
        Task<List<ArticuloDto>> GetArticulosFromApiAsync();
    }
}
