using Niko.IoC;
using SeniorCare.Resources;
using SeniorCare.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Infrastructure.Core;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SeniorCare
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var navigationService = AutofacIoC.Resolve<INavigationService>();
            MainPage = navigationService.SetMainPage(typeof(HomeViewModel), NavigationAnimation.None, NikoColors.NikoBlackColor);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
