using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using Niko.IoC;
using SeniorCare.Resources;
using ServiceModule.Settings;
using Xamarin.Forms;
using XF.Infrastructure.Core;

namespace SeniorCare.BaseClasses
{
    public class ViewModelBase : AbstractNpcObject, IViewModel, IDisposable
    {
        #region Properties
        protected readonly object syncRoot = new object();

        private string _title;
        public bool IsInitializing { get; set; }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }


        private string _isBusyMessage;
        public string IsBusyMessage
        {
            get => _isBusyMessage;
            set => SetProperty(ref _isBusyMessage, value);
        }

        private bool _isBusy;
        public bool IsBusy
        {

            get => _isBusy;
            set
            {
                if (DialogsService != null)
                {
                    if (_isBusy != value && value)
                    {
                        DialogsService.ShowLoading(IsBusyMessage ?? AppLocalization.Message_PleaseWait, MaskType.Gradient);
                    }

                    if (!value)
                    {
                        DialogsService.HideLoading();
                    }
                }

                SetProperty(ref _isBusy, value);
            }
        }



        private IUserDialogs _dialogsService;
        public IUserDialogs DialogsService
        {
            get
            {
                if (_dialogsService == null)
                {
                    _dialogsService = AutofacIoC.Resolve<IUserDialogs>();
                }
                return _dialogsService;
            }
        }

        private IPageLocator _pageLocator;
        public IPageLocator PageLocator
        {
            get
            {
                if (_pageLocator == null)
                {
                    _pageLocator = AutofacIoC.Resolve<IPageLocator>();
                }
                return _pageLocator;
            }
        }

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

        private ISettingsService _settingsService;
        public ISettingsService SettingsService
        {
            get
            {
                if (_settingsService == null)
                {
                    _settingsService = AutofacIoC.Resolve<ISettingsService>();
                }
                return _settingsService;
            }
        }

        public ICommand BackCommand { get; }
        public ICommand GoToHomePageCommand { get; }

        #endregion
        public ViewModelBase()
        {
            this.PropertyChanged += OnViewModelPropertyChanged;
            BackCommand = new Command(async () => { await OnBackCommandAction(); });
            GoToHomePageCommand = new Command(async () => { await GoToHomePageCommandAction(); });
        }

        protected virtual void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

        }

        public virtual async Task Init(object args)
        {
            await Task.Delay(0);
        }

        public virtual void OnAppearing()
        {

        }

        public virtual async Task GoToHomePageCommandAction()
        {
            await NavigationService.PopToRootAsync();
        }
        public virtual async Task OnBackCommandAction()
        {
            await NavigationService.PopAsync();
        }

        public virtual bool OnBackButtonPressed(Action callback)
        {
            return false;
        }

        public virtual void OnDisappearing()
        {

        }

        public virtual void Dispose()
        {
            PropertyChanged -= OnViewModelPropertyChanged;
            _navigationService = null;
            _pageLocator = null;
        }
    }
}
