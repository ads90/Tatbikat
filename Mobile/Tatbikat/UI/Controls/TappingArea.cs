using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Tatbikat.UI.Controls
{
    public class TappingArea : Grid
    { 
        public TappingArea()
        {
            RefreshTappingGesture();
        }

        internal void RefreshTappingGesture()
        {
            GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = Command,
                CommandParameter = CommandParameters
            });
        }

        public static readonly BindableProperty CommandProperty =
                BindableProperty.Create("Command", typeof(Command), typeof(TappingArea), null, propertyChanged: OnCommandPropChanged);

        private static void OnCommandPropChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null)
            {
                (bindable as TappingArea).RefreshTappingGesture();
            }
        }

        public Command Command
        {
            get { return (Command)GetValue(CommandProperty); }
            set
            {
                SetValue(CommandProperty, value);
            }
        }



        public static readonly BindableProperty CommandParametersProperty =
                BindableProperty.Create("CommandParameters", typeof(object), typeof(TappingArea), null, propertyChanged: OnCommandParamsPropChanged);

        private static void OnCommandParamsPropChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null)
            {
                (bindable as TappingArea).RefreshTappingGesture();
            }
        }

        public object CommandParameters
        {
            get { return GetValue(CommandParametersProperty); }
            set
            {
                SetValue(CommandParametersProperty, value);
            }
        }
    }
}
