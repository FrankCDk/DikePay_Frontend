using DikePay.Models.Facturacion;

namespace DikePay.Repositories.Interfaces
{
    public interface IArticuloRepository
    {
        Task<List<Articulo>> GetAllAsync();
        Task<int> SaveAllAsync(List<Articulo> articulos);
        Task<Articulo> GetBySkuAsync(string sku);
        Task<Articulo> GetByCodigoAsync(string codigo);
    }
}
