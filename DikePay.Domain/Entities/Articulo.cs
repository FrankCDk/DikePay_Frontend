using SQLite;

namespace DikePay.Domain.Entities
{
    [Table("Articulos")] // Nombre de la tabla en SQLite
    public class Articulo
    {
        [PrimaryKey]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Indexed(Name = "Idx_Articulo_Codigo", Unique = true)]
        public string Codigo { get; set; } = string.Empty;

        [Indexed(Name = "Idx_Articulo_Sku")] // Indexamos el SKU para que la búsqueda por código de barras sea instantánea
        public string CodigoSku { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;

        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public decimal StockMinimo { get; set; }

        // --- Campos SUNAT y Operativos ---
        public string CodigoProductoSunat { get; set; } = string.Empty;
        public string Unidad { get; set; } = "UND"; // Unidades (NIU, KGM, etc)
        public string TipoArticulo { get; set; } = string.Empty;
        public string TipoExistenciaSunat { get; set; } = string.Empty;

        // --- Flags de Control (Booleanos) ---
        public bool AceptaDecimales { get; set; } // Flag decimal
        public bool TieneSerie { get; set; }      // Flag de serie
        public bool TieneLote { get; set; }       // Flag de lote
        public bool ControlaStock { get; set; }   // Flag control de stock
        public bool EsPrecioLibre { get; set; }   // Flag precio libre

        // --- Finanzas ---
        public string MonedaId { get; set; } = string.Empty; // PEN o USD
        public decimal PorcentajeDescuento { get; set; }
        public string TipoAfectacion { get; set; } = string.Empty; // GR, INA, EXO
        public string Estado { get; set; } = string.Empty; // Activo/Inactivo


        public DateTime FechaCreacion {  get; set; }
        public string UsuarioCreacion { get; set; } = string.Empty;
        public DateTime FechaActulizacion {  get; set; }
        public string UsuarioActulizacion {  get; set; } = string.Empty;

        public DateTime UltimaActualizacion { get; set; } = DateTime.Now;
        public string EstadoSincronizacion { get; set; } = "P"; // P=Pendiente, S=Sincronizado, E=Error
    }
}
