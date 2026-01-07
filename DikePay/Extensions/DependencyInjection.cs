using DikePay.Application.Interfaces;
using DikePay.Application.Interfaces.Maui;
using DikePay.Application.Interfaces.Services;
using DikePay.Services.Implementations;

namespace DikePay.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddScoped<IPdfService, BlazorPdfService>();
            services.AddScoped<IAuthStorage, MauiAuthStorage>();
            services.AddScoped<INetworkService, MauiNetworkService>();
            services.AddScoped<IPathProvider, MauiPathProvider>();
            services.AddScoped<IQrService, QrService>();

            return services;
        }
    }
}
