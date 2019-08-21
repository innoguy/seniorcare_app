using System.Collections;
using System.ComponentModel;
using Xamarin.Forms;

namespace XF.Infrastructure.Core.Controls
{
    public class RepeaterView : StackLayout
    {
        #region Properties
        private readonly object _syncRoot = new object();
        public bool ItemsLoaded { get; set; }

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create("ItemsSource", typeof(IEnumerable), typeof(RepeaterView), null, BindingMode.OneWay, null, null, null, null, null);
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create("ItemTemplate", typeof(DataTemplate), typeof(RepeaterView), null, BindingMode.OneWay, null, null, null, null, null);

        public IEnumerable ItemsSource
        {
            get
            {
                return (IEnumerable)base.GetValue(RepeaterView.ItemsSourceProperty);
            }
            set
            {
                base.SetValue(RepeaterView.ItemsSourceProperty, value);
            }
        }

        public DataTemplate ItemTemplate
        {
            get
            {
                return (DataTemplate)base.GetValue(RepeaterView.ItemTemplateProperty);
            }
            set
            {
                base.SetValue(RepeaterView.ItemTemplateProperty, value);
            }
        }
        #endregion
        public RepeaterView()
        {
            this.PropertyChanged += new PropertyChangedEventHandler(BindableOnPropertyChanged);
        }

        #region Methods
        private void BindableOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == RepeaterView.ItemsSourceProperty.PropertyName)
            {
                ItemsLoaded = false;
                GenerateChildren();
                return;
            }

            if (e.PropertyName == RepeaterView.ItemTemplateProperty.PropertyName)
            {
                GenerateChildren();
                return;
            }
        }

        protected void GenerateChildren()
        {
            lock (_syncRoot)
            {
                if (this == null)
                {
                    return;
                }

                this.Children.Clear();

                if (this.ItemsSource == null)
                {
                    return;
                }

                foreach (var item in this.ItemsSource)
                {
                    View view = null;
                    if (ItemTemplate != null)
                    {

                        view = ItemTemplate.CreateContent() as View;

                        if (view != null)
                        {
                            view.BindingContext = item;
                        }
                    }
                    else
                    {
                        view = this.CreateDefault(item);
                    }

                    if (view == null)
                    {
                        continue;
                    }

                    this.Children.Add(view);

                }
                this.OnSizeAllocated(this.Width, this.Height);
                ItemsLoaded = true;
            }

        }

        protected View CreateDefault(object item)
        {
            return new Label { Text = item.ToString() };
        }
        #endregion
    }
}
