using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Infrastructure.Core;

namespace XF.Infrastructure.Core.Controls
{


    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabsControl : ContentView
    {
        #region Properties
        private static readonly object _syncRoot = new object();

        public static readonly BindableProperty PageLocatorProperty = BindableProperty.Create("PageLocator", typeof(IPageLocator), typeof(TabsControl), null, BindingMode.OneWay, null, null, null, null, null);
        public IPageLocator PageLocator
        {
            get
            {
                return (IPageLocator)base.GetValue(TabsControl.PageLocatorProperty);
            }
            set
            {
                base.SetValue(TabsControl.PageLocatorProperty, value);
            }
        }


        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create("ItemsSource", typeof(IEnumerable<IViewModel>), typeof(TabsControl), null, BindingMode.OneWay, null, null, null, null, null);
        public IEnumerable<IViewModel> ItemsSource
        {
            get
            {
                return (IEnumerable<IViewModel>)base.GetValue(TabsControl.ItemsSourceProperty);
            }
            set
            {
                base.SetValue(TabsControl.ItemsSourceProperty, value);
            }
        }

        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create("SelectedItem", typeof(IViewModel), typeof(TabsControl), null, BindingMode.TwoWay, null, null, null, null, null);
        public IViewModel SelectedItem
        {
            get
            {
                return (IViewModel)base.GetValue(TabsControl.SelectedItemProperty);
            }
            set
            {
                lock (_syncRoot)
                {
                    if (SelectedItem != null)
                    {
                        SelectedItem.IsSelected = false;
                        SelectedItem.OnDisappearing();
                    }
                    base.SetValue(TabsControl.SelectedItemProperty, value);
                    SelectedItem.IsSelected = true;
                    OnSelectedItemChanged();
                    SelectedItem.OnAppearing();
                }
            }
        }

        public static readonly BindableProperty HeaderItemTemplateProperty = BindableProperty.Create("HeaderItemTemplate", typeof(DataTemplate), typeof(TabsControl), null, BindingMode.OneWay, null, null, null, null, null);
        public DataTemplate HeaderItemTemplate
        {
            get
            {
                return (DataTemplate)base.GetValue(TabsControl.HeaderItemTemplateProperty);
            }
            set
            {
                base.SetValue(TabsControl.HeaderItemTemplateProperty, value);
            }
        }


        public static readonly BindableProperty TabItemMinWidthProperty = BindableProperty.Create("TabItemMinWidth", typeof(double), typeof(TabsControl), null, BindingMode.OneWay, null, null, null, null, null);
        public double TabItemMinWidth
        {
            get
            {
                return (double)base.GetValue(TabsControl.TabItemMinWidthProperty);
            }
            set
            {
                base.SetValue(TabsControl.TabItemMinWidthProperty, value);
            }
        }

        public static readonly BindableProperty TabItemMaxWidthProperty = BindableProperty.Create("TabItemMaxWidth", typeof(double), typeof(TabsControl), null, BindingMode.OneWay, null, null, null, null, null);
        public double TabItemMaxWidth
        {
            get
            {
                return (double)base.GetValue(TabsControl.TabItemMaxWidthProperty);
            }
            set
            {
                base.SetValue(TabsControl.TabItemMaxWidthProperty, value);
            }
        }


        public static readonly BindableProperty TabItemWidthProperty = BindableProperty.Create("TabItemWidth", typeof(double), typeof(TabsControl), null, BindingMode.OneWay, null, null, null, null, null);
        public double TabItemWidth
        {
            get
            {
                return (double)base.GetValue(TabsControl.TabItemWidthProperty);
            }
            set
            {
                base.SetValue(TabsControl.TabItemWidthProperty, value);
            }
        }

        public static readonly BindableProperty TabItemHasFixedSizeProperty = BindableProperty.Create("TabItemHasFixedSize", typeof(bool?), typeof(TabsControl), true, BindingMode.OneWay, null, null, null, null, null);
        public bool? TabItemHasFixedSize
        {
            get
            {
                return (bool?)base.GetValue(TabsControl.TabItemHasFixedSizeProperty);
            }
            set
            {
                base.SetValue(TabsControl.TabItemHasFixedSizeProperty, value);
            }
        }



        public static readonly BindableProperty HasHorizontalScrollEnabledProperty = BindableProperty.Create("HasHorizontalScrollEnabled", typeof(bool?), typeof(TabsControl), null, BindingMode.OneWay, null, null, null, null, null);
        public bool? HasHorizontalScrollEnabled
        {
            get
            {
                return (bool?)base.GetValue(TabsControl.HasHorizontalScrollEnabledProperty);
            }
            set
            {
                base.SetValue(TabsControl.HasHorizontalScrollEnabledProperty, value);
            }
        }

        public static readonly BindableProperty CanChnageTabOnSwipeProperty = BindableProperty.Create("CanChnageTabOnSwipe", typeof(bool), typeof(TabsControl), false, BindingMode.OneWay, null, null, null, null, null);
        public bool CanChnageTabOnSwipe
        {
            get
            {
                return (bool)base.GetValue(TabsControl.CanChnageTabOnSwipeProperty);
            }
            set
            {
                base.SetValue(TabsControl.CanChnageTabOnSwipeProperty, value);
            }
        }

        private readonly TapGestureRecognizer _tabItemTappedGestureRecognizer;
        private readonly SwipeContainer _swipeContainer;
        #endregion

        public TabsControl()
        {
            InitializeComponent();
            _swipeContainer = new SwipeContainer();
            _swipeContainer.Swipe += TabContent_Swiped;
            Root.Children.Add(_swipeContainer);
            Grid.SetRow(_swipeContainer, 1);

            _tabItemTappedGestureRecognizer = new TapGestureRecognizer();
            _tabItemTappedGestureRecognizer.Tapped += OnTabItemTapped;

            TabsRepeater.SizeChanged += TabsRepeater_SizeChanged;
            this.PropertyChanged += new PropertyChangedEventHandler(BindableOnPropertyChanged);
        }

        private void TabContent_Swiped(object sender, SwipedEventArgs e)
        {
            lock (_syncRoot)
            {
                if (ItemsSource == null) return;
                if (SelectedItem == null) return;
                var index = ItemsSource.ToList().IndexOf(SelectedItem);
                switch (e.Direction)
                {
                    case SwipeDirection.Left:
                        UpdateSelectedItem(index + 1);
                        break;
                    case SwipeDirection.Right:
                        UpdateSelectedItem(index - 1);
                        break;
                }
            }
        }

        private void UpdateSelectedItem(int index)
        {
            if (index < 0) index = ItemsSource.Count() - 1;
            if (index > ItemsSource.Count() - 1) index = 0;
            SelectedItem = ItemsSource.ToList()[index];

            var item = TabsRepeater.Children[index];
            TabsScroll.ScrollToAsync(item, ScrollToPosition.MakeVisible, false);
        }

        #region Methods
        private void BindableOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == TabsControl.ItemsSourceProperty.PropertyName)
            {
                TabsRepeater.ItemsSource = ItemsSource;
                SelectedItem = ItemsSource.FirstOrDefault();
                return;
            }
            if (e.PropertyName == TabsControl.HeaderItemTemplateProperty.PropertyName)
            {
                CreateRepeaterTemplate();
                OnSelectedItemChanged();
                return;
            }
            if (e.PropertyName == TabsControl.TabItemMinWidthProperty.PropertyName)
            {
                if (TabItemHasFixedSize.HasValue && TabItemHasFixedSize.Value) throw new ArgumentException("TabItemMinWidth cannot be set when TabItemHasFixedSize=True. Please use TabItemWidth property insdead!");
                if (TabItemWidth > 0 && TabItemMinWidth > 0 && TabItemMinWidth > TabItemWidth)
                {
                    throw new ArgumentException("TabItemMinWidth > TabItemWidth");
                }
                if (TabItemMinWidth > 0 && TabItemMaxWidth > 0 && TabItemMinWidth > TabItemMaxWidth)
                {
                    throw new ArgumentException("TabItemMinWidth > TabItemMaxWidth");
                }
                CreateRepeaterTemplate();
                return;
            }
            if (e.PropertyName == TabsControl.TabItemMaxWidthProperty.PropertyName)
            {
                if (TabItemHasFixedSize.HasValue && TabItemHasFixedSize.Value) throw new ArgumentException("TabItemMinWidth cannot be set when TabItemHasFixedSize=True. Please use TabItemWidth property insdead!");
                if (TabItemWidth > 0 && TabItemMaxWidth > 0 && TabItemWidth > TabItemMaxWidth)
                {
                    throw new ArgumentException("TabItemWidth > TabItemMaxWidth");
                }
                if (TabItemMinWidth > 0 && TabItemMaxWidth > 0 && TabItemMinWidth > TabItemMaxWidth)
                {
                    throw new ArgumentException("TabItemMinWidth > TabItemMaxWidth");
                }
                CreateRepeaterTemplate();
                return;
            }
            if (e.PropertyName == TabsControl.TabItemWidthProperty.PropertyName)
            {
                if (TabItemWidth > 0 && TabItemMinWidth > 0 && TabItemMinWidth > TabItemWidth)
                {
                    throw new ArgumentException("TabItemMinWidth > TabItemWidth");
                }
                if (TabItemWidth > 0 && TabItemMaxWidth > 0 && TabItemWidth > TabItemMaxWidth)
                {
                    throw new ArgumentException("TabItemWidth > TabItemMaxWidth");
                }
                CreateRepeaterTemplate();
                return;
            }

