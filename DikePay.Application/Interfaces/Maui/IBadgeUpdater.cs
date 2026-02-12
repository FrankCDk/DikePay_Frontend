namespace DikePay.Application.Interfaces.Maui
{
    public interface IBadgeUpdater
    {
        Task SetBadgeAsync(int count);
        Task ClearAsync();
    }
}
