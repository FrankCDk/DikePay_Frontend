namespace DikePay.Application.Interfaces.Services
{
    public interface IPdfService
    {
        // Retorna la ruta local del archivo generado
        //Task<string> GenerarTicketPdfAsync(string serie, decimal total, List<dynamic> productos);
        Task GenerarYImprimirAsync(string serie, decimal total, List<dynamic> productos);
    }
}
