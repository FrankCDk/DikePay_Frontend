using SQLite;

namespace DikePay.Models.Facturacion
{
    [Table("Articulos")] // Nombre de la tabla en SQLite
    public class Articulo
    {
        [PrimaryKey] // Aquí le decimos a SQLite que 'Codigo' manda
        public string Codigo { get; set; } = string.Empty;

        [Indexed] // Indexamos el SKU para que la búsqueda por código de barras sea instantánea
        public string CodigoSku { get; set; } = string.Empty;

        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Stock { get; set; }

        // --- Campos SUNAT y Operativos ---
        public string CodigoProductoSunat { get; set; } = string.Empty;
        public string Unidad { get; set; } = "NIU"; // Unidades (NIU, KGM, etc)
        public string TipoArticulo { get; set; } = string.Empty;
        public string TipoExistenciaSunat { get; set; } = string.Empty;

        // --- Flags de Control (Booleanos) ---
        public bool AceptaDecimales { get; set; } // Flag decimal
        public bool TieneSerie { get; set; }      // Flag de serie
        public bool TieneLote { get; set; }       // Flag de lote
        public bool ControlaStock { get; set; }   // Flag control de stock
        public bool EsPrecioLibre { get; set; }   // Flag precio libre

        // --- Finanzas ---
        public string MonedaId { get; set; } = "PEN"; // PEN o USD
        public decimal PorcentajeDescuento { get; set; }
        public string TipoAfectacion { get; set; } = "GR"; // GR, INA, EXO
        public string Estado { get; set; } = "V"; // Activo/Inactivo
    }
}
