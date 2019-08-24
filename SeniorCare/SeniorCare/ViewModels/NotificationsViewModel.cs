using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Niko.IoC;
using SeniorCare.BaseClasses;
using SeniorCare.Resources;
using ServiceModule.Notifications;
using Xamarin.Forms;

namespace SeniorCare.ViewModels
{
    public class NotificationsViewModel : ViewModelBase
    {
        private static string _startTime = "0";

        private INotificationsDataservice _notificationsDataservice;
        public INotificationsDataservice NotificationsDataservice =>
            _notificationsDataservice ?? (_notificationsDataservice = AutofacIoC.Resolve<INotificationsDataservice>());

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
            RefreshCommand = new Command(() => { IsRefreshing = false; });

            ThreadPool.QueueUserWorkItem(async o => await BackgroundAsync());
        }

        private async void GetNotifications()
        {
            var endTime = ((Int64) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds).ToString();

            Notifications = new ObservableCollection<NotificationViewModel>();
            var notificationEntities = await NotificationsDataservice.GetNotifications("device_id_1", _startTime, endTime);
            _startTime = endTime;
            var notificationViewModels = new ObservableCollection<NotificationViewModel>();
            foreach (var notificationEntity in notificationEntities)
            {
                notificationViewModels.Add(new NotificationViewModel(
                    notificationEntity.Ruleid,
                    notificationEntity.Controllerid,
                    notificationEntity.Time,
                    notificationEntity.Message));
            }

            await Device.InvokeOnMainThreadAsync(() => { Notifications = notificationViewModels; });
        }

        private async Task BackgroundAsync()
        {
            while (true)
            {
                GetNotifications();
                await Task.Delay(1000);
            }
        }

    }
}
