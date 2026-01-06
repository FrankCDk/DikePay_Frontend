using SQLite;

namespace DikePay.Domain.Entities
{
    [Table("Documentos")] // Nombre de la tabla en SQLite
    public class Documento
    {
        [PrimaryKey]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Cabecera { get; set; } = string.Empty;
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
        public bool EstaSincronizado { get; set; } = false;
    }
}
