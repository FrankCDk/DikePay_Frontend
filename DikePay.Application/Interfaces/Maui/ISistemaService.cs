namespace DikePay.Application.Interfaces.Maui
{
    public interface ISistemaService
    {
        string GetOSVersion();
        string GetProcessorName();
        double GetTotalRAM();
        string GetArchitecture();
        double GetFreeDiskSpaceGB();
        bool IsDotNetInstalled(string version);
        bool IsVb6RuntimeInstalled();
    }
}
