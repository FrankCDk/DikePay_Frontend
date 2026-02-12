using DikePay.Application.Interfaces.Maui;

#if ANDROID
using Android.App;
using XamarinShortcutBadger;
#elif IOS
using UIKit;
#elif WINDOWS
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;
#endif

namespace DikePay.Services.Implementations
{
    public class BadgeUpdater : IBadgeUpdater
    {
        public Task SetBadgeAsync(int count)
        {
            try
            {
#if ANDROID
                // IMPORTANTE: Asegúrate de tener instalado el NuGet 'ShortcutBadger' en el proyecto DikePay
                var context = Microsoft.Maui.ApplicationModel.Platform.AppContext;
                // La llamada suele ser ShortcutBadger.ApplyCount
                ShortcutBadger.ApplyCount(context, count);
#elif IOS
                UIApplication.SharedApplication.ApplicationIconBadgeNumber = count;
#elif WINDOWS
                var badgeXml = BadgeUpdateManager.GetTemplateContent(BadgeTemplateType.BadgeNumber);
                var badgeElement = badgeXml.SelectSingleNode("/badge") as XmlElement;
                badgeElement?.SetAttribute("value", count.ToString());
                var badge = new BadgeNotification(badgeXml);
                BadgeUpdateManager.CreateBadgeUpdaterForApplication().Update(badge);
#endif
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Badge Error: {ex.Message}");
            }

            return Task.CompletedTask;
        }

        public Task ClearAsync() => SetBadgeAsync(0);
    }
}