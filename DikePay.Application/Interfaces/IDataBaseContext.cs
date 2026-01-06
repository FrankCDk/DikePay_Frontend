using SQLite;

namespace DikePay.Application.Interfaces
{
    public interface IDataBaseContext
    {
        Task<SQLiteAsyncConnection> GetConnectionAsync();
    }
}
