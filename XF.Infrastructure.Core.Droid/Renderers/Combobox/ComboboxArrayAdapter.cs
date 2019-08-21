using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace XF.Infrastructure.Core.Droid.Renderers.Combobox
{
    public class ComboboxArrayAdapter : ArrayAdapter
    {
        public Android.Graphics.Color TextColor { get; set; }
        public Typeface Font { get; set; }

        public Android.Graphics.Color DropdownTextColor { get; set; }
        public Android.Graphics.Color DropdownBackgroundColor { get; set; }




        public ComboboxArrayAdapter(Context context, int textViewResourceId) : base(context, textViewResourceId)
        {
            TextColor = Android.Graphics.Color.Transparent;
        }

        public ComboboxArrayAdapter(Context context, int textViewResourceId, System.Collections.IList items) : base(context, textViewResourceId, items)
        {

        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            AppCompatTextView view = base.GetView(position, convertView, parent) as AppCompatTextView;
            if (view == null) return base.GetView(position, convertView, parent);

            view.SetTextColor(ColorStateList.ValueOf(TextColor));
            if (Font != null)
            {
                view.SetTypeface(Font, TypefaceStyle.Normal);
            }
            return view;


        }
        public override View GetDropDownView(int position, View convertView, ViewGroup parent)
        {
            AppCompatCheckedTextView view = base.GetDropDownView(position, convertView, parent) as AppCompatCheckedTextView;
            if (view == null) return base.GetDropDownView(position, convertView, parent);
            if (Font != null)
            {
                view.SetTypeface(Font, TypefaceStyle.Normal);
            }
            view.SetTextColor(ColorStateList.ValueOf(DropdownTextColor));
            view.SetBackgroundColor(DropdownBackgroundColor);
            return view;
        }
    }
}