using Android.App;
using Android.Content.PM;
using Android.OS;

namespace DikePay
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
            // No ZXing platform-specific initialization required here when using UseBarcodeReader() in MauiProgram
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            // If any library needs permission forwarding, add here. For now forward to base implementation.
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
