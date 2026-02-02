using System.Runtime.InteropServices;
using DikePay.Application.Interfaces.Maui;
using Hardware.Info;

namespace DikePay.Services.Implementations
{
    public class SistemaService : ISistemaService
    {
        private readonly IHardwareInfo _hardwareInfo;

        public SistemaService()
        {
            _hardwareInfo = new HardwareInfo();
        }

        //public string GetOSVersion()
        //{
        //    // Esto devolverá "Windows 11 Home" o similar
        //    return $"{RuntimeInformation.OSDescription}";
        //}

        public string GetOSVersion()
        {
            var os = Environment.OSVersion;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Traducción de Build a Nombre Comercial
                string name = os.Version.Build >= 22000 ? "Windows 11" : "Windows 10";
                return $"{name} ({os.Version.Build})";
            }
            return RuntimeInformation.OSDescription;
        }

        public string GetProcessorName()
        {
            _hardwareInfo.RefreshCPUList();
            var cpu = _hardwareInfo.CpuList.FirstOrDefault();
            return cpu != null ? cpu.Name.Trim() : "No detectado";
        }

        public double GetTotalRAM()
        {
            _hardwareInfo.RefreshMemoryStatus();
            // Usamos TotalPhysical para obtener los bytes exactos
            // Tu captura muestra 16.0 GB, esto lo calculará igual
            return _hardwareInfo.MemoryStatus.TotalPhysical / 1073741824.0;
        }

        public string GetArchitecture() => RuntimeInformation.OSArchitecture.ToString();

        public double GetFreeDiskSpaceGB()
        {
            var drive = new DriveInfo("C");
            return Math.Round(drive.AvailableFreeSpace / 1073741824.0, 2);
        }

        public bool IsDotNetInstalled(string version)
        {
            // Verificación simple por el runtime actual
            if (version.Contains("8") && Environment.Version.Major >= 8) return true;

            // Verificación en carpeta para versiones específicas
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "dotnet", "shared", "Microsoft.NETCore.App");
            if (Directory.Exists(path))
            {
                return Directory.GetDirectories(path).Any(d => d.Contains(version));
            }
            return false;
        }

        public bool IsVb6RuntimeInstalled()
        {
            // La DLL msvbvm60 es el corazón del runtime de VB6
            // En sistemas de 64 bits, las apps de 32 bits (VB6) buscan en SysWOW64
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.SystemX86), "msvbvm60.dll");
            return File.Exists(path);
        }


    }
}
