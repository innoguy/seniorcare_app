using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V4.Content;
using Android.Widget;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XF.Infrastructure.Core.Controls;
using XF.Infrastructure.Core.Droid.Extensions;
using XF.Infrastructure.Core.Droid.Renderers.Combobox;

[assembly: ExportRenderer(typeof(Combobox), typeof(ComboboxRenderer))]
namespace XF.Infrastructure.Core.Droid.Renderers.Combobox
{
    public class ComboboxRenderer : ViewRenderer<XF.Infrastructure.Core.Controls.Combobox, Spinner>
    {


        private Controls.Combobox _combobox
        {
            get
            {
                return (Controls.Combobox)Element;
            }
        }

        private bool _isDisposed;
        private List<string> _items;
        private ComboboxArrayAdapter _adapter;



        public ComboboxRenderer(Context context) : base(context)
        {
            base.AutoPackage = false;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Controls.Combobox> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                var spinner = new Android.Widget.Spinner(base.Context);
                spinner.Prompt = base.Element.Title;
                base.SetNativeControl(spinner);
                _items = new List<string>();
                LoadItems();
                UpdateBackground();
                UpdateFont();
                UpdateCharacterSpacing();
                UpdatePadding();
                UpdateTextColor();
            }
        }


        private void LoadItems()
        {

            if (base.Control == null || _combobox == null || _combobox.ItemsSource == null)
            {
                return;
            }


            _items.Clear();
            foreach (var item in _combobox.ItemsSource)
            {
                string itemValue = string.Empty;
                if (_combobox.ItemDisplayBinding == null)
                {
                    itemValue = item.ToString();
                }
                else
                {
                    itemValue = item.GetPropValue<string>(_combobox.ItemDisplayBinding.ToString());
                }

                _items.Add(itemValue);
            }

            _adapter = new ComboboxArrayAdapter(base.Context, Resource.Drawable.spinner_item, _items);
            _adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            base.Control.Adapter = _adapter;
            base.Control.ItemSelected += spinner_ItemSelected;

        }


        void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            base.Element.SelectedIndex = (sender as Spinner).SelectedItemPosition;
        }

        private void UpdateSelectedItem()
        {
            if (base.Control == null || base.Element == null || base.Element.SelectedItem == null)
            {
                return;
            }

            string selectedText = base.Element.ToString();
            if (_combobox.ItemDisplayBinding != null)
            {
                selectedText = _combobox.SelectedItem.GetPropValue<string>(_combobox.ItemDisplayBinding.ToString());
            }


            int index = base.Element.Items.IndexOf(selectedText);
            base.Control.SetSelection(index, false);
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == Controls.Combobox.ItemsSourceProperty.PropertyName)
            {
                LoadItems();
            }

            if (e.PropertyName == Controls.Combobox.SelectedItemProperty.PropertyName)
            {
                UpdateSelectedItem();
            }

            if (e.PropertyName == Controls.Combobox.BorderWidthProperty.PropertyName ||
                e.PropertyName == Controls.Combobox.BorderColorProperty.PropertyName ||
                e.PropertyName == Controls.Combobox.BackColorProperty.PropertyName ||
                e.PropertyName == Controls.Combobox.CornerRadiusProperty.PropertyName ||
                e.PropertyName == Controls.Combobox.BackgroundColorProperty.PropertyName ||
                e.PropertyName == Controls.Combobox.ImageProperty.PropertyName)
            {
                UpdateBackground();
            }

            if (e.PropertyName == ExtEntry.LeftPaddingProperty.PropertyName ||
                e.PropertyName == ExtEntry.RightPaddingProperty.PropertyName)
            {
                UpdatePadding();
            }

            if (e.PropertyName == ExtEntry.TextColorProperty.PropertyName)
            {
                UpdateTextColor();
            }



        }



        #region CustomMethods

        private void UpdateTextColor()
        {
            if (Control == null || _combobox == null || _adapter == null) return;
            _adapter.TextColor = _combobox.TextColor.ToAndroid();
            _adapter.DropdownTextColor = _combobox.TextColor.ToAndroid();
        }

        private void UpdateBackground()
        {
            if (_combobox == null || Control == null) return;
            Control.Background = GetBorderDrawable(_combobox.BorderColor, _combobox.BackColor, (float)_combobox.BorderWidth, _combobox.CornerRadius, _combobox.Image);
            if (_adapter != null)
            {
                _adapter.DropdownBackgroundColor = _combobox.BackColor.ToAndroid();
            }
        }

        private LayerDrawable GetBorderDrawable(Xamarin.Forms.Color borderColor, Xamarin.Forms.Color backgroundColor, float borderWidth, int borderRadius, ImageSource imageSource)
        {
            GradientDrawable gd = new GradientDrawable();
            borderWidth = borderWidth > 0 ? borderWidth : 0;
            borderRadius = borderRadius > 0 ? borderRadius : 0;
            borderColor = borderColor != Xamarin.Forms.Color.Default ? borderColor : Xamarin.Forms.Color.Transparent;
            backgroundColor = backgroundColor != Xamarin.Forms.Color.Default ? backgroundColor : Xamarin.Forms.Color.Transparent;

            gd.SetColor(ColorStateList.ValueOf(backgroundColor.ToAndroid()));
            gd.SetStroke((int)borderWidth, ColorStateList.ValueOf(borderColor.ToAndroid()));
            gd.SetCornerRadius((float)borderRadius);
            Drawable[] layers = { gd, GetImageDrawable(imageSource as FileImageSource) };
            LayerDrawable layerDrawable = new LayerDrawable(layers);
            layerDrawable.SetLayerInset(0, 0, 0, 0, 0);
            layerDrawable.SetLayerInset(1, 0, 0, 18, 0);
            return layerDrawable;
        }

        private BitmapDrawable GetImageDrawable(FileImageSource imageSource)
        {
            if (imageSource == null) return null;
            if (imageSource.IsEmpty) return null;
            int resId = Resources.GetIdentifier(imageSource.File.Replace("Images/", "").Replace(".png", "").Trim(), "drawable", Context.PackageName);
            var drawable = ContextCompat.GetDrawable(Context, resId);
            var bitmap = ((BitmapDrawable)drawable).Bitmap;
            var result = new BitmapDrawable(Resources, Bitmap.CreateBitmap(bitmap)); //  Bitmap.CreateScaledBitmap(bitmap, 70, 70, true));
            result.Gravity = Android.Views.GravityFlags.Right;
            return result;
        }


        private void UpdateFont()
        {
            if (Control == null || _combobox == null || _adapter == null) return;
            _adapter.Font = Typeface.CreateFromAsset(base.Context.Assets, "Roboto-Regular.ttf");
        }

        private void UpdateCharacterSpacing()
        {
            //TODO
        }

        private void UpdatePadding()
        {
            if (_combobox == null || Control == null) return;
            Control.SetPadding((int)Context.ToPixels(_combobox.LeftPadding), 0, (int)Context.ToPixels(_combobox.RightPadding), 0);
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing && !this._isDisposed)
            {
                this._isDisposed = true;

            }

            _items = null;
            _adapter = null;
            base.Control.ItemSelected -= spinner_ItemSelected;
            base.Dispose(disposing);
        }
    }
}