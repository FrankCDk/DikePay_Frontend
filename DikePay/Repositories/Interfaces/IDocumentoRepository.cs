using DikePay.Entities;

namespace DikePay.Repositories.Interfaces
{
    public interface IDocumentoRepository
    {
        Task<int> Create(Documento documento);
        Task<int> ObtenerConteoComprobantesAsync();
        Task<List<Documento>> GetPagedAsync(string serie, string correlativo, int skip, int take);
        Task<int> GetCountAsync(string serie, string correlativo);
    }
}
