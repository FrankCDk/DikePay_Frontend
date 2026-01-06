namespace DikePay.Application.Interfaces.Repositories
{
    public interface IAlmacenamientoRepository
    {
        Task<Dictionary<string, int>> ObtenerConteoPorTiposAsync();
    }
}
