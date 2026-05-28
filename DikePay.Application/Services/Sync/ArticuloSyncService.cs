using System;
using System.Collections.Generic;
using System.Text;
using DikePay.Application.Interfaces.Repositories;
using DikePay.Application.Interfaces.Sync;

namespace DikePay.Application.Services.Sync
{
    public class ArticuloSyncService : ISincronizable
    {
        private readonly IArticuloRepository _repo;
        private readonly IHttpClientFactory _httpClient;

        public ArticuloSyncService(IArticuloRepository repo, IHttpClientFactory httpClient)
        {
            _repo = repo;
            _httpClient = httpClient;
        }

        public async Task SincronizarAsync()
        {
            //var pendientes = await _repo.ObtenerPendientesAsync();
            //foreach (var item in pendientes)
            //{
            //    // Lógica de subir a API de Artículos
            //    // ...
            //    await _repo.MarcarSincronizadoAsync(item.Id);
            //}
        }
    }
}
