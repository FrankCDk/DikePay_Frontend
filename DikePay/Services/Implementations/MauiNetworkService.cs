using DikePay.Application.Interfaces;

namespace DikePay.Services.Implementations
{
    public class MauiNetworkService : INetworkService, IDisposable
    {
        public bool HasInternet => Connectivity.Current.NetworkAccess == NetworkAccess.Internet;

        public event Action<bool>? ConnectivityChanged;

        public MauiNetworkService()
        {
            Connectivity.Current.ConnectivityChanged += OnConnectivityChanged;
        }

        private void OnConnectivityChanged(object? sender, ConnectivityChangedEventArgs e)
        {
            bool isConnected = e.NetworkAccess == NetworkAccess.Internet;
            ConnectivityChanged?.Invoke(isConnected);
        }

        public void Dispose()
        {
            Connectivity.Current.ConnectivityChanged -= OnConnectivityChanged;
        }
    }
}