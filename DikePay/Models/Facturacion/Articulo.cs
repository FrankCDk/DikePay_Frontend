namespace DikePay.Models.Facturacion
{
    public class Articulo
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        // Este string es el que "dibujaremos" como barras
        public string CodigoSku { get; set; } = string.Empty;
        public int Stock { get; set; }
    }
}
