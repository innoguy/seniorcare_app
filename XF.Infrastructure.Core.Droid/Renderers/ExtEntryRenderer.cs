using Android.Content;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using XF.Infrastructure.Core.Controls;
using XF.Infrastructure.Core.Droid.Renderers;
using Android.Graphics.Drawables;
using Android.Content.Res;
using Android.Text;
using Android.Views;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(ExtEntry), typeof(ExtEntryRenderer))]
namespace XF.Infrastructure.Core.Droid.Renderers
{
    public class ExtEntryRenderer : EntryRenderer
    {
        private const GravityFlags DefaultGravity = GravityFlags.CenterVertical;
        private ExtEntry _extEntry
        {
            get
            {
                return (ExtEntry)Element;
            }
        }

        public ExtEntryRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control == null || Element == null) return;
            UpdateBackground();
            UpdatePadding();
            UpdateTextAlighnment();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {



            if (e.PropertyName == ExtEntry.BorderWidthProperty.PropertyName ||
                e.PropertyName == ExtEntry.BorderColorProperty.PropertyName ||
                e.PropertyName == ExtEntry.CornerRadiusProperty.PropertyName ||
                e.PropertyName == ExtEntry.BackgroundColorProperty.PropertyName)
            {
                UpdateBackground();
                return;
            }
            else if (e.PropertyName == ExtEntry.LeftPaddingProperty.PropertyName ||
                e.PropertyName == ExtEntry.RightPaddingProperty.PropertyName)
            {
                UpdatePadding();
                return;
            }
            else if (e.PropertyName == Entry.HorizontalTextAlignmentProperty.PropertyName)
            {
                UpdateTextAlighnment();
                return;
            }
            base.OnElementPropertyChanged(sender, e);
        }

        private void UpdateBackground()
        {
            if (_extEntry == null || Control == null) return;
            Control.Background = GetBorderDrawable(_extEntry.BorderColor, _extEntry.BackColor, (float)_extEntry.BorderWidth, _extEntry.CornerRadius);
        }

        private GradientDrawable GetBorderDrawable(Color borderColor, Color backgroundColor, float borderWidth, int borderRadius)
        {
            GradientDrawable gd = new GradientDrawable();
            borderWidth = borderWidth > 0 ? borderWidth : 0;
            borderRadius = borderRadius > 0 ? borderRadius : 0;
            borderColor = borderColor != Color.Default ? borderColor : Color.Transparent;
            backgroundColor = backgroundColor != Color.Default ? backgroundColor : Color.Transparent;

            gd.SetColor(ColorStateList.ValueOf(backgroundColor.ToAndroid()));
            gd.SetStroke((int)borderWidth, ColorStateList.ValueOf(borderColor.ToAndroid()));
            gd.SetCornerRadius((float)borderRadius);
            return gd;
        }

        private void UpdatePadding()
        {
            if (_extEntry == null || Control == null) return;
            Control.SetPadding((int)Context.ToPixels(_extEntry.LeftPadding), 0, (int)Context.ToPixels(_extEntry.RightPadding), 0);
        }

        private void UpdateTextAlighnment()
        {
            if (_extEntry == null || Control == null) return;
            var gravity = DefaultGravity;
            switch (_extEntry.HorizontalTextAlignment)
            {
                case Xamarin.Forms.TextAlignment.Start:
                    gravity |= GravityFlags.Start;
                    break;
                case Xamarin.Forms.TextAlignment.Center:
                    gravity |= GravityFlags.CenterHorizontal;
                    break;
                case Xamarin.Forms.TextAlignment.End:
                    gravity |= GravityFlags.End;
                    break;
            }
            Control.Gravity = gravity;
        }
    }
}