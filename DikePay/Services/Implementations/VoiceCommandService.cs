using System.Globalization;
using System.Runtime.InteropServices;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Media;

namespace DikePay.Services.Implementations
{
    public class VoiceCommandService
    {
        private readonly ISpeechToText speechToText;
        public string RecognitionText { get; set; } = string.Empty;

        public VoiceCommandService(ISpeechToText _speechToText)
        {
            speechToText = _speechToText;
        }

        //        public async Task StartListening(CancellationToken cancellationToken)
        //        {

        //            try
        //            {
        //                var isGranted = await speechToText.RequestPermissions(cancellationToken);
        //                if (!isGranted)
        //                {
        //                    await Toast.Make("Permiso denegado").Show(CancellationToken.None);
        //                    return;
        //                }
        //            }
        //            catch (Exception ex)
        //            {

        //                throw;
        //            }



        //            speechToText.RecognitionResultUpdated += OnRecognitionTextUpdated;
        //            speechToText.RecognitionResultCompleted += OnRecognitionTextCompleted;


        //            try
        //            {
        //                // Selección inteligente de Cultura según la plataforma
        //                CultureInfo culture;

        //#if WINDOWS
        //                    culture = CultureInfo.GetCultureInfo("es-MX");
        //#elif ANDROID
        //                culture = CultureInfo.CurrentCulture;
        //#else
        //                    culture = CultureInfo.CurrentCulture;
        //#endif

        //                await speechToText.StartListenAsync(new SpeechToTextOptions
        //                {
        //                    Culture = culture
        //                }, cancellationToken);
        //            }
        //            catch (Exception ex)
        //            {
        //                // Manejo de errores específico para Windows (COMException) y general para Android
        //                string mensaje = "Error al iniciar el reconocimiento.";

        //                if (DeviceInfo.Platform == DevicePlatform.WinUI && ex is COMException)
        //                {
        //                    mensaje = "Error de idioma: Instala el reconocimiento de voz mejorado en Windows.";
        //                }
        //                else
        //                {
        //                    mensaje = $"Error: {ex.Message}";
        //                }

        //                await Toast.Make(mensaje).Show(CancellationToken.None);
        //            }


        //        }


        public async Task StartListening(CancellationToken cancellationToken)
        {
            try
            {
                // En Android, el SpeechRecognizer es un componente de UI. 
                // Todo este bloque debe ejecutarse en el Main Thread.
                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    // 1. Petición de permisos
                    var isGranted = await speechToText.RequestPermissions(cancellationToken);
                    if (!isGranted)
                    {
                        await Toast.Make("Permiso denegado").Show(CancellationToken.None);
                        return;
                    }

                    // 2. Suscripción a eventos
                    // (Limpiamos suscripciones previas para evitar duplicados si se llama dos veces)
                    speechToText.RecognitionResultUpdated -= OnRecognitionTextUpdated;
                    speechToText.RecognitionResultCompleted -= OnRecognitionTextCompleted;

                    speechToText.RecognitionResultUpdated += OnRecognitionTextUpdated;
                    speechToText.RecognitionResultCompleted += OnRecognitionTextCompleted;

                    // 3. Selección de cultura e inicio
                    CultureInfo culture;
#if WINDOWS
                culture = CultureInfo.GetCultureInfo("es-MX");
#else
                    culture = CultureInfo.CurrentCulture;
#endif

                    await speechToText.StartListenAsync(new SpeechToTextOptions
                    {
                        Culture = culture
                    }, cancellationToken);
                });
            }
            catch (Exception ex)
            {
                string mensaje = DeviceInfo.Platform == DevicePlatform.WinUI && ex is COMException
                    ? "Error de idioma: Instala el reconocimiento de voz mejorado en Windows."
                    : $"Error: {ex.Message}";

                await Toast.Make(mensaje).Show(CancellationToken.None);
            }
        }


        public async Task StopListening(CancellationToken cancellationToken)
        {
            await speechToText.StopListenAsync(cancellationToken);
            speechToText.RecognitionResultUpdated -= OnRecognitionTextUpdated;
            speechToText.RecognitionResultCompleted -= OnRecognitionTextCompleted;
        }

        private void OnRecognitionTextUpdated(object? sender, SpeechToTextRecognitionResultUpdatedEventArgs args)
        {
            // Aquí acumulamos el texto parcial
            RecognitionText = args.RecognitionResult;
            Console.WriteLine($"Texto parcial: {RecognitionText}");
        }

        private void OnRecognitionTextCompleted(object? sender, SpeechToTextRecognitionResultCompletedEventArgs args)
        {
            // Aquí obtenemos el resultado final
            RecognitionText = args.RecognitionResult.Text;
        }
    }
}