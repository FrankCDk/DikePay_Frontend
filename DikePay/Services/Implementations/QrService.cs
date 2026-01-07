using DikePay.Application.Interfaces.Maui;
using Net.Codecrete.QrCodeGenerator;

namespace DikePay.Services.Implementations
{
    public class QrService : IQrService
    {
        public string GenerarQrBase64(string texto)
        {
            var qr = QrCode.EncodeText(texto, QrCode.Ecc.Medium);
            // Generamos un SVG o PNG (SVG es mejor para web/Blazor por resolución)
            string svg = qr.ToSvgString(4);
            return svg; // Retornamos el string del SVG directamente
        }
    }
}
