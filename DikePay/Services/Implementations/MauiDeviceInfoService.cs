using DikePay.Application.Interfaces.Maui;

namespace DikePay.Services.Implementations
{
    public class MauiDeviceInfoService : IDeviceInfoService
    {
        public string GetDeviceName() => DeviceInfo.Current.Name;
    }
}
