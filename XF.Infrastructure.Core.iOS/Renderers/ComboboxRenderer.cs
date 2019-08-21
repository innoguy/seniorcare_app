using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XF.Infrastructure.Core.Controls;
using XF.Infrastructure.Core.iOS.Renderers;

[assembly: ExportRenderer(typeof(Combobox), typeof(ComboboxRenderer))]
namespace XF.Infrastructure.Core.iOS.Renderers
{
    public class ComboboxRenderer : PickerRenderer
    {
        private Combobox _combobox
        {
            get
            {
                return (Combobox)Element;
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            if (_combobox == null || Control == null) return;

            Control.BorderStyle = UITextBorderStyle.None;
            UpdateBackgroundColor();
            UpdateBorderWidth();
            UpdateBorderColor();
            UpdateBorderRadius();
            UpdateLeftPadding();
            UpdateRightPadding();
            UpdateDropdownImage();
            UpdateTextColor();
            Control.ClipsToBounds = true;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (this.Element == null || Control == null) return;
            if (e.PropertyName == Combobox.BackColorProperty.PropertyName)
            {
                UpdateBackgroundColor();
            }
            if (e.PropertyName == Combobox.BorderWidthProperty.PropertyName)
            {
                UpdateBorderWidth();
            }
            else if (e.PropertyName == Combobox.BorderColorProperty.PropertyName)
            {
                UpdateBorderColor();
            }
            else if (e.PropertyName == Combobox.CornerRadiusProperty.PropertyName)
            {
                UpdateBorderRadius();
            }
            else if (e.PropertyName == Combobox.LeftPaddingProperty.PropertyName)
            {
                UpdateLeftPadding();
            }
            else if (e.PropertyName == Combobox.RightPaddingProperty.PropertyName)
            {
                UpdateRightPadding();
            }
            else if (e.PropertyName == Combobox.ImageProperty.PropertyName)
            {
                UpdateDropdownImage();
            }

        }


        #region Methods


        private void UpdateDropdownImage()
        {
            if (_combobox == null) return;
            FileImageSource source = _combobox.Image as FileImageSource;
            if (source == null) return;
            var downarrow = UIImage.FromFile(source.File);
            Control.RightViewMode = UITextFieldViewMode.Always;
            var image = new UIImageView(downarrow);
            var viewPadding = new UIView(new CGRect(0, 0, image.Frame.Width + 18, image.Frame.Height));
            image.Center = viewPadding.Center;
            viewPadding.AddSubview(image);
            Control.RightView = viewPadding;
        }

        private void UpdateBackgroundColor()
        {
            if (_combobox == null) return;
            Control.BackgroundColor = _combobox.BackColor.ToUIColor();
        }

        private void UpdateBorderWidth()
        {
            if (_combobox == null) return;
            Control.Layer.BorderWidth = (nfloat)_combobox.BorderWidth;
        }

        private void UpdateBorderColor()
        {
            if (_combobox == null) return;
            Control.Layer.BorderColor = _combobox.BorderColor.ToUIColor().CGColor;
        }

        private void UpdateBorderRadius()
        {
            if (_combobox == null) return;
            Control.Layer.CornerRadius = (nfloat)_combobox.CornerRadius;
        }

        private void UpdateLeftPadding()
        {
            if (_combobox == null) return;
            var leftPaddingView = new UIView(new CGRect(0, 0, _combobox.LeftPadding, 0));
            Control.LeftView = leftPaddingView;
            Control.LeftViewMode = UITextFieldViewMode.Always;
        }

        private void UpdateRightPadding()
        {
            if (_combobox == null) return;
            var rightPaddingView = new UIView(new CGRect(0, 0, _combobox.RightPadding, 0));
            Control.RightView = rightPaddingView;
            Control.RightViewMode = UITextFieldViewMode.Always;
        }
        #endregion  

    }
}