using DikePay.Application.Interfaces;
using DikePay.Application.Interfaces.Maui;
using DikePay.Application.Interfaces.Repositories;
using DikePay.Application.Interfaces.Services;
using DikePay.Infrastructure.ApiService;
using DikePay.Infrastructure.Notifications;
using DikePay.Infrastructure.Persistence;
using DikePay.Infrastructure.Repositories;
using DikePay.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace DikePay.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {

            services.AddScoped<IDataBaseContext, DataBaseContext>();
            services.AddScoped<IArticuloRepository, ArticuloRepository>();
            services.AddScoped<IAlmacenamientoRepository, AlmacenamientoRepository>();
            services.AddScoped<IDocumentoRepository, DocumentoRepository>();
            services.AddScoped<IPromocionesRepository, PromocionesRepository>();

            services.AddSingleton<INotificationService, NotificationService>();
            services.AddSingleton<IVersionApiService, VersionApiService>();

            // Registramos todos los servicios de API aquí
            services.AddScoped<IArticuloApiService, ArticuloApiService>();
            services.AddScoped<IPromocionApiService, PromocionApiService>();
                     
            #region Configuramos el HttpClient con Polly
            services.AddHttpClient("DikePayApi", client =>
            {
                //client.BaseAddress = new Uri("http://172.29.0.1:9000/api/v1/"); // IpConfig - Laptop Cambia esto por tu URL real
                client.BaseAddress = new Uri("http://192.168.18.40:9000/api/v1/"); //  Moviles
                //client.BaseAddress = new Uri("https://localhost:44361/api/v1/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            })
            .AddTransientHttpErrorPolicy(policy =>
                policy.WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(2)));
            #endregion

            return services;
        }
    }
}
