namespace DikePay.Application.Interfaces.Services
{
    public interface IAlmacenamientoService
    {
        Task<string> ObtenerTamañoArchivoDb();
        Task<int> ObtenerConteoComprobantesAsync();
        Task<Dictionary<string, int>> ObtenerConteoPorTiposAsync();
    }
}
