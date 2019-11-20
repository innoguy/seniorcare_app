﻿using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SeniorCare.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ThresholdsPage : PageBase
    {
		public ThresholdsPage ()
		{
			InitializeComponent ();
		}

        private void OnTimePickerFocus(object sender, FocusEventArgs e)
        {
            ((TimePicker)sender).Unfocus();

        }
    }
}