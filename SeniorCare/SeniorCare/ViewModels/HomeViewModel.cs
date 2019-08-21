using System.Collections.Generic;
using Niko.IoC;
using XF.Infrastructure.Core;
using XF.Infrastructure.Core.Controls;

namespace SeniorCare.ViewModels
{
    public class HomeViewModel : TabsViewModelBase
    {
        private INavigationService _navigationService;
        public INavigationService NavigationService
        {
            get
            {
                if (_navigationService == null)
                {
                    _navigationService = AutofacIoC.Resolve<INavigationService>();
                }
                return _navigationService;
            }
        }

        public HomeViewModel()
        {
            PageLocator = AutofacIoC.Resolve<IPageLocator>();
            Title = "Home";
            TabItems = new List<IViewModel> { new ThresholdsViewModel(), new NotificationsViewModel() };
        }
    }
}
