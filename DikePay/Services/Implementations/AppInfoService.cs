using DikePay.Application.Interfaces.Maui;

namespace DikePay.Services.Implementations
{
    public class AppInfoService : IAppInfoService
    {
        public string GetAppVersion()
        {
            return AppInfo.Current.VersionString;
        }

        public string GetPlatform()
        {
            // DevicePlatform.Current devuelve un objeto, usamos .ToString() 
            // o comparamos para normalizar el nombre.
            var platform = DeviceInfo.Current.Platform;

            if (platform == DevicePlatform.Android) return "Android";
            if (platform == DevicePlatform.iOS) return "iOS";
            if (platform == DevicePlatform.WinUI) return "Windows";

            return "Unknown";
        }

        // Tip Pro: Si tu API necesita el BuildNumber (el número entero)
        public int GetBuildNumber()
        {
            if (int.TryParse(AppInfo.Current.BuildString, out int build))
                return build;
            return 0;
        }
    }
}
