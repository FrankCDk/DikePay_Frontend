using DikePay.Application.Interfaces.Maui;
using DikePay.Application.Interfaces.Services;

namespace DikePay.Application.Services
{
    public class VersionService : IVersionService
    {
        private readonly IVersionApiService _version;
        private readonly IAppInfoService _app;
        private readonly IDialogService _dialogs;

        public VersionService(IVersionApiService versionApiService, IAppInfoService appInfoService, IDialogService dialogs)
        {
            _version = versionApiService;
            _app = appInfoService;
            _dialogs = dialogs;
        }

        public async Task ValidarVersionAsync()
        {
            var buildActual = _app.GetBuildNumber();
            var plataforma = _app.GetPlatform();

            var infoVersion = await _version.GetVersionInfoAsync(plataforma, buildActual);

            if (infoVersion.ActualizacionDisponible)
            {
                if (infoVersion.EsCritica)
                {
                    await _dialogs.AlertaAsync("Actualización Obligatoria", "...", "Ir a la tienda");
                    await _dialogs.AbrirNavegadorAsync(infoVersion.UrlDescarga);
                    return;
                }
                else
                {
                    // ACTUALIZACIÓN OPCIONAL (Solo un aviso)
                    // No bloqueamos el return, para que el usuario pueda loguearse.
                    await _dialogs.AlertaAsync(
                        "Nueva versión disponible",
                        $"Hay una nueva versión ({infoVersion.NumeroVersionReciente}) con mejoras. ¿Deseas actualizar?",
                        "Más tarde");
                }
            }
        }
    }
}
