using FormsControls.Base;
using System;
using Xamarin.Forms;

namespace XF.Infrastructure.Core
{
    public interface IPageLocator
    {
        AnimationPage ResolvePageAndViewModel(Type viewModelType, object args = null);
        AnimationPage ResolvePage(IViewModel viewModel);
        ContentPage ResolveContentPage(IViewModel viewModel);
    }
}
