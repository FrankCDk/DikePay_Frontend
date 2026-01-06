using DikePay.Entities;

namespace DikePay.Repositories.Interfaces
{
    public interface IArticuloRepository
    {
        Task<List<Articulo>> GetAllAsync();
        Task<int> SaveAllAsync(IEnumerable<Articulo> articulos);
        Task<Articulo> GetBySkuAsync(string sku);
        Task<Articulo> GetByCodigoAsync(string codigo);
        Task<int> SaveAsync(Articulo articulo);
        Task<int> UpdateAsync(Articulo articulo);
        Task<bool> ExistsAsync(string codigo);

        // Listado paginado y filtrado
        Task<List<Articulo>> GetPagedAsync(string codigo, string nombre, int skip, int take);
        Task<int> GetCountAsync(string codigo, string nombre);
    }
}
