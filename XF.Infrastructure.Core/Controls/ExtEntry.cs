using Xamarin.Forms;

namespace XF.Infrastructure.Core.Controls
{
    public class ExtEntry : Entry
    {
        public static readonly BindableProperty BackColorProperty = BindableProperty.Create(nameof(BackColor), typeof(Color), typeof(ExtEntry), Color.Transparent);
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(ExtEntry), Color.Transparent);
        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(ExtEntry));
        public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(nameof(BorderWidth), typeof(int), typeof(ExtEntry), 0);
        public static readonly BindableProperty LeftPaddingProperty = BindableProperty.Create(nameof(LeftPadding), typeof(int), typeof(ExtEntry), 0);
        public static readonly BindableProperty RightPaddingProperty = BindableProperty.Create(nameof(RightPadding), typeof(int), typeof(ExtEntry), 0);

        public Color BackColor
        {
            get => (Color)GetValue(BackColorProperty);
            set => SetValue(BackColorProperty, value);
        }

        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public int CornerRadius
        {
            get => (int)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }
        
        public int BorderWidth
        {
            get => (int)GetValue(BorderWidthProperty);
            set => SetValue(BorderWidthProperty, value);
        }
        
        public int LeftPadding
        {
            get => (int)GetValue(LeftPaddingProperty);
            set => SetValue(LeftPaddingProperty, value);
        }
        
        public int RightPadding
        {
            get => (int)GetValue(RightPaddingProperty);
            set => SetValue(RightPaddingProperty, value);
        }
    }
}
