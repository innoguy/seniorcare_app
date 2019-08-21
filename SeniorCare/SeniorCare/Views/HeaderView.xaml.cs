using System.Runtime.CompilerServices;
using System.Windows.Input;
using SeniorCare.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SeniorCare.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HeaderView : ContentView
	{
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(HeaderView), string.Empty);
        public static readonly BindableProperty TitleColorProperty = BindableProperty.Create(nameof(TitleColor), typeof(Color), typeof(HeaderView), NikoColors.NikoWhiteColor);
        public static readonly BindableProperty LeftMenuItemCommandProperty = BindableProperty.Create(nameof(LeftMenuItemCommand), typeof(ICommand), typeof(HeaderView));
        public static readonly BindableProperty LeftMenuIconProperty = BindableProperty.Create(nameof(LeftMenuIcon), typeof(ImageSource), typeof(HeaderView));
        public static readonly BindableProperty RightMenuItemCommandProperty = BindableProperty.Create(nameof(RightMenuItemCommand), typeof(ICommand), typeof(HeaderView));
        public static readonly BindableProperty RightMenuIconProperty = BindableProperty.Create(nameof(RightMenuIcon), typeof(ImageSource), typeof(HeaderView));
        public static readonly BindableProperty BottomBorderProperty = BindableProperty.Create(nameof(BottomBorder), typeof(bool), typeof(HeaderView), false);
        public static readonly BindableProperty BottomBorderColorProperty = BindableProperty.Create(nameof(BottomBorderColor), typeof(Color), typeof(HeaderView), NikoColors.NikoHardGreyColor);
        public static readonly BindableProperty BottomBorderHeightProperty = BindableProperty.Create(nameof(BottomBorderHeight), typeof(int), typeof(HeaderView), 1);

        public HeaderView()
        {
            InitializeComponent();
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (TextContainer == null) return;
            if (LeftMenuIconProperty.PropertyName == propertyName && LeftMenuIcon != null)
            {
                Grid.SetColumn(TextContainer, 1);
                Grid.SetColumnSpan(TextContainer, 1);
            }

            if (RightMenuIconProperty.PropertyName == propertyName && RightMenuIcon != null)
            {
                Grid.SetColumn(TextContainer, 1);
                Grid.SetColumnSpan(TextContainer, 1);
            }
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public Color TitleColor
        {
            get => (Color)GetValue(TitleColorProperty);
            set => SetValue(TitleColorProperty, value);
        }

        public ICommand LeftMenuItemCommand
        {
            get => (ICommand)GetValue(LeftMenuItemCommandProperty);
            set => SetValue(LeftMenuItemCommandProperty, value);
        }

        public ImageSource LeftMenuIcon
        {
            get => (ImageSource)GetValue(LeftMenuIconProperty);
            set => SetValue(LeftMenuIconProperty, value);
        }

        public ICommand RightMenuItemCommand
        {
            get => (ICommand)GetValue(RightMenuItemCommandProperty);
            set => SetValue(RightMenuItemCommandProperty, value);
        }

        public ImageSource RightMenuIcon
        {
            get => (ImageSource)GetValue(RightMenuIconProperty);
            set => SetValue(RightMenuIconProperty, value);
        }

        public bool BottomBorder
        {
            get => (bool)GetValue(BottomBorderProperty);
            set => SetValue(BottomBorderProperty, value);
        }
        public Color BottomBorderColor
        {
            get => (Color)GetValue(BottomBorderColorProperty);
            set => SetValue(BottomBorderColorProperty, value);
        }

        public int BottomBorderHeight
        {
            get => (int)GetValue(BottomBorderHeightProperty);
            set => SetValue(BottomBorderHeightProperty, value);
        }
    }
}