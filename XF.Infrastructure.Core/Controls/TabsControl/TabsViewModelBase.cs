using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XF.Infrastructure.Core;
using XF.Infrastructure.Core.Controls;

namespace XF.Infrastructure.Core.Controls
{
    public abstract class TabsViewModelBase : AbstractNpcObject, ITabsViewModel, IDisposable
    {
        #region Properties
        #region ITabsViewModel
        public IPageLocator PageLocator { get; set; }

        private IEnumerable<IViewModel> _tabItems;
        public IEnumerable<IViewModel> TabItems
        {
            get { return _tabItems; }
            set { SetProperty(ref _tabItems, value); }
        }

        private IViewModel _selectedTab;
        public IViewModel SelectedTab
        {
            get { return _selectedTab; }
            set { SetProperty(ref _selectedTab, value); }
        }

        public ICommand TabItemTappedCommand { get; private set; }
        #endregion

        #region IViewModel
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { SetProperty(ref _isSelected, value); }
        }
        #endregion
        #endregion

        public TabsViewModelBase()
        {

        }
        public TabsViewModelBase(IPageLocator pageLocator)
        {
            PageLocator = pageLocator;
            this.PropertyChanged += OnViewModelPropertyChanged;
            //  TabItemTappedCommand = new Command(OnTabItemTapped);
        }

        private void OnTabItemTapped()
        {

        }

        #region IViewModelMethods
        public virtual async Task Init(object args)
        {
            await Task.Delay(0);
        }

        public virtual void OnAppearing()
        {

        }

        public virtual bool OnBackButtonPressed(Action callback)
        {
            return false;
        }

        public virtual void OnDisappearing()
        {

        }

        protected virtual void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

        }
        #endregion
        public virtual void Dispose()
        {
            this.PropertyChanged -= OnViewModelPropertyChanged;

        }
    }
}
