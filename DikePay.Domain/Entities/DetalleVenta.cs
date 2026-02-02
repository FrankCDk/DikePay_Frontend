namespace DikePay.Domain.Entities
{
    public class DetalleVenta
    {
        // El producto asociado a esta línea de venta
        public Articulo Producto { get; set; } = null!;
        public int Cantidad { get; set; } = 1;
        public decimal PrecioUnitario { get; set; }
        public decimal Descuento { get; set; }
        public decimal Subtotal => PrecioUnitario * Cantidad;
        public DetalleVenta() { }
        public DetalleVenta(Articulo producto, int cantidad, decimal descuento)
        {
            Producto = producto;
            Cantidad = cantidad;
            PrecioUnitario = producto.Precio;
            Descuento = descuento;
        }
    }
}
