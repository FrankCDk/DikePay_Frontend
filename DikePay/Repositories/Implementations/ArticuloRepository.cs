using DikePay.Entities;
using DikePay.Repositories.Interfaces;
using DikePay.Services.Interfaces;
using SQLite;

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

        public async Task<int> SaveAllAsync(IEnumerable<Articulo> articulos)
        {
            var db = await _context.GetConnectionAsync();
            int result = 0;

            // RunInTransactionAsync espera una Action<SQLiteConnection>
            // NO uses 'async' en el delegado de adentro
            await db.RunInTransactionAsync((SQLiteConnection conn) =>
            {
                // Al ser una conexión síncrona (conn), las operaciones son directas
                foreach (var articulo in articulos)
                {
                    result += conn.InsertOrReplace(articulo);
                }
            });

            return result;
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

        /// <summary>
        /// Inserta un nuevo articulo en la base de datos sql lite
        /// </summary>
        /// <param name="articulo"></param>
        /// <returns></returns>
        public async Task<int> SaveAsync(Articulo articulo)
        {
            var db = await _context.GetConnectionAsync();
            return await db.InsertAsync(articulo);
        }

        public async Task<int> UpdateAsync(Articulo articulo)
        {
            var db = await _context.GetConnectionAsync();
            // UpdateAsync busca el registro por su Primary Key ([PrimaryKey]) y lo actualiza
            return await db.UpdateAsync(articulo);
        }

        public async Task<bool> ExistsAsync(string codigo)
        {
            var db = await _context.GetConnectionAsync();
            var count = await db.Table<Articulo>()
                                .Where(a => a.Codigo == codigo)
                                .CountAsync();
            return count > 0;
        }


        /// <summary>
        /// Listado de articulos de manera paginada y filtrada
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="nombre"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public async Task<List<Articulo>> GetPagedAsync(string codigo, string nombre, int skip, int take)
        {
            var db = await _context.GetConnectionAsync();
            var query = db.Table<Articulo>();

            // Aplicamos filtros si vienen informados
            if (!string.IsNullOrWhiteSpace(codigo))
                query = query.Where(a => a.Codigo.Contains(codigo));

            if (!string.IsNullOrWhiteSpace(nombre))
                query = query.Where(a => a.Nombre.Contains(nombre));

            // Importante: El ordenamiento ayuda a que la paginación sea consistente
            return await query.Skip(skip).Take(take).ToListAsync();
        }

        /// <summary>
        /// Conteo de articulos filtrados
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public async Task<int> GetCountAsync(string codigo, string nombre)
        {
            var db = await _context.GetConnectionAsync();
            var query = db.Table<Articulo>();

            if (!string.IsNullOrWhiteSpace(codigo))
                query = query.Where(a => a.Codigo.Contains(codigo));

            if (!string.IsNullOrWhiteSpace(nombre))
                query = query.Where(a => a.Nombre.Contains(nombre));

            return await query.CountAsync();
        }
    }
}
