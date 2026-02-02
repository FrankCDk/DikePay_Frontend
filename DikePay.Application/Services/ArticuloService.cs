using DikePay.Application.DTOs.Articulos.Request;
using DikePay.Application.DTOs.Articulos.Response;
using DikePay.Application.Interfaces.Repositories;
using DikePay.Application.Interfaces.Services;
using DikePay.Domain.Entities;

namespace DikePay.Application.Services
{
    public class ArticuloService : IArticuloService
    {
        private readonly IArticuloRepository _repository;

        public ArticuloService(IArticuloRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> CrearArticuloAsync(ArticuloCreateDto articulo)
        {
            // 1. Lógica de negocio: No podemos tener códigos duplicados
            var existe = await _repository.ExistsAsync(articulo.Codigo);
            if (existe) throw new Exception("El código del artículo ya existe.");

            // 2. Otras validaciones (ej. stock no negativo)
            if (articulo.Precio < 0) return false;

            Articulo entity = new Articulo
            {
                Codigo = articulo.Codigo,
                CodigoSku = articulo.CodigoSku,
                Nombre = articulo.Nombre,
                Precio = articulo.Precio,
                Stock = articulo.Stock,
                StockMinimo = articulo.StockMinimo,
                Estado = "V",
                UltimaActualizacion = DateTime.UtcNow
            };


            var rows = await _repository.SaveAsync(entity);
            return rows > 0;
        }

        public async Task<bool> ActualizarArticuloAsync(ArticuloCreateDto articulo)
        {
            // Validamos que el artículo exista antes de intentar actualizar
            var existe = await _repository.GetByCodigoAsync(articulo.Codigo);
            if (existe == null) return false;

            Articulo entity = new Articulo
            {
                Codigo = articulo.Codigo,
                CodigoSku = articulo.CodigoSku,
                Nombre = articulo.Nombre,
                Precio = articulo.Precio,
                Stock = articulo.Stock,
                StockMinimo = articulo.StockMinimo,
                Estado = "V",
                UltimaActualizacion = DateTime.UtcNow
            };

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
