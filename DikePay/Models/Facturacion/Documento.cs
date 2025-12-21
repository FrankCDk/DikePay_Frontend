namespace DikePay.Models.Facturacion
{
    public class Documento
    {
        public string Agencia { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public string Serie { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string CodigoCliente { get; set; } = string.Empty;
        public string NombreCliente { get; set; } = string.Empty;
        public string Moneda { get; set; } = string.Empty;
        public decimal Importe { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string EstadoDocumento { get; set; } = string.Empty;
        public string EstadoSunat { get; set; } = string.Empty;
    }
}
