using Android.App;
using Android.Content.PM;
using Android.OS;

namespace Centrum
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density, ScreenOrientation = ScreenOrientation.Locked)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            Window.SetStatusBarColor(Android.Graphics.Color.ParseColor("#c9c9c9"));
            
            base.OnCreate(savedInstanceState);
        }
    }
}
