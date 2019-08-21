using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Windows.Input;

namespace XF.Infrastructure.Core.Controls
{
    public class ContentButton : Frame
    {
        public static readonly BindableProperty CommandProperty = BindableProperty.Create("Command", typeof(ICommand), typeof(ContentButton), null, BindingMode.OneWay);

        /// <summary>Backing store for the CommandParameter bindable property.</summary>
        /// <remarks />
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create("CommandParameter", typeof(object), typeof(ContentButton), null, BindingMode.OneWay);

        private readonly TapGestureRecognizer _gesture;
        public ContentButton()
        { 
            this.HasShadow = false;
            Padding = new Thickness(0);
            _gesture = new TapGestureRecognizer();
            _gesture.Command = new Command(() =>
            {
                if (!this.IsEnabled)
                {
                    return;
                }
                if (Command?.CanExecute(CommandParameter ?? this) == true)
                {
                    Command.Execute(CommandParameter ?? this);
                }
            });
            GestureRecognizers.Add(_gesture);
        }

        public ICommand Command
        {
            get
            {
                return (ICommand)base.GetValue(ContentButton.CommandProperty);
            }
            set
            {
                base.SetValue(ContentButton.CommandProperty, value);
            }
        }

        public object CommandParameter
        {
            get
            {
                return base.GetValue(ContentButton.CommandParameterProperty);
            }
            set
            {
                base.SetValue(ContentButton.CommandParameterProperty, value);
            }
        }
    }
}
