using Foundation;
using UIKit;
using Xamarin.Forms.Platform.iOS;

namespace SeniorCare.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            var statusBar = UIApplication.SharedApplication.ValueForKey(new NSString("statusBar")) as UIView;
            if (statusBar.RespondsToSelector(new ObjCRuntime.Selector("setBackgroundColor:")))
            {
                var statusBarColor = Xamarin.Forms.Color.FromHex("#1A1A1A");
                statusBar.BackgroundColor = statusBarColor.ToUIColor();
                statusBar.TintColor = UIColor.White;
            }
            Rg.Plugins.Popup.Popup.Init();
            app.StatusBarStyle = UIStatusBarStyle.LightContent;
            FormsControls.Touch.Main.Init();

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
