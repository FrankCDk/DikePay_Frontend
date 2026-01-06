namespace DikePay.Services.Interfaces
{
    public interface IAlmacenamientoService
    {
        Task<string> ObtenerTamañoArchivoDb();
        Task<int> ObtenerConteoComprobantesAsync();
    }
}
