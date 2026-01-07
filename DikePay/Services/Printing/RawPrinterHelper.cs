#if WINDOWS
using System.Runtime.InteropServices;
using System.Text;
#endif

namespace DikePay.Services.Printing
{
#if WINDOWS
    public static class RawPrinterHelper
    {
        [DllImport("winspool.Drv", SetLastError = true)]
        private static extern bool OpenPrinter(
            string pPrinterName,
            out IntPtr phPrinter,
            IntPtr pDefault);

        [DllImport("winspool.Drv", SetLastError = true)]
        private static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", SetLastError = true)]
        private static extern bool StartDocPrinter(
            IntPtr hPrinter,
            int level,
            ref DOC_INFO_1 pDocInfo);

        [DllImport("winspool.Drv", SetLastError = true)]
        private static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", SetLastError = true)]
        private static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", SetLastError = true)]
        private static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", SetLastError = true)]
        private static extern bool WritePrinter(
            IntPtr hPrinter,
            byte[] pBytes,
            int dwCount,
            out int dwWritten);

        [StructLayout(LayoutKind.Sequential)]
        private struct DOC_INFO_1
        {
            public string pDocName;
            public string pOutputFile;
            public string pDataType;
        }

        public static void SendStringToPrinter(string printerName, string data)
        {
            IntPtr printerHandle;
            var docInfo = new DOC_INFO_1
            {
                pDocName = "Ticket POS",
                pDataType = "RAW"
            };

            if (!OpenPrinter(printerName, out printerHandle, IntPtr.Zero))
                throw new InvalidOperationException("No se pudo abrir la impresora.");

            try
            {
                if (!StartDocPrinter(printerHandle, 1, ref docInfo))
                    throw new InvalidOperationException("No se pudo iniciar el documento.");

                if (!StartPagePrinter(printerHandle))
                    throw new InvalidOperationException("No se pudo iniciar la página.");

                byte[] bytes = Encoding.ASCII.GetBytes(data);

                WritePrinter(printerHandle, bytes, bytes.Length, out _);

                EndPagePrinter(printerHandle);
                EndDocPrinter(printerHandle);
            }
            finally
            {
                ClosePrinter(printerHandle);
            }
        }
    }
#endif
}