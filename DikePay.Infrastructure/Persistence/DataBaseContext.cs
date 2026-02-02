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
                        storeDateTimeAsTicks: false, // <--- AQUÍ ESTÁ EL TRUCO
                        openFlags: SQLiteOpenFlags.ReadWrite |
                           SQLiteOpenFlags.Create |
                           SQLiteOpenFlags.FullMutex
                    );

                    _connection = new SQLiteAsyncConnection(options);

                    // CONFIGURACIONES PRO:
                    // 1. Esperar hasta 2 segundos si la DB está ocupada antes de lanzar excepción
                    await _connection.ExecuteScalarAsync<string>("PRAGMA busy_timeout = 2000;");

                    // 2. Activar modo WAL para permitir lecturas y escrituras simultáneas
                    await _connection.ExecuteScalarAsync<string>("PRAGMA journal_mode = WAL;");
                }

                if (!_isInitialized)
                {

                    // --- AQUÍ VA TU LÓGICA DE TABLAS ---
                    // En Desarrollo puedes dejar el Drop, en producción COMENTALO.
                    //await _connection.DropTableAsync<Articulo>();
                    //await _connection.DropTableAsync<Documento>();
                    //await _connection.DropTableAsync<Comanda>();
                    //await _connection.DropTableAsync<Promocion>();

                    // Lógica de creación de tablas
                    await _connection.CreateTableAsync<Articulo>();
                    await _connection.CreateTableAsync<Documento>();
                    await _connection.CreateTableAsync<Comanda>();
                    await _connection.CreateTableAsync<Promocion>();
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