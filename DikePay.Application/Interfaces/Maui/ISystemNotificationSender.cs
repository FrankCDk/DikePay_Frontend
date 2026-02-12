using DikePay.Domain.Entities;

namespace DikePay.Application.Interfaces.Maui
{
    public interface ISystemNotificationSender
    {
        Task SendAsync(Notificacion notificacion);
    }
}
