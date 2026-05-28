using DikePay.Application.DTOs.Api;
using DikePay.Application.DTOs.Articulos.Response;
using DikePay.Domain.Entities;

namespace DikePay.Application.Interfaces.Repositories
{

    /// <summary>
    /// Interface que contiene los metodos relacionados a la tabla de articulo de la base de datos SQL Lite
    /// </summary>
    public interface IArticuloRepository
    {

        /// <summary>
        /// Registramos los articulo de la API en la base de datos SQL Lite, si el articulo ya existe lo actualizamos, sino lo insertamos
        /// </summary>
        Task<int> SaveAllAsync(IEnumerable<Articulo> articulos);
        Task SaveAsync(Articulo articulo);
        Task<int> UpdateAsync(Articulo articulo);
        Task<List<Articulo>> GetAllAsync();







        Task<Articulo> GetBySkuAsync(string sku);
        Task<Articulo> GetByCodigoAsync(string codigo);
        
        

        // Listado paginado y filtrado
        Task<List<Articulo>> GetPagedAsync(string codigo, string nombre, int skip, int take);
        Task<int> GetCountAsync(string codigo, string nombre);
    }
}
