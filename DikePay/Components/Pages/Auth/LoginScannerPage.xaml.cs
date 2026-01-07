using ZXing.Net.Maui;
using CommunityToolkit.Mvvm.Messaging; // Importante
using DikePay.Services.Implementations; // Donde creaste la clase del mensaje

namespace DikePay.Components.Pages.Auth;

public partial class LoginScannerPage : ContentPage
{
    public LoginScannerPage()
    {
        InitializeComponent();
        barcodeReader.Options = new BarcodeReaderOptions
        {
            Formats = BarcodeFormat.QrCode,
            AutoRotate = true,
            Multiple = false
        };
    }

    private async void OnBarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        var firstResult = e.Results.FirstOrDefault();
        if (firstResult == null) return;

        // 2. TODO lo que toque la UI o el sistema debe ir en el Dispatcher
        Dispatcher.Dispatch(async () =>
        {
            try
            {
                barcodeReader.IsDetecting = false; // Detener el hardware

                string contenido = firstResult.Value;

                // Enviar el mensaje
                WeakReferenceMessenger.Default.Send(new QrLoginMessage(contenido));

                // Navegar hacia atrás
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                // Si algo falla aquí, al menos sabrás qué es
                Console.WriteLine($"Error interno: {ex.Message}");
            }
        });
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        // Detenemos la cámara antes de salir para liberar recursos
        barcodeReader.IsDetecting = false;
        await Shell.Current.GoToAsync("..");
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        barcodeReader.IsDetecting = true;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        barcodeReader.IsDetecting = false;
    }

    //private async void OnCancelClicked(object sender, EventArgs e) =>
    //    await Shell.Current.GoToAsync(".."); // Usar Shell es más consistente si entraste con Shell
}