using DikePay.Repositories.Interfaces;
using DikePay.Services.Interfaces;

namespace DikePay.Services.Implementations
{
    public class AlmacenamientoService : IAlmacenamientoService
    {

        private readonly IAlmacenamientoRepository _almacenamientoRepository;
        private readonly IDocumentoRepository _documentoRepository;
        public AlmacenamientoService(IAlmacenamientoRepository almacenamientoRepository, IDocumentoRepository documentoRepository)
        {
            _almacenamientoRepository = almacenamientoRepository;
            _documentoRepository = documentoRepository;
        }

        public async Task<int> ObtenerConteoComprobantesAsync()
        {
            return await _documentoRepository.ObtenerConteoComprobantesAsync();
        }

        public async Task<string> ObtenerTamañoArchivoDb()
        {
            // La ruta donde creaste tu base de datos SQLite
            string path = Path.Combine(FileSystem.AppDataDirectory, "DikePay.db3");
            if (File.Exists(path))
            {
                long bytes = new FileInfo(path).Length;
                double mb = (bytes / 1024f) / 1024f;
                return mb.ToString("N2");
            }

            return "0.00";
        }

    }
}
