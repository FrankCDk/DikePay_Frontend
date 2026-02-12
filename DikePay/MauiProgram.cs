using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Media;
using DikePay.Application.Extensions;
using DikePay.Application.Interfaces.Maui;
using DikePay.Extensions;
using DikePay.Helpers;
using DikePay.Infrastructure.Extensions;
using DikePay.Infrastructure.Notifications;
using DikePay.Services.Implementations;
using Microsoft.Extensions.Logging;
using MudBlazor;
using MudBlazor.Services;
using Plugin.LocalNotification;
using Plugin.LocalNotification.AndroidOption;
using SkiaSharp.Views.Maui.Controls.Hosting;
using ZXing.Net.Maui.Controls;

namespace DikePay
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder()
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseSkiaSharp()
                .UseBarcodeReader()                
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });


#if ANDROID || IOS
            builder.UseLocalNotification(config =>
            {
#if ANDROID
                config.AddAndroid(android =>
                {
                    android.AddChannel(new NotificationChannelRequest
                    {
                        Id = "default_channel",
                        Name = "General Notifications",
                        Description = "Default channel",
                        Importance = AndroidImportance.Max
                    });
                });
#endif
            });
#endif

            //MudBlazor
            builder.Services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopRight;
                config.SnackbarConfiguration.PreventDuplicates = true;
                config.SnackbarConfiguration.NewestOnTop = false;
                config.SnackbarConfiguration.ShowCloseIcon = true;
                config.SnackbarConfiguration.VisibleStateDuration = 2000;
                // Configuramos la velocidad de la animación de entrada/salida
                config.SnackbarConfiguration.ShowTransitionDuration = 200; // Más rápido (ms)
                config.SnackbarConfiguration.HideTransitionDuration = 200; // Más rápido (ms)
            });

            builder.Services.AddSingleton<VoiceCommandService>();
            builder.Services.AddSingleton<ISpeechToText>(SpeechToText.Default);
            builder.Services.AddSingleton<BarcodeScannerService>();
            builder.Services.AddSingleton<EscPosPrintService>();
            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddApiServices();
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure();

#if ANDROID
            builder.Services.AddSingleton<ISystemNotificationSender, LocalNotificationSender>();
#elif WINDOWS
    // Apuntamos a la implementación que acabas de crear en la carpeta Platforms
    builder.Services.AddSingleton<ISystemNotificationSender, Platforms.Windows.Notifications.WindowsNotificationSender>();
#endif




#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
