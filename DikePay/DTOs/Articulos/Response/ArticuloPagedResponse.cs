using DikePay.Entities;

namespace DikePay.DTOs.Articulos.Response
{
    public class ArticuloPagedResponse
    {
        public List<Articulo> Items { get; set; } = new List<Articulo>();
        public int TotalRegistros { get; set; }
        public int PaginaActual { get; set; }
    }
}
