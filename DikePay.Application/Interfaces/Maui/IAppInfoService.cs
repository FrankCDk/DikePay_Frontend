namespace DikePay.Application.Interfaces.Maui
{
    public interface IAppInfoService
    {
        string GetAppVersion();
        string GetPlatform();
        int GetBuildNumber();
    }
}
