using FormsControls.Base;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XF.Infrastructure.Core
{
    public interface INavigationService
    {
        AnimationNavigationPage SetMainPage(Type targetType, NavigationAnimation animation, Color? color = null, object args = null);

        Task NavigateToViewModelAsync(Type viewModelType, NavigationAnimation animation, Color? color = null, bool animated = false, bool modal = false, object args = null);

        Task NavigateToViewModelAsync(IViewModel viewModel, NavigationAnimation animation, Color? color = null, bool animated = false, bool modal = false);

        Task PopAsync(bool animated = false);

        Task PopToRootAsync(bool animated = false);

        void Pop(bool animated = false);

        Task PopModalAsync(NavigationAnimation animation, bool animated = false);

    }
}
