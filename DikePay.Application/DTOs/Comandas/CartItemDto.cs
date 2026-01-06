namespace DikePay.Application.DTOs.Comandas
{
    public class CartItemDto
    {
        public string ArticuloId { get; set; } = string.Empty;
        public string Nombre { get; set; } = "";
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal => Precio * Cantidad;
        public string Notas { get; set; } = ""; // Ej: "Sin cebolla"
    }
}
