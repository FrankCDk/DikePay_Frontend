using DikePay.Application.Interfaces;
using DikePay.Application.Interfaces.Repositories;
using DikePay.Application.Interfaces.Services;

namespace DikePay.Application.Services
{
    public class AlmacenamientoService : IAlmacenamientoService
    {

        private readonly IPathProvider _pathProvider;
        private readonly IDocumentoRepository _documentoRepository;
        private readonly IAlmacenamientoRepository _almacenamientoRepository;
        public AlmacenamientoService(IPathProvider pathProvider, IDocumentoRepository documentoRepository, IAlmacenamientoRepository almacenamientoRepository)
        {
            _pathProvider = pathProvider;
            _documentoRepository = documentoRepository;
            _almacenamientoRepository = almacenamientoRepository; 
        }

        public async Task<int> ObtenerConteoComprobantesAsync()
        {
            return await _documentoRepository.ObtenerConteoComprobantesAsync();
        }

        public async Task<Dictionary<string, int>> ObtenerConteoPorTiposAsync()
        {
            return await _almacenamientoRepository.ObtenerConteoPorTiposAsync();
        }

        public async Task<string> ObtenerTamañoArchivoDb()
        {
            // La ruta donde creaste tu base de datos SQLite
            //string path = Path.Combine(FileSystem.AppDataDirectory, "DikePay.db3");
            string path = _pathProvider.GetDatabasePath();

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
