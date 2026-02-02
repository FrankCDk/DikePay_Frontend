namespace DikePay.Application.Interfaces.Services
{
    public interface ISyncService
    {
        Task SincronizarTodoAsync();
        bool EstaSincronizando { get; }
        double Progreso { get; }
        string? UltimoError { get; }
        event Action? OnSyncStarted;
        event Action<string>? OnSyncCompleted;
        event Action<string>? OnSyncError;
    }
}
