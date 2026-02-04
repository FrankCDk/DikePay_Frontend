using DikePay.Domain.Entities;

namespace DikePay.Application.Interfaces.Services
{
    public interface IFacturacionQueueService
    {
        Task ProcesarEnvioFiscal(Venta venta);
        event Action<string, TipoNotificacion>? OnNotification;
    }
}
