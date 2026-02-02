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


        [Required(ErrorMessage = "El campo Stock es obligatorio.")]
        [Range(0.00, 999999.99, ErrorMessage = "El stock debe ser mayor o igual 0.")]
        [RegularExpression(@"^\d+(\.\d{1,4})?$", ErrorMessage = "El stock debe tener hasta 4 decimales")]
        public decimal Stock { get; set; }


        [Range(0.00, 999999.99, ErrorMessage = "El stock minimo debe ser mayor o igual 0.")]
        [RegularExpression(@"^\d+(\.\d{1,4})?$", ErrorMessage = "El stock minimo debe tener hasta 4 decimales")]
        public decimal StockMinimo { get; set; } = 0m;

    }
}
