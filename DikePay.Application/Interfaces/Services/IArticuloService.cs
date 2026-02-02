using DikePay.Application.DTOs.Articulos.Request;
using DikePay.Application.DTOs.Articulos.Response;

namespace DikePay.Application.Interfaces.Services
{
    public interface IArticuloService
    {
        Task<bool> CrearArticuloAsync(ArticuloCreateDto articulo);
        Task<bool> ActualizarArticuloAsync(ArticuloCreateDto articulo);
        Task<ArticuloPagedResponse> ListarArticulosAsync(string codigo, string nombre, int pagina, int registrosPorPagina);
    }
}
