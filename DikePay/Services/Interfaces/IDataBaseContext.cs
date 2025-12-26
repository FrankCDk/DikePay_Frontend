using SQLite;

namespace DikePay.Services.Interfaces
{
    public interface IDataBaseContext
    {
        Task<SQLiteAsyncConnection> GetConnectionAsync();
    }
}
