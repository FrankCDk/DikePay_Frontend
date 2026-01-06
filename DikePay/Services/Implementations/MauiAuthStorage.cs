using DikePay.Application.Interfaces;

namespace DikePay.Services.Implementations
{
    public class MauiAuthStorage : IAuthStorage
    {
        public Task SaveValueAsync(string key, string value) => SecureStorage.Default.SetAsync(key, value);

        public async Task<string?> GetValueAsync(string key) => await SecureStorage.Default.GetAsync(key);

        public void RemoveValue(string key) => SecureStorage.Default.Remove(key);
    }
}
