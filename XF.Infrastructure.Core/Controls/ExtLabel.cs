using Xamarin.Forms;

namespace XF.Infrastructure.Core.Controls
{
    public class ExtLabel : Label
    {
        public static readonly BindableProperty MaxWidthRequestProperty = BindableProperty.Create("MaxWidthRequest", typeof(int), typeof(ExtLabel), null, BindingMode.OneWay, null, null, null, null, null);
        public int MaxWidthRequest
        {
            get
            {
                return (int)base.GetValue(ExtLabel.MaxWidthRequestProperty);
            }
            set
            {
                base.SetValue(ExtLabel.MaxWidthRequestProperty, value);
            }
        }


        public static readonly BindableProperty MaxHeightRequestProperty = BindableProperty.Create("MaxHeightRequest", typeof(int), typeof(ExtLabel), null, BindingMode.OneWay, null, null, null, null, null);
        public int MaxHeightRequest
        {
            get
            {
                return (int)base.GetValue(ExtLabel.MaxHeightRequestProperty);
            }
            set
            {
                base.SetValue(ExtLabel.MaxHeightRequestProperty, value);
            }
        }


        public static readonly BindableProperty LetterSpacingProperty = BindableProperty.Create("LetterSpacing", typeof(float), typeof(ExtLabel), 0f, BindingMode.OneWay, null, null, null, null, null);
        public float LetterSpacing
        {
            get
            {
                return (float)base.GetValue(ExtLabel.LetterSpacingProperty);
            }
            set
            {
                base.SetValue(ExtLabel.LetterSpacingProperty, value);
            }
        }



    }
}
