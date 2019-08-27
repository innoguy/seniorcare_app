using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Niko.IoC;
using SeniorCare.BaseClasses;
using SeniorCare.Resources;
using ServiceModule.Notifications;
using ServiceModule.Notifications.Models;
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
            Notifications = new ObservableCollection<NotificationViewModel>();
            ThreadPool.QueueUserWorkItem(async o => await BackgroundAsync());
        }

        private async Task GetNotifications()
        {
            var endTime = ((Int64) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds).ToString();

            var notificationEntities = await NotificationsDataservice.GetNotifications("device_id_1", _startTime, endTime);
            _startTime = endTime;
            if (!notificationEntities.Any()) return;

            await UpdateNotifications(notificationEntities);
        }

        private async Task UpdateNotifications(IEnumerable<NotificationEntity> notificationEntities)
        {
            AudioPlayer.PlayNotification();
            await Device.InvokeOnMainThreadAsync(() =>
            {
                foreach (var notificationEntity in notificationEntities)
                {
                    Notifications.Insert(0, new NotificationViewModel(
                        notificationEntity.Ruleid,
                        notificationEntity.Controllerid,
                        UnixTimeStampToDateTime(notificationEntity.Time),
                        notificationEntity.Message));
                }
            });
        }

        public string UnixTimeStampToDateTime(long unixTimeStamp)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            double time = unixTimeStamp;
            dtDateTime = dtDateTime.AddMilliseconds(time).ToLocalTime();
            string dateTime = dtDateTime.ToString("hh:mm:ss dd/MM/yyy");
            return dateTime;
        }


        private async Task BackgroundAsync()
        {
            while (true)
            {
                await GetNotifications();
                await Task.Delay(1000);
            }
        }

    }
}
