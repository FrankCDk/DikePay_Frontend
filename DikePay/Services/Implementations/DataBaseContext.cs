using DikePay.Entities;
using DikePay.Services.Interfaces;
using SQLite;

namespace DikePay.Services.Implementations
{
    public class DataBaseContext : IDataBaseContext
    {
        private SQLiteAsyncConnection? _connection;
        private bool _isInitialized = false; // Flag de control
        private readonly SemaphoreSlim _semaphore = new(1, 1); // Evita problemas de concurrencia

        private static string DbPath => Path.Combine(FileSystem.AppDataDirectory, "DikePayLocal.db3");

        public async Task<SQLiteAsyncConnection> GetConnectionAsync()
        {
            // 1. Si no hay conexión, la creamos
            if (_connection == null)
            {
                _connection = new SQLiteAsyncConnection(DbPath);
            }

            // 2. Si ya inicializamos las tablas, devolvemos la conexión de inmediato
            if (_isInitialized) return _connection;

            // 3. Bloqueamos para que si dos procesos piden la conexión al mismo tiempo, 
            // solo uno ejecute la creación de tablas.
            await _semaphore.WaitAsync();
            try
            {
                if (!_isInitialized)
                {
                    // --- AQUÍ VA TU LÓGICA DE TABLAS ---
                    // En Desarrollo puedes dejar el Drop, en producción COMENTALO.
                    await _connection.DropTableAsync<Articulo>();
                    await _connection.DropTableAsync<Documento>();

                    await _connection.CreateTableAsync<Articulo>();
                    await _connection.CreateTableAsync<Documento>();
                    // await _connection.CreateTableAsync<Documento>();
                    // await _connection.CreateTableAsync<Log>();

                    _isInitialized = true;
                }
            }
            finally
            {
                _semaphore.Release();
            }

            return _connection;
        }

        private async Task InitTablesAsync()
        {
            // Una sola lista de tareas para que SQLite las procese eficientemente
            //await _connection.CreateTablesAsync<Articulo, Documento, Usuario, Configuracion>();
        }
    }
}
