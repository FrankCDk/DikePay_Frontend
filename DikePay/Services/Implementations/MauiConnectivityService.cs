using DikePay.Application.Interfaces;

namespace DikePay.Services.Implementations
{
    public class MauiNetworkService : INetworkService
    {
        public bool HasInternet => Connectivity.Current.NetworkAccess == NetworkAccess.Internet;
    }
}
