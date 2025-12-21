using ZXing.Net.Maui;

namespace DikePay.Components.Pages;

public partial class ScannerPage : ContentPage
{
    private bool _isHandled;
    public TaskCompletionSource<string> Result { get; } = new();

    public ScannerPage()
    {
        InitializeComponent();
        // ✅ FIX #2 → AQUÍ VA
        CameraView.Options = new BarcodeReaderOptions
        {
            Formats = BarcodeFormat.Ean13 | BarcodeFormat.Code128,
            AutoRotate = true,
            Multiple = false
        };
    }

    private void OnBarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        if (_isHandled)
            return;

        var code = e.Results.FirstOrDefault()?.Value;
        if (string.IsNullOrEmpty(code))
            return;

        _isHandled = true;

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            // 🔴 DETENER LA CÁMARA
            CameraView.IsDetecting = false;
            CameraView.Handler?.DisconnectHandler();

            Result.TrySetResult(code);

            await Navigation.PopModalAsync();
        });
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        // 🔴 BACKUP DE SEGURIDAD
        CameraView.IsDetecting = false;
        CameraView.Handler?.DisconnectHandler();
    }
}
