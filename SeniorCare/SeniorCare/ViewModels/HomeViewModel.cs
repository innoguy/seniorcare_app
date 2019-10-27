using System.Collections.Generic;
using System.Windows.Input;
using Niko.IoC;
using Rg.Plugins.Popup.Services;
using SeniorCare.Popups;
using ServiceModule.Settings;
using Xamarin.Forms;
using XF.Infrastructure.Core;
using XF.Infrastructure.Core.Controls;

namespace SeniorCare.ViewModels
{
    public class HomeViewModel : TabsViewModelBase
    {
        private INavigationService _navigationService;
        public INavigationService NavigationService => _navigationService ?? (_navigationService = AutofacIoC.Resolve<INavigationService>());

        private ISettingsService _settingsService;
        public ISettingsService SettingsService => _settingsService ?? (_settingsService = AutofacIoC.Resolve<ISettingsService>());


        public HomeViewModel()
        {
            PageLocator = AutofacIoC.Resolve<IPageLocator>();
            Title = "Home";
            TabItems = new List<IViewModel> { new ThresholdsViewModel(), new NotificationsViewModel() };

            OpenIpConfigCommand = new Command(OpenIpConfigCommandAction);
        }

        public ICommand OpenIpConfigCommand { get; }

        private async void OpenIpConfigCommandAction()
        {
            var ipSetupPopup = new IpSetupPopup(SettingsService);
            await PopupNavigation.Instance.PushAsync(ipSetupPopup);
        }
    }
}
