using System.Collections.ObjectModel;
using System.Windows.Input;
using SeniorCare.BaseClasses;
using SeniorCare.Resources;
using Xamarin.Forms;

namespace SeniorCare.ViewModels
{
    public class NotificationsViewModel : ViewModelBase
    {
        private ObservableCollection<NotificationViewModel> _notifications;
        public ObservableCollection<NotificationViewModel> Notifications
        {
            get => _notifications;
            set => SetProperty(ref _notifications, value);
        }

        private NotificationViewModel _selectedDevice;
        public NotificationViewModel SelectedDevice
        {
            get => _selectedDevice;
            set
            {
                if (_selectedDevice != null)
                {
                    _selectedDevice.IsSelected = false;
                }
                if (value != null)
                {
                    value.IsSelected = true;
                }
                SetProperty(ref _selectedDevice, value);
            }
        }

        public ICommand RefreshCommand { get; }

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        public NotificationsViewModel()
        {
            Title = AppLocalization.NotificationsPage_Title;

            RefreshCommand = new Command(() =>
            {
                IsRefreshing = true;
                LoadItems();
                IsRefreshing = false;
            });

            Notifications = GetNotifications();
        }

        private void LoadItems()
        {
            lock (syncRoot)
            {
                Notifications = GetNotifications();
            }
        }

        private ObservableCollection<NotificationViewModel> GetNotifications()
        {
            return new ObservableCollection<NotificationViewModel>
            {
                new NotificationViewModel("Device Id: 1", "5234523455", "124142152", "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry\'s standard dummy text ever since the 1500s"),
                new NotificationViewModel("Device Id: 2", "5234523455", "124142152", "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry\'s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries"),
                new NotificationViewModel("Device Id: 3", "5234523455", "124142152", "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old"),
                new NotificationViewModel("Device Id: 4", "5234523455", "124142152", "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. If you are going to use a passage of Lorem Ipsum"),
            };
        }
    }
}
