using DikePay.Models.Facturacion;
using DikePay.Repositories.Interfaces;
using DikePay.Services.Interfaces;

namespace DikePay.Repositories.Implementations
{
    public class ArticuloRepository : IArticuloRepository
    {
        private readonly IDataBaseContext _context;

        public ArticuloRepository(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<List<Articulo>> GetAllAsync()
        {
            var db = await _context.GetConnectionAsync();
            return await db.Table<Articulo>().ToListAsync();
        }

        public async Task<int> SaveAllAsync(List<Articulo> articulos)
        {
            var db = await _context.GetConnectionAsync();
            return await db.InsertAllAsync(articulos, "OR REPLACE");
        }

        public async Task<Articulo> GetBySkuAsync(string sku)
        {
            var db = await _context.GetConnectionAsync();
            return await db.Table<Articulo>().FirstOrDefaultAsync(a => a.CodigoSku == sku);
        }

        public async Task<Articulo> GetByCodigoAsync(string codigo)
        {
            var db = await _context.GetConnectionAsync();
            return await db.Table<Articulo>().FirstOrDefaultAsync(a => a.Codigo == codigo);
        }
    }
}
