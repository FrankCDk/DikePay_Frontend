using Microsoft.AspNetCore.Components;

#if ANDROID || IOS
using Plugin.LocalNotification;
#endif

namespace DikePay
{
    public partial class MainPage : ContentPage
    {
        public MainPage(NavigationManager navigationManager)
        {
            InitializeComponent();

#if ANDROID || IOS
    LocalNotificationCenter.Current.NotificationActionTapped += (e) =>
    {
        if (e.IsTapped) 
        {
            // Extraemos la ruta desde ReturningData
            string route = e.Request.ReturningData; // "notificaciones_page" -> "/notificaciones"
            
            MainThread.BeginInvokeOnMainThread(() =>
            {
                // Importante: Si usas rutas de Blazor, asegúrate de que coincida con el @page
                if (route == "notificaciones_page")
                {
                    navigationManager.NavigateTo("notificaciones");
                }
            });
        }
    };
#endif
        }

        protected override async void OnNavigatedTo(NavigatedToEventArgs args)
        {
            base.OnNavigatedTo(args);
#if ANDROID || IOS
            await RequestNotificationPermissions();
#endif
        }

        private async Task RequestNotificationPermissions()
        {
#if ANDROID || IOS
            if (await LocalNotificationCenter.Current.AreNotificationsEnabled() == false)
            {
                await LocalNotificationCenter.Current.RequestNotificationPermission();
            }
#endif
        }
    }
}