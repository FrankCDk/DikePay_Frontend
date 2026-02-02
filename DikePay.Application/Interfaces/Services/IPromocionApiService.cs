using DikePay.Application.DTOs.Promocion.Response;

namespace DikePay.Application.Interfaces.Services
{
    public interface IPromocionApiService
    {
        Task<List<PromocionDto>> GetPromocionFromApiAsync();
    }
}
