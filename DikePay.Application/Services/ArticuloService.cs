using DikePay.Application.DTOs.Articulos.Request;
using DikePay.Application.DTOs.Articulos.Response;
using DikePay.Application.Interfaces.Api;
using DikePay.Application.Interfaces.Repositories;
using DikePay.Application.Interfaces.Services;
using DikePay.Domain.Entities;

namespace DikePay.Application.Services
{
    public class ArticuloService : IArticuloService
    {
        private readonly IArticuloRepository _repository;
        private readonly IArticuloApiService _api;

        public ArticuloService(IArticuloRepository repository, IArticuloApiService api)
        {
            _repository = repository;
            _api = api;
        }

        public async Task<bool> CrearArticuloAsync(ArticuloCreateDto dto)
        {
            // 1. Crear la entidad del articulo a partir del DTO
            Articulo entity = new Articulo
            {
                Id = Guid.NewGuid().ToString(),
                Codigo = dto.Codigo,
                CodigoSku = dto.CodigoSku,
                Nombre = dto.Nombre,
                Precio = dto.Precio,
                StockMinimo = dto.StockMinimo,
                MonedaId = dto.MonedaId,
                TipoAfectacion = dto.TipoAfectacion,
                ControlaStock = dto.ControlaStock,
                Estado = "V",
                EstadoSincronizacion = "P", // Marcamos como Pendiente
                FechaCreacion = DateTime.UtcNow,
                UltimaActualizacion = DateTime.UtcNow
            };

            // 2. Validamos si existe el registro en la base de datos local
            var existing = await _repository.GetByCodigoAsync(entity.Codigo);

            if (existing != null)
            {
                return false;
            }

            // 3. Registramos el articulo en BD SQL
            await _repository.SaveAsync(entity);

            // Actualizamos en la base de datos local
            var respinseApi = await _api.SaveAsync(entity);

            // MEJORA: En caso no se registre debemos actualizar en segundo plano
            if (respinseApi.Success)
            {
                entity.EstadoSincronizacion = "S"; // Marcamos como Sincronizado
                await _repository.UpdateAsync(entity);
                return true;
            }

            return true;
        }

        public async Task<bool> ActualizarArticuloAsync(ArticuloCreateDto articulo)
        {

            // 1. Crear la entidad del articulo a partir del DTO
            Articulo entity = new Articulo
            {
                // Mandarle el UUID del articulo
                Codigo = articulo.Codigo,
                CodigoSku = articulo.CodigoSku,
                Nombre = articulo.Nombre,
                Precio = articulo.Precio,
                StockMinimo = articulo.StockMinimo,
                Estado = "V",
                UltimaActualizacion = DateTime.UtcNow
            };

            // 2. Enviamos la nueva entidad a la API para su creación.
            var respinseApi = await _api.UpdateAsync(entity);

            if (!respinseApi.Success)
            {
                return false;
            }

            // 3. Si la respuesta es positiva, actualizamos el artículo en la base de datos local.
            var rows = await _repository.UpdateAsync(entity);
            return rows > 0;
        }

        public async Task<ArticuloPagedResponse> ListarArticulosAsync(string codigo, string nombre, int pagina, int registrosPorPagina)
        {
            // Cálculo de paginación: si es página 1, skip es 0. Si es página 2, skip es registrosPorPagina.
            int skip = (pagina - 1) * registrosPorPagina;

            var items = await _repository.GetPagedAsync(codigo, nombre, skip, registrosPorPagina);
            var total = await _repository.GetCountAsync(codigo, nombre);

            return new ArticuloPagedResponse
            {
                Items = items,
                TotalRegistros = total,
                PaginaActual = pagina
            };
        }
    }
}
