namespace DikePay.Services.Interfaces
{
    public interface ISyncWorkerService
    {
        /// <summary>
        /// Evento que se dispara cuando una factura se sincroniza con éxito.
        /// El string contiene el mensaje que se mostrará en el Snackbar.
        /// </summary>
        event Action<string>? OnSyncCompleted;

        /// <summary>
        /// Inicia el temporizador (Timer) que ejecuta la sincronización periódica.
        /// </summary>
        void Start();

        /// <summary>
        /// Detiene el temporizador de sincronización.
        /// </summary>
        void Stop();

        /// <summary>
        /// Ejecuta una sincronización manual inmediata de todos los registros pendientes.
        /// Útil cuando el usuario presiona un botón de "Sincronizar ahora".
        /// </summary>
        Task SincronizarPendientesAsync();

        /// <summary>
        /// Indica si el servicio está realizando una operación de sincronización en este momento.
        /// </summary>
        bool IsBusy { get; }
    }
}
