using DikePay.Application.Interfaces;
using DikePay.Application.Interfaces.Repositories;
using DikePay.Domain.Entities;
using SQLite;

namespace DikePay.Infrastructure.Repositories
{
    public class PromocionesRepository : IPromocionesRepository
    {
        private readonly IDataBaseContext _context;
        public PromocionesRepository(IDataBaseContext dataBaseContext)
        {
            _context = dataBaseContext;
        }

        public async Task<List<Promocion>> GetPromocionesVigentesByArticuloAsync(string articuloId)
        {
            var db = await _context.GetConnectionAsync();
            var hoy = DateTime.Now;

            // Traemos las promociones del artículo que estén vigentes por estado
            var promos = await db.Table<Promocion>()
                                 .Where(p => p.ArticuloId == articuloId && p.Estado == "V")
                                 .ToListAsync();

            // Filtramos fechas en memoria para evitar problemas de formato de fecha en el motor SQLite
            return promos.Where(p => p.FechaInicio <= hoy && p.FechaFin >= hoy).ToList();
        }

        public async Task<int> InsertAsync(Promocion promocion)
        {
            var db = await _context.GetConnectionAsync();
            return await db.InsertAsync(promocion);
        }

        public async Task<int> SaveAllAsync(IEnumerable<Promocion> promociones)
        {
            var db = await _context.GetConnectionAsync();
            int result = 0;
            try
            {
                await db.RunInTransactionAsync((SQLiteConnection conn) =>
                {
                    foreach (var promocion in promociones)
                    {
                        // InsertOrReplace es pesado. Si sabes que son nuevos, usa Insert.
                        result += conn.InsertOrReplace(promocion);
                    }
                });
                return result;
            }
            catch (SQLiteException ex) when (ex.Message.Contains("locked"))
            {
                // Log específico para debugging profesional
                Console.WriteLine("DB bloqueada. Reintentando...");
                throw;
            }
        }
    }
}
