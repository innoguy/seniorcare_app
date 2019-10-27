using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Services;
using ServiceModule.Settings;
using Xamarin.Forms.Xaml;

namespace SeniorCare.Popups
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class IpSetupPopup
	{
	    private readonly ISettingsService _settingsService;

        public IpSetupPopup (ISettingsService settingsService)
        {
            _settingsService = settingsService ?? throw new ArgumentNullException(nameof(settingsService));
 
            InitializeComponent ();
            ProtocolSelection.ItemsSource = new List<string> {"http", "https"};
            InitializeViewItems();
        }

        private async void SaveButton_OnClicked(object sender, EventArgs e)
        {
            _settingsService.Protocol = ProtocolSelection.SelectedItem.ToString();
            _settingsService.IpAddress = IpAddressEntry.Text;
            _settingsService.Port = PortEntry.Text;

            await PopupNavigation.Instance.PopAsync();
        }

	    private void InitializeViewItems()
	    {
	        IpAddressEntry.Text = _settingsService.IpAddress;
	        PortEntry.Text = _settingsService.Port;
	        ProtocolSelection.SelectedItem = _settingsService.Protocol;
	    }
    }
}