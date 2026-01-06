namespace DikePay.Services.Interfaces
{
    public interface IPdfService
    {
        // Retorna la ruta local del archivo generado
        //Task<string> GenerarTicketPdfAsync(string serie, decimal total, List<dynamic> productos);
        Task GenerarYCompartirAsync(string serie, decimal total, List<dynamic> productos);
    }
}