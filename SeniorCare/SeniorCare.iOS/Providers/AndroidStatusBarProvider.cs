using Foundation;
using SeniorCare.iOS.Providers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XF.Infrastructure.Core.Interfaces;

[assembly: Dependency(typeof(AppleStatusBarProvider))]
namespace SeniorCare.iOS.Providers
{
    public class AppleStatusBarProvider : IStatusBarProvider
    {
        public void SetStatusBarColor(Color color)
        {
            if (UIApplication.SharedApplication.ValueForKey(new NSString("statusBar")) is UIView statusBar &&
                statusBar.RespondsToSelector(new ObjCRuntime.Selector("setBackgroundColor:")))
            {
                statusBar.BackgroundColor = color.ToUIColor();
            }
        }
    }
}