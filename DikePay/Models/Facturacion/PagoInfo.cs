namespace DikePay.Models.Facturacion
{
    public class PagoInfo
    {
        public string Metodo { get; set; } = "CONTADO";
        public decimal MontoRecibido { get; set; }
        public decimal Descuento { get; set; }
        public decimal Vuelto => MontoRecibido > 0 ? MontoRecibido - (TotalConDescuento) : 0;
        public decimal TotalVenta { get; set; }
        public decimal TotalConDescuento => TotalVenta - Descuento;
    }
}
