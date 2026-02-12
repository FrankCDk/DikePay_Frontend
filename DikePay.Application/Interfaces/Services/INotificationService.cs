using DikePay.Domain.Entities;

namespace DikePay.Application.Interfaces.Services
{
    public interface INotificationService
    {
        event Action? OnChange;
        IReadOnlyList<Notificacion> Notificaciones { get; }
        int NoLeidas { get; }
        Task Agregar(string titulo, string mensaje, TipoNotificacion tipo);
        Task MarcarComoLeidas();
    }
}
