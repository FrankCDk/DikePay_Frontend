using Microsoft.UI.Xaml;
using Microsoft.Windows.AppLifecycle;
using Microsoft.Windows.AppNotifications;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DikePay.WinUI
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : MauiWinUIApplication
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        private static Microsoft.UI.Dispatching.DispatcherQueue? _uiDispatcher;

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            base.OnLaunched(args);

            // Suscribirse a activaciones futuras (cuando la app ya está abierta)
            AppInstance.GetCurrent().Activated += OnAppInstanceActivated;

            // Inicializar notificaciones
            try { AppNotificationManager.Default.Register(); } catch { }

            // Si se abrió por una notificación estando cerrada (Cold Start)
            var activatedArgs = AppInstance.GetCurrent().GetActivatedEventArgs();
            if (activatedArgs.Kind == ExtendedActivationKind.AppNotification)
            {
                ForceWindowToFront();
            }
        }

        private void OnAppInstanceActivated(object sender, Microsoft.Windows.AppLifecycle.AppActivationArguments args)
        {
            // Rigor de Ingeniería: Obtenemos el dispatcher de la ventana activa de WinUI 3
            // Evitamos ambigüedad usando el namespace completo de Microsoft.UI.Xaml.Window
            var mauiWindow = Microsoft.Maui.Controls.Application.Current?.Windows.FirstOrDefault();
            var nativeWindow = mauiWindow?.Handler?.PlatformView as Microsoft.UI.Xaml.Window;

            if (nativeWindow != null)
            {
                nativeWindow.DispatcherQueue.TryEnqueue(() =>
                {
                    ForceWindowToFront();
                });
            }
        }

        private void ForceWindowToFront()
        {
            // Rigor Técnico: User32 Interop para forzar el foco de Windows
            var window = Microsoft.Maui.Controls.Application.Current?.Windows.FirstOrDefault()?.Handler?.PlatformView as Microsoft.UI.Xaml.Window;
            if (window != null)
            {
                window.Activate(); // Activa WinUI 3

                IntPtr hwnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
                ShowWindow(hwnd, SW_RESTORE); // La saca de la barra de tareas
                SetForegroundWindow(hwnd);    // La pone al frente de todo
            }
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        private const int SW_RESTORE = 9;

        ~App() { try { AppNotificationManager.Default.Unregister(); } catch { } }
    }

}
