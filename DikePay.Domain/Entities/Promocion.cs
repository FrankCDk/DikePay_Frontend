using SQLite;

namespace DikePay.Domain.Entities
{
    [Table("promociones")]
    public class Promocion
    {
        [PrimaryKey]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }

        // Tipos: 'CANTIDAD_FIJA', 'PORCENTAJE', 'BONIFICACION'
        [Column("tipo_promocion")]
        public string TipoPromocion { get; set; } = "CANTIDAD_FIJA";

        [Indexed]
        [Column("articulo_id")]
        public string ArticuloId { get; set; } = string.Empty;

        [Column("cantidad_minima")]
        public decimal CantidadMinima { get; set; } = 1;

        [Column("nuevo_precio")]
        public decimal? NuevoPrecio { get; set; }

        [Column("porcentaje_descuento")]
        public decimal? PorcentajeDescuento { get; set; }

        [Column("fecha_inicio")]   // <--- Crucial para el filtro de fechas
        public DateTime FechaInicio { get; set; }

        [Column("fecha_fin")]      // <--- Crucial para el filtro de fechas
        public DateTime FechaFin { get; set; }

        public string Estado { get; set; } = "V"; // V: Vigente, A: Anulado
    }
}
