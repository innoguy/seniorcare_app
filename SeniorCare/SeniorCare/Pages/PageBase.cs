using FormsControls.Base;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace SeniorCare.Pages
{
    public class PageBase : AnimationPage
    {
        public PageBase()
        {
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }
    }
}