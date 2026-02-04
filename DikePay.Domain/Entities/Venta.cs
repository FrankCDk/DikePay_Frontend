namespace DikePay.Domain.Entities
{
    public class Venta
    {
        public int Id { get; set; } // Local PK
        public string Serie { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public DateTime FechaEmision { get; set; }
        public string TipoComprobante { get; set; } = "03"; // 01 Factura, 03 Boleta
        public string ClienteNombre { get; set; } = "Público General";
        public string ClienteDocumento { get; set; } = "00000000";
        public string Moneda { get; set; } = "PEN";
        public decimal Total { get; set; }
        public List<DetalleVenta> Items { get; set; } = new();

        // Estado de sincronización fiscal
        public bool EnviadoSunat { get; set; } = false;
        public string? MensajeSunat { get; set; }
    }
}
