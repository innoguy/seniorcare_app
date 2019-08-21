using CoreGraphics;
using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XF.Infrastructure.Core.Controls;
using XF.Infrastructure.Core.iOS.Renderers;

[assembly: ExportRenderer(typeof(ExtEntry), typeof(ExtEntryRenderer))]

namespace XF.Infrastructure.Core.iOS.Renderers
{
    public class ExtEntryRenderer : EntryRenderer
    {
        private ExtEntry _extEntry
        {
            get
            {
                return (ExtEntry)Element;
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control == null || Element == null) return;
            Control.BorderStyle = UITextBorderStyle.None;
            UpdateBackgroundColor();
            UpdateBorderWidth();
            UpdateBorderColor();
            UpdateBorderRadius();
            UpdateLeftPadding();
            UpdateRightPadding();
            Control.ClipsToBounds = true;
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (this.Element == null) return;
            if (e.PropertyName == ExtEntry.BackColorProperty.PropertyName)
            {
                UpdateBackgroundColor();
            }
            if (e.PropertyName == ExtEntry.BorderWidthProperty.PropertyName)
            {
                UpdateBorderWidth();
            }
            else if (e.PropertyName == ExtEntry.BorderColorProperty.PropertyName)
            {
                UpdateBorderColor();
            }
            else if (e.PropertyName == ExtEntry.CornerRadiusProperty.PropertyName)
            {
                UpdateBorderRadius();
            }
            else if (e.PropertyName == ExtEntry.LeftPaddingProperty.PropertyName)
            {
                UpdateLeftPadding();
            }
            else if (e.PropertyName == ExtEntry.RightPaddingProperty.PropertyName)
            {
                UpdateRightPadding();
            }
        }


        private void UpdateBackgroundColor()
        {
            if (_extEntry == null) return;
            Control.BackgroundColor = _extEntry.BackColor.ToUIColor();
        }

        private void UpdateBorderWidth()
        {
            if (_extEntry == null) return;
            Control.Layer.BorderWidth = (nfloat)_extEntry.BorderWidth;
        }

        private void UpdateBorderColor()
        {
            if (_extEntry == null) return;
            Control.Layer.BorderColor = _extEntry.BorderColor.ToUIColor().CGColor;
        }

        private void UpdateBorderRadius()
        {
            if (_extEntry == null) return;
            Control.Layer.CornerRadius = (nfloat)_extEntry.CornerRadius;
        }

        private void UpdateLeftPadding()
        {
            if (_extEntry == null) return;
            var leftPaddingView = new UIView(new CGRect(0, 0, _extEntry.LeftPadding, 0));
            Control.LeftView = leftPaddingView;
            Control.LeftViewMode = UITextFieldViewMode.Always;
        }

        private void UpdateRightPadding()
        {
            if (_extEntry == null) return;
            var rightPaddingView = new UIView(new CGRect(0, 0, _extEntry.RightPadding, 0));
            Control.RightView = rightPaddingView;
            Control.RightViewMode = UITextFieldViewMode.Always;
        }
    }
}