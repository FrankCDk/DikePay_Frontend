using System.Text.Json.Serialization;

namespace DikePay.DTOs.Articulos.Response
{
    public class ArticuloDto
    {

        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; } = string.Empty;

        [JsonPropertyName("code")]
        public string Codigo { get; set; } = string.Empty;

        [JsonPropertyName("sku")]
        public string Sku { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Nombre { get; set; } = string.Empty;

        [JsonPropertyName("price")]
        public decimal Precio { get; set; }

        [JsonPropertyName("stock")]
        public decimal Stock { get; set; }

        [JsonPropertyName("stockMin")]
        public decimal StockMinimo { get; set; }

        [JsonPropertyName("taxProductCode")]
        public string CodigoImpuestoSunat { get; set; } = string.Empty;

        [JsonPropertyName("unit")]
        public string UnidadMedida { get; set; } = string.Empty;

        [JsonPropertyName("productType")]
        public string TipoProducto { get; set; } = string.Empty;

        [JsonPropertyName("taxInventoryType")]
        public string TipoInventarioSunat { get; set; } = string.Empty;

        [JsonPropertyName("allowsDecimals")]
        public bool PermiteDecimales { get; set; }

        [JsonPropertyName("hasSerialNumber")]
        public bool TieneNumeroSerie { get; set; }

        [JsonPropertyName("hasBatchNumber")]
        public bool TieneNumeroLote { get; set; }

        [JsonPropertyName("trackStock")]
        public bool ControlaStock { get; set; }

        [JsonPropertyName("isOpenPrice")]
        public bool EsPrecioAbierto { get; set; }

        [JsonPropertyName("currency")]
        public string Moneda { get; set; } = string.Empty;

        [JsonPropertyName("discountPercentage")]
        public decimal PorcentajeDescuento { get; set; }

        [JsonPropertyName("taxAffectationType")]
        public string TipoAfectacionImpuesto { get; set; } = string.Empty;

        [JsonPropertyName("status")]
        public string Estado { get; set; } = string.Empty;

        [JsonPropertyName("createdAt")]
        public DateTime FechaCreacion { get; set; }

        [JsonPropertyName("userCreatedAt")]
        public string UsuarioCreacion { get; set; } = string.Empty;

        [JsonPropertyName("updatedAt")]
        public DateTime FechaActualizacion { get; set; }

        [JsonPropertyName("userUpdateddAt")]
        public string UsuarioActualizacion { get; set; } = string.Empty;
    }
}
