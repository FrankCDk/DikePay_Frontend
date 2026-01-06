namespace DikePay.Application.Services
{
    public static class HtmlGenerator
    {
        public static string ObtenerTicketHtml(string serie, decimal total, List<dynamic> productos)
        {
            var filas = string.Join("", productos.Select(p =>
                $"<tr><td>{p.Cantidad} x {p.Nombre}</td><td style='text-align:right'>S/ {p.Precio * p.Cantidad:N2}</td></tr>"));

            return $@"
            <html>
            <style>
                body {{ 
                    background-color: #fffdec; 
                    padding: 20px; 
                    color: #333;
                }}
                table {{ font-size: 12px; }}
            </style>
            <body style='font-family:monospace; width:300px;'>
                <h2 style='text-align:center'>DIKEPAY POS</h2>
                <p style='text-align:center'>Boleta: {serie}<br>Fecha: {DateTime.Now:dd/MM/yyyy}</p>
                <hr>
                <table style='width:100%'>{filas}</table>
                <hr>
                <h3 style='text-align:right'>TOTAL: S/ {total:N2}</h3>
                <p style='text-align:center'>¡Gracias por su compra!</p>
            </body>
            </html>";
        }
    }
}
