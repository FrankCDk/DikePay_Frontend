using DikePay.Domain.Entities;

namespace DikePay.Application.DTOs.Articulos.Response
{
    public class ArticuloPagedResponse
    {
        public List<Articulo> Items { get; set; } = new List<Articulo>();
        public int TotalRegistros { get; set; }
        public int PaginaActual { get; set; }
    }
}
