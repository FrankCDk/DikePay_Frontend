using DikePay.Application.Interfaces;
using DikePay.Application.Interfaces.Repositories;
using DikePay.Domain.Entities;

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

            // Filtramos por ID de artículo, estado vigente y que la fecha actual esté en el rango
            //return await db.Table<Promocion>()
            //    .Where(p => p.ArticuloId == articuloId &&
            //                p.Estado == "V" &&
            //                p.FechaInicio <= hoy &&
            //                p.FechaFin >= hoy)
            //    .ToListAsync();
        }

        public async Task<int> InsertAsync(Promocion promocion)
        {
            var db = await _context.GetConnectionAsync();
            return await db.InsertAsync(promocion);
        }
    }
}
