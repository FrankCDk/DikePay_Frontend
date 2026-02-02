namespace DikePay.Application.DTOs.Promocion.Response
{
    public class PromocionDto
    {
        public Guid Id { get; set; }
        public string CodigoPromocion { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public string TipoPromocion { get; set; } = "CANTIDAD_FIJA";
        public Guid ArticuloId { get; set; }
        public decimal CantidadMinima { get; set; }
        public decimal? NuevoPrecio { get; set; }
        public decimal? PorcentajeDescuento { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Estado { get; set; } = "V";
    }
}
