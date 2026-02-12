using DikePay.Application.Interfaces.Maui;
using DikePay.Domain.Entities;
using Microsoft.Windows.AppNotifications;
using Microsoft.Windows.AppNotifications.Builder;

namespace DikePay.Platforms.Windows.Notifications
{
    public class WindowsNotificationSender : ISystemNotificationSender
    {
        //public async Task SendAsync(Notificacion n)
        //{
        //    // Rigor Técnico: Construcción de Toast mediante el App SDK de Windows
        //    var toast = new AppNotificationBuilder()
        //        .AddText(n.Titulo)
        //        .AddText(n.Mensaje)
        //        //.SetLaunchUri(new Uri("dikepay://notificaciones"))
        //        .BuildNotification();

        //    AppNotificationManager.Default.Show(toast);

        //    await Task.CompletedTask;
        //}

        public async Task SendAsync(Notificacion n)
        {
            // Rigor de Ingeniería: Usamos interpolación segura. 
            // Nota: Si n.Titulo contiene caracteres especiales como '&', el XML fallará.
            string tituloEscaped = System.Security.SecurityElement.Escape(n.Titulo);
            string mensajeEscaped = System.Security.SecurityElement.Escape(n.Mensaje);

            string toastXml = $@"
                <toast launch='dikepay://notificaciones'>
                    <visual>
                        <binding template='ToastGeneric'>
                            <text>{tituloEscaped}</text>
                            <text>{mensajeEscaped}</text>
                        </binding>
                    </visual>
                </toast>";

            // Usamos global:: para evitar que el compilador busque dentro de DikePay.Platforms.Windows
            var xmlDoc = new global::Windows.Data.Xml.Dom.XmlDocument();
            xmlDoc.LoadXml(toastXml);

            var toast = new Microsoft.Windows.AppNotifications.AppNotification(xmlDoc.GetXml());

            // Invocación a través del Singleton del App SDK
            Microsoft.Windows.AppNotifications.AppNotificationManager.Default.Show(toast);

            await Task.CompletedTask;
        }
    }
}