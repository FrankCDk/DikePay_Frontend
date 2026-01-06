using DikePay.Application.Interfaces.Services;
using DikePay.Application.Services;
using Microsoft.JSInterop;

namespace DikePay.Services.Implementations
{
    public class BlazorPdfService : IPdfService
    {
        private readonly IJSRuntime _js;
        public BlazorPdfService(IJSRuntime js) => _js = js;

        public async Task GenerarYImprimirAsync(string serie, decimal total, List<dynamic> productos)
        {
            var html = HtmlGenerator.ObtenerTicketHtml(serie, total, productos);
            var htmlSafe = System.Text.Json.JsonSerializer.Serialize(html);

            // Tu código de JS se queda aquí porque es una implementación técnica de la UI
            await _js.InvokeVoidAsync("eval", $@"
            (function() {{
                var iframe = document.createElement('iframe');
                iframe.style.display = 'none';
                document.body.appendChild(iframe);
                iframe.contentDocument.open();
                iframe.contentDocument.write({htmlSafe});
                iframe.contentDocument.close();
                iframe.contentWindow.focus();
                setTimeout(function() {{
                    iframe.contentWindow.print();
                    setTimeout(function() {{
                        document.body.removeChild(iframe);
                    }}, 1000);
                }}, 500);
            }})();");
        }
    }
}
