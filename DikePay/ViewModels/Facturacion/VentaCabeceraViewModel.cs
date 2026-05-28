using System.ComponentModel.DataAnnotations;

namespace DikePay.ViewModels.Facturacion
{
    public class VentaCabeceraViewModel
    {
        [Required(ErrorMessage = "El tipo de documento es obligatorio")]
        public string TipoDocumento { get; set; } = "BV"; // Solo permite BV o FT

        [Required(ErrorMessage = "La serie es obligatoria")]
        public string Serie { get; set; } = ""; // Debemos traerla de una tabla de numeración

        [Required]
        public string Moneda { get; set; } = "MN"; // MN - Moneda nacional o US - Dolares

        public DateTime? FechaEmision { get; set; } = DateTime.Today; // Usamos la fecha actual por defecto

        [Required]
        public string CondicionVenta { get; set; } = ""; // Contado, Crédito, etc. Podría ser un dropdown con opciones predefinidas

        public string Vendedor { get; set; } = ""; // Codigo del vendedor

        [Required(ErrorMessage = "Debe seleccionar un cliente")]
        public string CodigoCliente { get; set; } = "Público General";
        
        public string NombreCliente { get; set; } = "Público General";

        public string DireccionCliente { get; set; } = string.Empty;

        // Totales calculados (pueden ser solo lectura o setters privados si la lógica va aquí)
        public decimal ValVentaBruto { get; set; }
        public decimal DescuentoTotal { get; set; }
        public decimal IgvTotal { get; set; }
        public decimal IcbperTotal { get; set; }
        public decimal TotalVenta { get; set; }
    }
}
