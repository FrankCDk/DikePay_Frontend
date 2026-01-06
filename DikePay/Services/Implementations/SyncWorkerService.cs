using System.Net.Http.Json;
using DikePay.Entities;
using DikePay.Services.Interfaces;

namespace DikePay.Services.Implementations
{

    public class SyncWorkerService : ISyncWorkerService, IDisposable
    {
        private readonly IDataBaseContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private System.Timers.Timer? _timer;
        private bool _isBusy = false;

        public event Action<string>? OnSyncCompleted;

        // Implementación de la propiedad de la interfaz
        public bool IsBusy => _isBusy;

        public SyncWorkerService(IDataBaseContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
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

        public async Task SincronizarPendientesAsync()
        {
            // Lock semántico para evitar ejecuciones concurrentes
            if (_isBusy) return;
            _isBusy = true;

            try
            {
                var db = await _context.GetConnectionAsync();
                var pendientes = await db.Table<Documento>()
                                         .Where(f => !f.EstaSincronizado)
                                         .ToListAsync();

                foreach (var factura in pendientes)
                {
                    // Tip Senior: Cada factura debe ir en su propio try-catch
                    // para que si una falla por datos, las demás sigan procesándose.
                    try
                    {
                        bool exito = await SubirAFacturacionApi(factura);
                        if (exito)
                        {
                            factura.EstaSincronizado = true;
                            await db.UpdateAsync(factura);
                            OnSyncCompleted?.Invoke($"Documento {factura.Serie}-{factura.Numero} sincronizado.");
                        }
                    }
                    catch (Exception ex)
                    {
                        // Loguear el error de esta factura específica
                        Console.WriteLine($"Error sincronizando comprobante {factura.Serie}-{factura.Numero}: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                // Error general (ej. fallo la conexión a SQLite)
                Console.WriteLine($"Error general en SyncWorker: {ex.Message}");
            }
            finally
            {
                _isBusy = false;
            }
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
