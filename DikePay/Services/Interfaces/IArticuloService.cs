using DikePay.DTOs.Articulos.Response;
using DikePay.Entities;

namespace DikePay.Services.Interfaces
{
    public interface IArticuloService
    {
        Task<bool> CrearArticuloAsync(Articulo articulo);
        Task<bool> ActualizarArticuloAsync(Articulo articulo);
        Task<ArticuloPagedResponse> ListarArticulosAsync(string codigo, string nombre, int pagina, int registrosPorPagina);
    }
}
