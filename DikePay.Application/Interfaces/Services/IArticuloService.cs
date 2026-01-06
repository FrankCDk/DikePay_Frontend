using DikePay.Application.DTOs.Articulos.Response;
using DikePay.Domain.Entities;

namespace DikePay.Application.Interfaces.Services
{
    public interface IArticuloService
    {
        Task<bool> CrearArticuloAsync(Articulo articulo);
        Task<bool> ActualizarArticuloAsync(Articulo articulo);
        Task<ArticuloPagedResponse> ListarArticulosAsync(string codigo, string nombre, int pagina, int registrosPorPagina);
    }
}
