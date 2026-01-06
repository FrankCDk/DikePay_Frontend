using DikePay.Domain.Entities;

namespace DikePay.Application.Interfaces.Repositories
{
    public interface IDocumentoRepository
    {
        Task<int> Create(Documento documento);
        Task<int> ObtenerConteoComprobantesAsync();
        Task<List<Documento>> GetPagedAsync(string serie, string correlativo, int skip, int take);
        Task<int> GetCountAsync(string serie, string correlativo);
        Task<List<Documento>> ObtenerPendientesSincronizacionAsync();
        Task MarcarComoSincronizadoAsync(string id);
    }
}
