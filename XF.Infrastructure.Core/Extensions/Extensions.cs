using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XF.Infrastructure.Core
{
    public static class Extensions
    {
        public static void BindViewModel(this Page page, IViewModel viewModel)
        {
            page.BindingContext = viewModel;
            page.Appearing += (sender, args1) => viewModel.OnAppearing();
            page.Disappearing += (sender, args1) => viewModel.OnDisappearing();
            page.SetBinding(Page.IsBusyProperty, "IsBusy");
        }

        public static void BindViewModel(this View page, IViewModel viewModel)
        {
            page.BindingContext = viewModel;
        }
    }
}
