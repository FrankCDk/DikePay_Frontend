using DikePay.Application.Interfaces.Maui;
using DikePay.Domain.Entities;
using Plugin.LocalNotification;
using Plugin.LocalNotification.AndroidOption;

namespace DikePay.Infrastructure.Notifications
{
    public class LocalNotificationSender : ISystemNotificationSender
    {
        public async Task SendAsync(Notificacion n)
        {
            var request = new NotificationRequest
            {
                NotificationId = Guid.NewGuid().GetHashCode(),
                Title = n.Titulo,
                Description = n.Mensaje,
                ReturningData = "notificaciones_page",
                Android = new AndroidOptions
                {
                    ChannelId = "default_channel", // DEBE coincidir con el configurado en UseLocalNotification()
                }
            };

            await LocalNotificationCenter.Current.Show(request);
        }
    }

}
