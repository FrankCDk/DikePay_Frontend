namespace DikePay.Application.Interfaces
{
    public interface IAuthStorage
    {
        Task SaveValueAsync(string key, string value);
        Task<string?> GetValueAsync(string key);
        void RemoveValue(string key);
    }
}
