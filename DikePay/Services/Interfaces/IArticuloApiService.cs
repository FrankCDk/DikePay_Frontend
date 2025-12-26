using DikePay.Models.DTOs.Articulos;

namespace DikePay.Services.Interfaces
{
    public interface IArticuloApiService
    {
        Task<List<ArticuloDto>> GetArticulosFromApiAsync();
    }
}
