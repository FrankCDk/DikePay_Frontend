using DikePay.Application.Interfaces.Services;
using DikePay.Domain.Entities;

namespace DikePay.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly List<Notificacion> _notificaciones = new();
        public event Action? OnChange;

        public IReadOnlyList<Notificacion> Notificaciones => _notificaciones.AsReadOnly();
        public int NoLeidas => _notificaciones.Count(n => !n.Leido);

        public void Agregar(string titulo, string mensaje, TipoNotificacion tipo)
        {
            _notificaciones.Insert(0, new Notificacion
            {
                Titulo = titulo,
                Mensaje = mensaje,
                Tipo = tipo
            });
            OnChange?.Invoke();
        }

        public void MarcarComoLeidas()
        {
            _notificaciones.ForEach(n => n.Leido = true);
            OnChange?.Invoke();
        }

        public void LimpiarTodo()
        {
            _notificaciones.Clear();
            OnChange?.Invoke();
        }
    }
}
