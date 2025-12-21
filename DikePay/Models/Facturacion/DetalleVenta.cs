namespace DikePay.Models.Facturacion
{
    public class DetalleVenta
    {
        // El producto asociado a esta línea de venta
        public Articulo Producto { get; set; } = null!;

        // Cantidad vendida (usamos int para abarrotes, pero podría ser decimal si vendes por peso)
        public int Cantidad { get; set; } = 1;

        // El precio al que se vendió en ese momento (Importante: Ver explicación abajo)
        public decimal PrecioUnitario { get; set; }

        // Propiedad calculada: Subtotal del item
        // Aplicamos lógica de solo lectura para asegurar integridad
        public decimal Subtotal => PrecioUnitario * Cantidad;

        // Constructor opcional para facilitar la creación
        public DetalleVenta() { }

        public DetalleVenta(Articulo producto, int cantidad)
        {
            Producto = producto;
            Cantidad = cantidad;
            // Al crear el detalle, "congelamos" el precio actual del producto
            PrecioUnitario = producto.Precio;
        }
    }
}