            if (e.PropertyName == TabsControl.HasHorizontalScrollEnabledProperty.PropertyName)
            {
                if (HasHorizontalScrollEnabled == false && TabItemHasFixedSize == false)
                {
                    throw new ArgumentException("HasHorizontalScrollEnabled must be True when TabItemHasFixedSize=False");
                }
                CreateRepeaterTemplate();
                return;
            }
            if (e.PropertyName == TabsControl.TabItemHasFixedSizeProperty.PropertyName)
            {
                if (TabItemMinWidth > 0) throw new ArgumentException("TabItemMinWidth cannot be set when TabItemHasFixedSize=True. Please use TabItemWidth property insdead!");
                if (TabItemMaxWidth > 0) throw new ArgumentException("TabItemMaxWidth cannot be set when TabItemHasFixedSize=True. Please use TabItemWidth property insdead!");
                if (HasHorizontalScrollEnabled == false && TabItemHasFixedSize == false)
                {
                    throw new ArgumentException("HasHorizontalScrollEnabled must be True when TabItemHasFixedSize=False");
                }
                CreateRepeaterTemplate();
                return;
            }

            if (e.PropertyName == TabsControl.PageLocatorProperty.PropertyName)
            {
                OnSelectedItemChanged();
                return;
            }
        }

        private void CreateRepeaterTemplate()
        {
            lock (_syncRoot)
            {
                TabsRepeater.ItemTemplate = new DataTemplate(() =>
                {
                    if (HeaderItemTemplate == null)
                    {
                        throw new ArgumentException("HeaderItemTemplate property cannot be null");
                    }
                    Frame headerView = new Frame() { Margin = 0, Padding = 0, HasShadow = false };
                    if (TabItemMinWidth > 0) { headerView.MinimumWidthRequest = TabItemMinWidth; }
                    if (TabItemWidth > 0) headerView.WidthRequest = TabItemWidth;
                    headerView.GestureRecognizers.Add(_tabItemTappedGestureRecognizer);
                    if (HeaderItemTemplate != null)
                    {
                        var view = HeaderItemTemplate.CreateContent() as View;
                        headerView.Content = view;
                    }
                    return headerView;
                });
            }
        }

        private void TabsRepeater_SizeChanged(object sender, EventArgs e)
        {
            lock (_syncRoot)
            {
                if (ItemsSource == null) return;
                if (HeaderItemTemplateProperty == null) return;
                if (!TabsRepeater.ItemsLoaded) return;
                StackLayout rep = (TabsRepeater as StackLayout);
                if (rep == null) return;
                if (TabItemHasFixedSize.HasValue && TabItemHasFixedSize.Value)
                {
                    ResizeForEvenTabs(rep);
                }
                else
                {
                    ResizeForUnevenTabs(rep);
                }
            }
        }

        private void ResizeForUnevenTabs(StackLayout repeater)
        {
            if (TabItemMaxWidth > 0)
            {
                if (TabsRepeater.Width > Root.Width)
                {
                    var tabItem = repeater.Children.FirstOrDefault(t => t.Width != TabItemMaxWidth);
                    if (tabItem == null) return;
                    tabItem.WidthRequest = TabItemMaxWidth;
                }

                if (TabsRepeater.Width <= Root.Width)
                {
                    var tabItem = repeater.Children.LastOrDefault(t => t.Width != TabItemMaxWidth);
                    if (tabItem == null) return;
                    tabItem.WidthRequest = Root.Width - TabItemMaxWidth;
                }
            }
        }

        private void ResizeForEvenTabs(StackLayout repeater)
        {
            double desiredWidth = -1;
            if (TabItemHasFixedSize.HasValue && TabItemHasFixedSize.Value)
            {
                if (TabItemWidth > 0)
                {
                    desiredWidth = TabItemWidth;
                }
                else
                {
                    if (HasHorizontalScrollEnabled == true)
                    {
                        desiredWidth = GetLargestItemWidth(repeater);
                    }
                    else
                    {
                        desiredWidth = Root.Width / ItemsSource.Count();
                    }
                }
            }

            var tabItem = repeater.Children.FirstOrDefault(x => x.Width != desiredWidth);
            if (tabItem == null) return;
            tabItem.WidthRequest = desiredWidth;
        }

        private double GetLargestItemWidth(StackLayout repeater)
        {
            var largestItem = repeater.Children.OrderByDescending(x => x.Width).FirstOrDefault();
            if (largestItem == null) return -1;
            return largestItem.Width;
        }

        private void OnTabItemTapped(object sender, EventArgs e)
        {
            lock (_syncRoot)
            {
                if (PageLocator == null) return;
                var control = sender as BindableObject;
                if (control == null) return;
                var vm = control.BindingContext as IViewModel;
                if (vm == null) return;
                if (vm == SelectedItem) return;
                SelectedItem = vm;
            }

        }

        private void OnSelectedItemChanged()
        {
            if (PageLocator == null) return;
            if (SelectedItem == null) return;
            if (HeaderItemTemplate == null) return;
            SelectedItem.IsInitializing = true;
            var page = PageLocator.ResolveContentPage(SelectedItem);
            if (page == null) return;
            _swipeContainer.Content = page.Content;
            _swipeContainer.BindingContext = SelectedItem;
        }
        #endregion


    }

}