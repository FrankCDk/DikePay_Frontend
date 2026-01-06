using DikePay.Components.Pages;

namespace DikePay.Helpers
{
    public class BarcodeScannerService
    {
        public async Task<string?> ScanAsync()
        {
            var page = new ScannerPage();
            await Microsoft.Maui.Controls.Application.Current.MainPage.Navigation.PushModalAsync(page);
            return await page.Result.Task;
        }
    }

}
