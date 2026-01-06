using DikePay.Application.DTOs.Comandas;
using DikePay.Application.Interfaces;
using DikePay.Application.Interfaces.Repositories;
using DikePay.Domain.Entities;
using DikePay.Domain.Enums;

namespace DikePay.Infrastructure.Repositories
{
    public class ComandaRepository : IComandaRepository
    {
        private readonly IDataBaseContext _context;
        public ComandaRepository(IDataBaseContext context) => _context = context;

        public async Task<int> GuardarComandaAsync(TipoServicio servicio, string referencia, List<CartItemDto> items)
        {
            var db = await _context.GetConnectionAsync();
            var comanda = new Comanda
            {
                Servicio = servicio,
                MesaOReferencia = referencia,
                Total = items.Sum(x => x.Subtotal),
                ItemsJson = System.Text.Json.JsonSerializer.Serialize(items)
            };
            return await db.InsertAsync(comanda);
        }

        public async Task<List<Comanda>> ObtenerComandasPendientesAsync()
        {
            var db = await _context.GetConnectionAsync();
            return await db.Table<Comanda>().Where(x => !x.EstaPagada).ToListAsync();
        }
    }
}
