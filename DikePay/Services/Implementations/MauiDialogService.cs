using DikePay.Application.Interfaces.Maui;

namespace DikePay.Services.Implementations
{
    public class MauiDialogService : IDialogService
    {
        public async Task AlertaAsync(string titulo, string mensaje, string boton)
            => await Shell.Current.DisplayAlert(titulo, mensaje, boton);

        public async Task AbrirNavegadorAsync(string url)
            => await Launcher.Default.OpenAsync(url);
    }
}
