using DikePay.Application.Interfaces.Services;
using DikePay.Domain.Entities;

namespace DikePay.Application.Services
{
    public class FacturacionQueueService : IFacturacionQueueService
    {

        public event Action<string, TipoNotificacion>? OnNotification;
        private readonly INotificationService _notifService;

        public FacturacionQueueService(INotificationService notificationService)
        {
            _notifService = notificationService;
        }


        public async Task ProcesarEnvioFiscal(Venta venta)
        {
            try
            {
                // Paso 2: Generar XML
                await Task.Delay(1000);
                OnNotification?.Invoke($"XML de {venta.Serie}-{venta.Numero} generado con exito.", TipoNotificacion.Exito);

                // Paso 3: Envío a SUNAT
                // Aquí usas tu ConnectivityService
                await Task.Delay(2000);


                OnNotification?.Invoke("Venta enviada a OSE", TipoNotificacion.Exito);
                await _notifService.Agregar("Declaración exitosa", $"Venta {venta.Serie}-{venta.Numero} con estado CDRA", TipoNotificacion.Exito);


            }
            catch (Exception ex)
            {
                OnNotification?.Invoke($"Error: {ex.Message}", TipoNotificacion.Error);
                await _notifService.Agregar("Error en Declaración", $"Error OSE en {venta.Serie}-{venta.Numero}: {ex.Message}", TipoNotificacion.Error);
            }
        }
    }
}
