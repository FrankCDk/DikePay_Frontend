namespace DikePay.Services.Interfaces
{
    public interface ISyncService
    {
        Task SincronizarTodoAsync();
        bool EstaSincronizando { get; }
        double Progreso { get; }
    }
}
