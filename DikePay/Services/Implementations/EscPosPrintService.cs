#if WINDOWS
using DikePay.Application.DTOs.Comandas;
using DikePay.Application.DTOs.Impresion;
using DikePay.Services.Printing;
using System.Text;
#endif

namespace DikePay.Services.Implementations
{
    public class EscPosPrintService
    {
#if WINDOWS
        public void PrintTicket(
            string printerName,
            string serieNumero,
            decimal total,
            decimal descuento,
            string metodoPago,
            List<TicketItemDto> productos)
        {
            var sb = new StringBuilder();

            // Inicializar impresora
            sb.Append("\x1B@");

            // Centrado + Negrita
            sb.Append("\x1B\x61\x01"); // Center
            sb.Append("\x1B\x45\x01"); // Bold ON
            sb.AppendLine("MiRS POS");
            sb.Append("\x1B\x45\x00"); // Bold OFF

            sb.AppendLine("RUC: 20123456789");
            sb.AppendLine("--------------------------------");

            sb.AppendLine("BOLETA DE VENTA");
            sb.AppendLine(serieNumero);
            sb.AppendLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm"));

            sb.AppendLine("--------------------------------");

            // Izquierda
            sb.Append("\x1B\x61\x00");

            foreach (var p in productos)
            {
                string linea =
                    $"{p.Cantidad}x {p.Nombre}".PadRight(20) +
                    $"S/ {(p.Precio * p.Cantidad):N2}".PadLeft(10);

                sb.AppendLine(linea);
            }

            sb.AppendLine("--------------------------------");

            if (descuento > 0)
                sb.AppendLine($"DESCUENTO: -S/ {descuento:N2}");

            sb.Append("\x1B\x45\x01");
            sb.AppendLine($"TOTAL: S/ {total:N2}");
            sb.Append("\x1B\x45\x00");

            sb.AppendLine($"PAGO: {metodoPago}");
            sb.AppendLine("--------------------------------");

            // Gracias
            sb.Append("\x1B\x61\x01");
            sb.AppendLine("GRACIAS POR SU COMPRA");

            // Avance + Corte
            sb.AppendLine("\n\n\n");
            sb.Append("\x1D\x56\x41"); // Corte parcial

            RawPrinterHelper.SendStringToPrinter(printerName, sb.ToString());
        }

        public void PrintComanda(
            string printerName,
            List<CartItemDto> items)
        {
            if (items == null || items.Count == 0)
                return;

            var sb = new StringBuilder();

            // Inicializar impresora
            sb.Append("\x1B@");

            // Título centrado y grande
            sb.Append("\x1B\x61\x01");     // Center
            sb.Append("\x1D\x21\x11");     // Double width & height
            sb.AppendLine("COMANDA");
            sb.Append("\x1D\x21\x00");     // Normal size

            sb.AppendLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            sb.AppendLine("--------------------------------");

            // Alinear izquierda
            sb.Append("\x1B\x61\x00");

            foreach (var item in items)
            {
                // Cantidad grande
                sb.Append("\x1B\x45\x01"); // Bold ON
                sb.AppendLine($"{item.Cantidad}x {item.Nombre}");
                sb.Append("\x1B\x45\x00"); // Bold OFF

                // Notas
                if (!string.IsNullOrWhiteSpace(item.Notas))
                {
                    sb.Append("\x1B\x45\x01");
                    sb.AppendLine($"  * {item.Notas}");
                    sb.Append("\x1B\x45\x00");
                }

                sb.AppendLine();
            }

            sb.AppendLine("--------------------------------");

            // Avance de papel
            sb.AppendLine("\n\n");

            // Corte parcial
            sb.Append("\x1D\x56\x41");

            RawPrinterHelper.SendStringToPrinter(printerName, sb.ToString());
        }
#endif
    }
}