using DikePay.Repositories.Implementations;
using DikePay.Repositories.Interfaces;
using DikePay.Services.Implementations;
using DikePay.Services.Interfaces;
using DikePay.State;
using Polly;

namespace DikePay.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {

            services.AddScoped<IDataBaseContext, DataBaseContext>();
            services.AddScoped<IPdfService, SistemaImpresionService>();

            services.AddScoped<IArticuloRepository, ArticuloRepository>();
            services.AddScoped<IAlmacenamientoRepository, AlmacenamientoRepository>();
            services.AddScoped<IDocumentoRepository, DocumentoRepository>();

            // Registramos todos los servicios de API aquí
            services.AddScoped<IArticuloApiService, ArticuloApiService>();
            services.AddScoped<ISyncWorkerService, SyncWorkerService>();
            services.AddScoped<IArticuloService, ArticuloService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ISyncService, SyncService>();
            services.AddScoped<IAlmacenamientoService, AlmacenamientoService>();

            #region Configuramos el HttpClient con Polly
            services.AddHttpClient("DikePayApi", client =>
            {
                client.BaseAddress = new Uri("http://172.29.0.1:9000/api/v1/"); // Cambia esto por tu URL real
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            })
            .AddTransientHttpErrorPolicy(policy =>
                policy.WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(2)));
            #endregion

            // Registramos el estado de la aplicación como singleton
            services.AddSingleton<AppState>();

            return services;
        }
    }
}
