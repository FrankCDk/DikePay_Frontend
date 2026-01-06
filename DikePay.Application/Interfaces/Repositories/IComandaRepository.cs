using DikePay.Application.DTOs.Comandas;
using DikePay.Domain.Entities;
using DikePay.Domain.Enums;

namespace DikePay.Application.Interfaces.Repositories
{
    public interface IComandaRepository
    {
        Task<int> GuardarComandaAsync(TipoServicio servicio, string referencia, List<CartItemDto> items);
        Task<List<Comanda>> ObtenerComandasPendientesAsync();
    }
}
