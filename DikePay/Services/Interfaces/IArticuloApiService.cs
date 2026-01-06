using DikePay.DTOs.Articulos.Response;

namespace DikePay.Services.Interfaces
{
    public interface IArticuloApiService
    {
        Task<List<ArticuloDto>> GetArticulosFromApiAsync();
    }
}
