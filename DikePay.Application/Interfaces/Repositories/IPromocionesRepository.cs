using DikePay.Domain.Entities;

namespace DikePay.Application.Interfaces.Repositories
{
    public interface IPromocionesRepository
    {
        Task<List<Promocion>> GetPromocionesVigentesByArticuloAsync(string articuloId);
        Task<int> InsertAsync(Promocion promocion);
        Task<int> SaveAllAsync(IEnumerable<Promocion> promociones);
    }
}
