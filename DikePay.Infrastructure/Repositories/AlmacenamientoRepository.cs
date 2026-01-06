using DikePay.Application.Interfaces;
using DikePay.Application.Interfaces.Repositories;
using DikePay.Domain.Entities;

namespace DikePay.Infrastructure.Repositories
{
    public class AlmacenamientoRepository : IAlmacenamientoRepository
    {

        private readonly IDataBaseContext _context;

        public AlmacenamientoRepository(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<string, int>> ObtenerConteoPorTiposAsync()
        {
            var db = await _context.GetConnectionAsync();
            var documentos = await db.Table<Documento>().ToListAsync();

            // Agrupamos por el campo TipoDocumento (asumiendo que tu entidad lo tiene)
            return documentos.GroupBy(d => d.Tipo)
                             .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}
