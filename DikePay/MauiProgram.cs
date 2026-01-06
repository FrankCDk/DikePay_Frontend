using DikePay.Application.Extensions;
using DikePay.Extensions;
using DikePay.Helpers;
using DikePay.Infrastructure.Extensions;
using Microsoft.Extensions.Logging;
using MudBlazor;
using MudBlazor.Services;
using SkiaSharp.Views.Maui.Controls.Hosting;
using ZXing.Net.Maui.Controls;

namespace DikePay
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseSkiaSharp()
                .UseBarcodeReader()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

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

            builder.Services.AddSingleton<BarcodeScannerService>();
            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddApiServices();
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure();

            //builder.Services.AddSingleton<IDataBaseContext, DataBaseContext>();
            //builder.Services.AddScoped<IArticuloRepository, ArticuloRepository>();
            //builder.Services.AddSingleton<AppState>();


            //// Definimos la configuración central de nuestra API
            //builder.Services.AddHttpClient("DikePayApi", client =>
            //{
            //    client.BaseAddress = new Uri("https://tu-api.com/"); // Cambia esto por tu URL real
            //    client.DefaultRequestHeaders.Add("Accept", "application/json");
            //})
            //.AddTransientHttpErrorPolicy(policy =>
            //    policy.WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(2)));
            //// ^ Polly aplicado a nivel global para este cliente

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
