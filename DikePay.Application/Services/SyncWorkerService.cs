using System.Net.Http.Json;
using DikePay.Application.Interfaces.Repositories;
using DikePay.Application.Interfaces.Services;
using DikePay.Domain.Entities;

namespace DikePay.Application.Services
{

    public class SyncWorkerService : ISyncWorkerService, IDisposable
    {
        private readonly IDocumentoRepository _documentoRepo;
        private readonly IHttpClientFactory _httpClientFactory;
        private System.Timers.Timer? _timer;
        private bool _isBusy = false;

        public event Action<string>? OnSyncCompleted;
        public bool IsBusy => _isBusy;

        public SyncWorkerService(
            IDocumentoRepository documentoRepo, // Inyectamos repo
            IHttpClientFactory httpClientFactory)
        {
            _documentoRepo = documentoRepo;
            _httpClientFactory = httpClientFactory;
        }

        public async Task SincronizarPendientesAsync()
        {
            if (_isBusy) return;
            _isBusy = true;

            try
            {
                // El servicio no sabe de dónde vienen, solo que son pendientes
                var pendientes = await _documentoRepo.ObtenerPendientesSincronizacionAsync();

                foreach (var factura in pendientes)
                {
                    try
                    {
                        bool exito = await SubirAFacturacionApi(factura);
                        if (exito)
                        {
                            await _documentoRepo.MarcarComoSincronizadoAsync(factura.Id);
                            OnSyncCompleted?.Invoke($"Documento {factura.Serie}-{factura.Numero} sincronizado.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error específico: {ex.Message}");
                    }
                }
            }
            finally
            {
                _isBusy = false;
            }
        }

        public void Start()
        {
            if (_timer != null) return; // Ya está corriendo

            _timer = new System.Timers.Timer(120000); // 2 minutos
            _timer.Elapsed += async (s, e) => await SincronizarPendientesAsync();
            _timer.AutoReset = true;
            _timer.Enabled = true;

            // Tip Senior: No bloqueamos el inicio de la app, lo mandamos al ThreadPool
            Task.Run(async () => await SincronizarPendientesAsync());
        }

        public void Stop()
        {
            _timer?.Stop();
            _timer?.Dispose();
            _timer = null;
        }

        private async Task<bool> SubirAFacturacionApi(Documento factura)
        {
            try
            {
                // Usamos el Factory para obtener un cliente configurado (DikePayApi es el nombre que pusiste en el MauiProgram)
                var client = _httpClientFactory.CreateClient("DikePayApi");

                // Tip Senior: El endpoint debe coincidir con tu API
                var response = await client.PostAsJsonAsync("documentos/sincronizar", factura);

                return response.IsSuccessStatusCode;
            }
            catch
            {
                // Si hay error de red (timeout, sin internet), devolvemos false para reintentar luego
                return false;
            }
        }

        // Importante para limpiar recursos si la app se cierra o el servicio se destruye
        public void Dispose()
        {
            Stop();
        }
    }

}
