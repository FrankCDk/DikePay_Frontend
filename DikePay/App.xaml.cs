namespace DikePay
{
    public partial class App : Microsoft.Maui.Controls.Application
    {
        public App()
        {
            InitializeComponent();

            // Esto le dice a MAUI: "Usa el motor de navegación Shell"
            MainPage = new AppShell();
        }

        private async void RequestNotificationPermission()
        {
#if ANDROID
            if (OperatingSystem.IsAndroidVersionAtLeast(33))
            {
                await Permissions.RequestAsync<Permissions.PostNotifications>();
            }
#endif
        }
    }
}
