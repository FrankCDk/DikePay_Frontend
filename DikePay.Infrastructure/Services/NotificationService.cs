using DikePay.Application.Interfaces.Maui;
using DikePay.Application.Interfaces.Services;
using DikePay.Domain.Entities;

namespace DikePay.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {

        private readonly ISystemNotificationSender _sender;
        private readonly IBadgeUpdater _badge;

        private readonly List<Notificacion> _notificaciones = new();
        public event Action? OnChange;

        public NotificationService(ISystemNotificationSender sender, IBadgeUpdater badge)
        {
            _sender = sender;
            _badge = badge;
        }

        public IReadOnlyList<Notificacion> Notificaciones => _notificaciones.AsReadOnly();
        public int NoLeidas => _notificaciones.Count(n => !n.Leido);

        public async Task Agregar(string titulo, string mensaje, TipoNotificacion tipo)
        {
            var notif = new Notificacion
            {
                Titulo = titulo,
                Mensaje = mensaje,
                Tipo = tipo,
                Fecha = DateTime.UtcNow
            };

            _notificaciones.Insert(0, notif);

            await _sender.SendAsync(notif);
            await _badge.SetBadgeAsync(NoLeidas);

            OnChange?.Invoke();
        }

        public async Task MarcarComoLeidas()
        {
            _notificaciones.ForEach(n => n.Leido = true);

            await _badge.ClearAsync();

            OnChange?.Invoke();
        }
        
    }
}
