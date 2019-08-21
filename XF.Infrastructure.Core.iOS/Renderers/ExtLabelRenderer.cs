using Foundation;
using System;
using System.ComponentModel;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XF.Infrastructure.Core.Controls;
using XF.Infrastructure.Core.iOS.Renderers;

[assembly: ExportRenderer(typeof(ExtLabel), typeof(ExtLabelRenderer))]
namespace XF.Infrastructure.Core.iOS.Renderers
{
    public class ExtLabelRenderer : LabelRenderer
    {
        private ExtLabel _extLabel
        {
            get
            {
                return (ExtLabel)Element;
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            if (Control == null || Element == null) return;
            if (_extLabel.MaxWidthRequest > 0) Control.PreferredMaxLayoutWidth = _extLabel.MaxWidthRequest;
            UpdateLetterSpacing();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (base.Element == null) return;
            if (e.PropertyName == ExtLabel.MaxWidthRequestProperty.PropertyName)
            {
                if (_extLabel.MaxWidthRequest > 0) Control.PreferredMaxLayoutWidth = _extLabel.MaxWidthRequest;
            }
            UpdateLetterSpacing();

        }

        private void UpdateLetterSpacing()
        {
            if (_extLabel != null && Control != null && _extLabel.LetterSpacing > 0)
            {
                Control.AttributedText = new NSAttributedString(Element.Text, new UIStringAttributes
                {

                    Font = UIFont.SystemFontOfSize((nfloat)Element.FontSize),
                    ForegroundColor = _extLabel.TextColor.ToUIColor(),
                    KerningAdjustment = _extLabel.LetterSpacing * 10
                });
            }
        }


    }
}