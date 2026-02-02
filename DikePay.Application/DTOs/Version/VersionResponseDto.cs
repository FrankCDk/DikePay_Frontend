using System.Text.Json.Serialization;

namespace DikePay.Application.DTOs.Version
{
    public class VersionResponseDto
    {
        [JsonPropertyName("updateAvailable")]
        public bool ActualizacionDisponible { get; set; }

        [JsonPropertyName("isCritical")]
        public bool EsCritica { get; set; }

        [JsonPropertyName("latestVersionNumber")]
        public string NumeroVersionReciente { get; set; } = string.Empty;

        [JsonPropertyName("latestBuildNumber")]
        public int NumeroCompilacionReciente { get; set; }

        [JsonPropertyName("downloadUrl")]
        public string? UrlDescarga { get; set; }

        [JsonPropertyName("releaseNotes")]
        public List<string> NotasLanzamiento { get; set; } = new();
    }
}
