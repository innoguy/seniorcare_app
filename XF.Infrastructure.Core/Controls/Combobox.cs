using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XF.Infrastructure.Core.Controls
{
    public class Combobox : Picker
    {
        public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create("BorderWidth", typeof(int), typeof(Combobox), 0);
        public int BorderWidth
        {
            get { return (int)GetValue(BorderWidthProperty); }
            set { SetValue(BorderWidthProperty, value); }
        }

        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create("BorderColor", typeof(Color), typeof(Combobox), Color.Transparent);
        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }

        public static readonly BindableProperty ImageProperty = BindableProperty.Create(nameof(Image), typeof(ImageSource), typeof(Combobox), null);
        public static readonly BindableProperty BackColorProperty = BindableProperty.Create(nameof(BackColor), typeof(Color), typeof(Combobox), Color.Transparent);
        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(Combobox));

        public Color BackColor
        {
            get => (Color)GetValue(BackColorProperty);
            set => SetValue(BackColorProperty, value);
        }

        public ImageSource Image
        {
            get => (ImageSource)GetValue(ImageProperty);
            set => SetValue(ImageProperty, value);
        }

        public int CornerRadius
        {
            get => (int)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }


        public static readonly BindableProperty LeftPaddingProperty = BindableProperty.Create("LeftPadding", typeof(int), typeof(Combobox), 0, BindingMode.OneWay, null, null, null, null, null);
        public int LeftPadding
        {
            get { return (int)GetValue(LeftPaddingProperty); }
            set { SetValue(LeftPaddingProperty, value); }
        }

        public static readonly BindableProperty RightPaddingProperty = BindableProperty.Create("RightPadding", typeof(int), typeof(Combobox), 0, BindingMode.OneWay, null, null, null, null, null);
        public int RightPadding
        {
            get { return (int)GetValue(RightPaddingProperty); }
            set { SetValue(RightPaddingProperty, value); }
        }

        public Combobox()
        {

        }


    }
}
