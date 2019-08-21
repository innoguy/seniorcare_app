using FormsControls.Base;
using Xamarin.Forms;

namespace SeniorCare.Pages
{
    public class PageBase : AnimationPage
    {
        public PageBase()
        {
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}