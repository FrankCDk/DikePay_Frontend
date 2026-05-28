using System.ComponentModel.DataAnnotations;

namespace DikePay.ViewModels.Facturacion
{
    public class VentaDetalleViewModel
    {
        // --- Referencias para la Base de Datos (Ocultas en la UI, pero necesarias para guardar) ---
        public string ArticuloId { get; set; } = string.Empty;
        public string CodigoArticulo { get; set; } = string.Empty;
        public string TipoAfectacion { get; set; } = "GR"; // Vital para los impuestos (GR, INA, EXO)

        // --- Campos de Interfaz ---
        public string Descripcion { get; set; } = string.Empty;
        public string Unidad { get; set; } = "UND";

        [Range(0.001, double.MaxValue, ErrorMessage = "La cantidad debe ser mayor a cero")]
        public decimal Cantidad { get; set; } = 1;

        [Range(0.01, double.MaxValue, ErrorMessage = "El precio unitario no es válido")]
        public decimal PrecioUnitario { get; set; }

        public decimal PorcentajeDescuento { get; set; }

        // --- Cálculos de solo lectura para la vista ---

        // Muestra el importe descontado en esa línea específica
        public decimal DescuentoImporte => (PrecioUnitario * Cantidad) * (PorcentajeDescuento / 100);

        // Valor antes de impuestos (Asumiendo que el PrecioUnitario incluye IGV, esto varía según tu lógica de negocio)
        public decimal Total => (PrecioUnitario * Cantidad) - DescuentoImporte;
    }
}
