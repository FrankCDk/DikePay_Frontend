using DikePay.Models.Facturacion;
using DikePay.Services.Interfaces;
using Microsoft.JSInterop;

namespace DikePay.Services.Implementations
{
    public class SistemaImpresionService : IPdfService
    {
        private readonly IJSRuntime _js;
        public SistemaImpresionService(IJSRuntime js) => _js = js;

        public async Task GenerarYCompartirAsync(string serie, decimal total, List<dynamic> productos)
        {
            var html = HtmlGenerator.ObtenerTicketHtml(serie, total, productos);

            // Serializamos el HTML para evitar problemas con comillas y saltos de línea
            var htmlSafe = System.Text.Json.JsonSerializer.Serialize(html);

            await _js.InvokeVoidAsync("eval", $@"
                (function() {{
                    // 1. Crear un iframe invisible
                    var iframe = document.createElement('iframe');
                    iframe.style.display = 'none';
                    document.body.appendChild(iframe);

                    // 2. Escribir el contenido en el iframe
                    iframe.contentDocument.open();
                    iframe.contentDocument.write({htmlSafe});
                    iframe.contentDocument.close();

                    // 3. Esperar a que cargue y lanzar la impresión
                    iframe.contentWindow.focus();
                    setTimeout(function() {{
                        iframe.contentWindow.print();
                        // 4. Limpiar: eliminar el iframe después de imprimir
                        setTimeout(function() {{
                            document.body.removeChild(iframe);
                        }}, 1000);
                    }}, 500);
                }})();
            ");
        }
    }
}
