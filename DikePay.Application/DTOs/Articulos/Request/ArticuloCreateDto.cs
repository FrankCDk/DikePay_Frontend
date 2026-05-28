using System.ComponentModel.DataAnnotations;

namespace DikePay.Application.DTOs.Articulos.Request
{
    public class ArticuloCreateDto
    {

        [Required(ErrorMessage = "El campo Código es obligatorio.")]
        [StringLength(50)]
        public string Codigo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo Código SKU es obligatorio.")]
        [StringLength(50)]
        public string CodigoSku { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [StringLength(200)]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo Precio es obligatorio.")]
        [Range(0.01, 999999.99, ErrorMessage = "El importe debe ser mayor a 0." )]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "El importe debe tener hasta 2 decimales")]
        public decimal Precio { get; set; }


        [Range(0.00, 999999.99, ErrorMessage = "El stock minimo debe ser mayor o igual 0.")]
        [RegularExpression(pattern: @"^\d+(\.\d{1,4})?$", ErrorMessage = "El stock minimo debe tener hasta 4 decimales")]
        public decimal StockMinimo { get; set; } = 0m;

        // --- Configuración SUNAT y Operativa ---
        public string CodigoProductoSunat { get; set; } = string.Empty;
        public string Unidad { get; set; } = "UND";
        public string TipoArticulo { get; set; } = string.Empty;
        public string TipoExistenciaSunat { get; set; } = string.Empty;

        // --- Flags de Control ---
        public bool AceptaDecimales { get; set; }
        public bool TieneSerie { get; set; }
        public bool TieneLote { get; set; }
        public bool ControlaStock { get; set; } = true;
        public bool EsPrecioLibre { get; set; }

        // --- Finanzas ---
        public string MonedaId { get; set; } = "PEN";

        [Range(0.00, 100.00, ErrorMessage = "El descuento debe estar entre 0 y 100.")]
        public decimal PorcentajeDescuento { get; set; } = 0m;

        public string TipoAfectacion { get; set; } = "GR"; // GR, INA, EXO

    }
}
