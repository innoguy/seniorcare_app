using Android.App;
using Android.OS;
using SeniorCare.Droid.Providers;
using Xamarin.Forms.Platform.Android;
using XF.Infrastructure.Core.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidStatusBarProvider))]
namespace SeniorCare.Droid.Providers
{
    public class AndroidStatusBarProvider : IStatusBarProvider
    {
        private static Activity _activity;

        public static void Init(Activity activity) => _activity = activity;

        public void SetStatusBarColor(Xamarin.Forms.Color color)
        {
            if (_activity == null || Build.VERSION.SdkInt < BuildVersionCodes.Lollipop)
                return;

            _activity.Window?.SetStatusBarColor(color.ToAndroid());
        }
    }
}