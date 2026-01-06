using DikePay.Application.Interfaces;
using DikePay.Application.Interfaces.Repositories;
using DikePay.Domain.Entities;

namespace DikePay.Infrastructure.Repositories
{
    public class DocumentoRepository : IDocumentoRepository
    {

        private readonly IDataBaseContext _context;
        public DocumentoRepository(IDataBaseContext dataBaseContext)
        {
            _context = dataBaseContext;
        }

        public async Task<int> Create(Documento documento)
        {
            var db = await _context.GetConnectionAsync();
            return await db.InsertAsync(documento);
        }

        public async Task<int> ObtenerConteoComprobantesAsync()
        {
            var db = await _context.GetConnectionAsync();
            return await db.Table<Documento>().CountAsync();
        }



        /// <summary>
        /// Listado de documentos de manera paginada y filtrada
        /// </summary>
        /// <param name="serie"></param>
        /// <param name="correlativo"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public async Task<List<Documento>> GetPagedAsync(string serie, string correlativo, int skip, int take)
        {
            var db = await _context.GetConnectionAsync();
            var query = db.Table<Documento>();

            // Aplicamos filtros si vienen informados
            if (!string.IsNullOrWhiteSpace(serie))
                query = query.Where(a => a.Serie.Contains(serie));

            if (!string.IsNullOrWhiteSpace(correlativo))
                query = query.Where(a => a.Numero.Contains(correlativo));

            // Importante: El ordenamiento ayuda a que la paginación sea consistente
            return await query.Skip(skip).Take(take).ToListAsync();
        }

        /// <summary>
        /// Conteo de documento filtrados
        /// </summary>
        /// <param name="serie"></param>
        /// <param name="correlativo"></param>
        /// <returns></returns>
        public async Task<int> GetCountAsync(string serie, string correlativo)
        {
            var db = await _context.GetConnectionAsync();
            var query = db.Table<Documento>();

            if (!string.IsNullOrWhiteSpace(serie))
                query = query.Where(a => a.Serie.Contains(serie));

            if (!string.IsNullOrWhiteSpace(correlativo))
                query = query.Where(a => a.Numero.Contains(correlativo));

            return await query.CountAsync();
        }

        public Task<List<Documento>> ObtenerPendientesSincronizacionAsync()
        {
            throw new NotImplementedException();
        }

        public Task MarcarComoSincronizadoAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
