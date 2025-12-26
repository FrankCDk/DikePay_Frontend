using DikePay.Models.Facturacion;
using DikePay.Services.Interfaces;
using SQLite;

namespace DikePay.Services.Implementations
{
    public class DataBaseContext : IDataBaseContext
    {
        private SQLiteAsyncConnection? _connection;
        private static string DbPath => Path.Combine(FileSystem.AppDataDirectory, "DikePayLocal.db3");

        public async Task<SQLiteAsyncConnection> GetConnectionAsync()
        {
            if (_connection != null) return _connection;

            _connection = new SQLiteAsyncConnection(DbPath);
            // Aquí centralizamos la creación de TODAS las tablas de la app
            await _connection.CreateTableAsync<Articulo>();
            // await _connection.CreateTableAsync<Venta>(); // Próximamente

            return _connection;
        }
    }
}
