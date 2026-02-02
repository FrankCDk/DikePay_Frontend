namespace DikePay.Application.Interfaces.Maui
{
    public interface IDialogService
    {
        Task AlertaAsync(string titulo, string mensaje, string boton);
        Task AbrirNavegadorAsync(string url);
    }
}
