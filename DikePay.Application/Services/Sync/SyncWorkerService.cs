using DikePay.Application.Interfaces.Sync;

namespace DikePay.Application.Services.Sync
{

    public class SyncWorkerService : ISyncWorkerService, IDisposable
    {
        private readonly IEnumerable<ISincronizable> _serviciosSincronizables;
        private System.Timers.Timer? _timer;
        private bool _isBusy = false;
        public event Action<string>? OnSyncCompleted;

        public bool IsBusy => _isBusy;

        public SyncWorkerService(IEnumerable<ISincronizable> serviciosSincronizables)
        {
            _serviciosSincronizables = serviciosSincronizables;
        }

        public async Task SincronizarPendientesAsync()
        {
            if (_isBusy) return;
            _isBusy = true;

            try
            {
                // Ejecutamos cada servicio de sincronización uno tras otro
                foreach (var servicio in _serviciosSincronizables)
                {
                    await servicio.SincronizarAsync();
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
        
        // Importante para limpiar recursos si la app se cierra o el servicio se destruye
        public void Dispose()
        {
            Stop();
        }
    }

}
