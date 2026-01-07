using DikePay.Application.Interfaces;

namespace DikePay.Services.Implementations
{
    public class MauiPathProvider : IPathProvider
    {
        public string GetDatabasePath()
        {
            // Aquí SÍ tienes acceso a FileSystem porque estás dentro del proyecto MAUI
            return Path.Combine(FileSystem.AppDataDirectory, "DikePayLocal.db3");
        }
    }
}
   