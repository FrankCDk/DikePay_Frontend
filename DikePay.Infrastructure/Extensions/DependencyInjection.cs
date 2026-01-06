using DikePay.Application.Interfaces;
using DikePay.Application.Interfaces.Repositories;
using DikePay.Application.Interfaces.Services;
using DikePay.Infrastructure.ApiService;
using DikePay.Infrastructure.Persistence;
using DikePay.Infrastructure.Repositories;
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

            // Registramos todos los servicios de API aquí
            services.AddScoped<IArticuloApiService, ArticuloApiService>();

            #region Configuramos el HttpClient con Polly
            services.AddHttpClient("DikePayApi", client =>
            {
                client.BaseAddress = new Uri("http://172.29.0.1:9000/api/v1/"); // Cambia esto por tu URL real
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            })
            .AddTransientHttpErrorPolicy(policy =>
                policy.WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(2)));
            #endregion

            return services;
        }
    }
}
