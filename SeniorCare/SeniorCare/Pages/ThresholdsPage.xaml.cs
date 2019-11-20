using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Infrastructure.Core.Controls;

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

        private void OnEntryFocus(object sender, FocusEventArgs e)
        {
            ((ExtEntry)sender).Unfocus();
        }
    }
}