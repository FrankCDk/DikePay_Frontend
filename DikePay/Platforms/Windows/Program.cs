using Microsoft.UI.Dispatching;
using Microsoft.Windows.AppLifecycle;

namespace DikePay.WinUI
{
    public class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            WinRT.ComWrappersSupport.InitializeComWrappers();

            // 1. Rigor de Ingeniería: Single Instance
            var keyInstance = AppInstance.FindOrRegisterForKey("DikePay_Main_Instance");

            if (!keyInstance.IsCurrent)
            {
                // Si ya existe la App, redirigimos el evento de clic a la original
                var activatedArgs = AppInstance.GetCurrent().GetActivatedEventArgs();
                keyInstance.RedirectActivationToAsync(activatedArgs).AsTask().Wait();
                return; // Matamos esta instancia secundaria
            }

            // 2. Instancia Principal
            Microsoft.UI.Xaml.Application.Start((p) =>
            {
                var context = new DispatcherQueueSynchronizationContext(DispatcherQueue.GetForCurrentThread());
                SynchronizationContext.SetSynchronizationContext(context);
                new App();
            });
        }
    }
}