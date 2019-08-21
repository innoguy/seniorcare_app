using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XF.Infrastructure.Core.Controls;
using XF.Infrastructure.Core.Droid.Renderers;

[assembly: ExportRenderer(typeof(ExtLabel), typeof(ExtLabelRenderer))]
namespace XF.Infrastructure.Core.Droid.Renderers
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

        public ExtLabelRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            if (Control == null || Element == null) return;
            if (_extLabel.MaxHeightRequest > 0) Control.SetMaxHeight(GetValueInPx(_extLabel.MaxHeightRequest));
            if (_extLabel.MaxWidthRequest > 0) Control.SetMaxWidth(GetValueInPx(_extLabel.MaxWidthRequest));
            UpdateLetterSpacing();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (base.Element == null) return;
            if (e.PropertyName == ExtLabel.MaxWidthRequestProperty.PropertyName)
            {
                if (_extLabel.MaxWidthRequest > 0) Control.SetMaxWidth(GetValueInPx(_extLabel.MaxWidthRequest));
            }

            if (e.PropertyName == ExtLabel.MaxHeightRequestProperty.PropertyName)
            {
                if (_extLabel.MaxHeightRequest > 0) Control.SetMaxHeight(GetValueInPx(_extLabel.MaxHeightRequest));
            }

            if (e.PropertyName == ExtLabel.LetterSpacingProperty.PropertyName)
            {
                UpdateLetterSpacing();
            }
        }

        private void UpdateLetterSpacing()
        {
            if (_extLabel != null && Control != null && _extLabel.LetterSpacing > 0)
            {
                Control.LetterSpacing = (float)_extLabel.LetterSpacing;
            }
        }

        private int GetValueInPx(int value)
        {
            return (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, value, Context.Resources.DisplayMetrics);
        }

    }
}