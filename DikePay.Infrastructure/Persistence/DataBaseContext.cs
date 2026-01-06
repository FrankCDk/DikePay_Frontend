using DikePay.Application.Interfaces;
using DikePay.Application.Interfaces.Repositories; // Para IAlmacenamientoRepository
using DikePay.Domain.Entities;
using SQLite;

namespace DikePay.Infrastructure.Persistence
{
    public class DataBaseContext : IDataBaseContext
    {
        private SQLiteAsyncConnection? _connection;
        private bool _isInitialized = false;
        private readonly SemaphoreSlim _semaphore = new(1, 1);
        private readonly IPathProvider _pathProvider;

        public DataBaseContext(IPathProvider pathProvider)
        {
            _pathProvider = pathProvider;
        }

        public async Task<SQLiteAsyncConnection> GetConnectionAsync()
        {
            await _semaphore.WaitAsync();
            try
            {
                if (_connection == null)
                {
                    //// Obtenemos la ruta desde el repo de almacenamiento
                    //string dbPath = _pathProvider.GetDatabasePath();
                    //_connection = new SQLiteAsyncConnection(dbPath);

                    string dbPath = _pathProvider.GetDatabasePath();

                    // CREAMOS LAS OPCIONES DE CONEXIÓN
                    var options = new SQLiteConnectionString(dbPath,
                        storeDateTimeAsTicks: false // <--- AQUÍ ESTÁ EL TRUCO
                    );

                    _connection = new SQLiteAsyncConnection(options);
                }

                if (!_isInitialized)
                {

                    // --- AQUÍ VA TU LÓGICA DE TABLAS ---
                    // En Desarrollo puedes dejar el Drop, en producción COMENTALO.
                    //await _connection.DropTableAsync<Articulo>();
                    //await _connection.DropTableAsync<Documento>();
                    //await _connection.DropTableAsync<Comanda>();

                    // Lógica de creación de tablas
                    await _connection.CreateTableAsync<Articulo>();
                    await _connection.CreateTableAsync<Documento>();
                    await _connection.CreateTableAsync<Comanda>();
                    _isInitialized = true;
                }
            }
            finally
            {
                _semaphore.Release();
            }

            return _connection;
        }
    }
}