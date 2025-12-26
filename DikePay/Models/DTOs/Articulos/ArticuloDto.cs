namespace DikePay.Models.DTOs.Articulos
{
    public class ArticuloDto
    {
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string CodigoSku { get; set; } = string.Empty;
        public string CodigoProductoSunat { get; set; } = string.Empty;
        public string Estado { get; set; }
    }
}
