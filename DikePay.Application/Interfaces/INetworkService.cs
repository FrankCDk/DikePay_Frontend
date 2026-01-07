namespace DikePay.Application.Interfaces
{
    public interface INetworkService
    {
        bool HasInternet { get; }
        event Action<bool>? ConnectivityChanged;
    }
}
