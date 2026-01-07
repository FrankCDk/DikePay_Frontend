namespace DikePay;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // AQUÍ REGISTRAMOS LA RUTA DEL ESCÁNER
        Routing.RegisterRoute("login-scanner", typeof(DikePay.Components.Pages.Auth.LoginScannerPage));
    }
}