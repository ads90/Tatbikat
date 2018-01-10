using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Tatbikat.UI.Controls
{
    [ContentProperty("ItemContent")]
    public class SAStackLayout : ContentView
    {
        private ScrollView _scrollview;
        private StackLayout _stacklayout;
        public SAStackLayout()
        {

            //_stacklayout = new StackLayout() { InputTransparent=true,  HorizontalOptions = LayoutOptions.End };

            _scrollview = new ScrollView()
            {
                 InputTransparent = false,
                //Content = _stacklayout,
                Padding = 0
            };
            Content = _scrollview;
            this.InputTransparent = false;
            (Content as ScrollView).InputTransparent = false;
            //this.InputTransparent = false;
            //var x = new TapGestureRecognizer();
            //x.Tapped += X_Tapped;
            //Content.GestureRecognizers.Add(x);
            Content.BindingContextChanged += (o,e) => { (o as View).InputTransparent = true; };
            var y = new TapGestureRecognizer();
            y.Tapped += Y_Tapped;
            Content.GestureRecognizers.Add(y);
            this.GestureRecognizers.Add(y);
          
            _scrollview.GestureRecognizers.Add(y);
           // this.GestureRecognizers.Add(y);
            ////
        }
        internal void RefreshTappingGesture()
        {
           
            //{
            //    Command = Command 
            //});
        }

        private void X_Tapped(object sender, EventArgs e)
        {
            
        }
        private void Y_Tapped(object sender, EventArgs e)
        {

        }
        public static readonly BindableProperty CommandProperty =
         BindableProperty.Create("Command", typeof(Command), typeof(TappingArea), null, propertyChanged: OnCommandPropChanged);

        private static void OnCommandPropChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null)
            {
                (bindable as SAStackLayout).RefreshTappingGesture();
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
        public static readonly BindableProperty ItemContentProperty = BindableProperty.Create("ItemContent", typeof(DataTemplate), typeof(SAStackLayout), default(ElementTemplate));

        public DataTemplate ItemContent
        {
            get { return (DataTemplate)GetValue(ItemContentProperty); }
            set { SetValue(ItemContentProperty, value); }
        }


        private ScrollOrientation _scrollOrientation;
        public ScrollOrientation Orientation
        {
            get
            {
                return _scrollOrientation;
            }
            set
            {
                _scrollOrientation = value;
                //_stacklayout.Orientation = value == ScrollOrientation.Horizontal ? StackOrientation.Horizontal : StackOrientation.Vertical;
                //_scrollview.Orientation = value;
            }
        }

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create("ItemsSource", typeof(IEnumerable), typeof(SAStackLayout), default(IEnumerable), propertyChanged: GetEnumerator);
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        private static void GetEnumerator(BindableObject bindable, object oldValue, object newValue)
        {
            //if (newValue == null)
            //{
            //    return;
            //}
            //var element = (bindable as SAStackLayout);
            //element._stacklayout.Children.Clear();
            //foreach (object child in (newValue as IEnumerable))
            //{
            //    View view = (View)element.ItemContent.CreateContent();
             
            //    view.BindingContext = child;
            //    element._stacklayout.Children.Add(view);
            //    if (element._stacklayout.Children.Count > 0)
            //    {
            //        Device.BeginInvokeOnMainThread(async () => await element._scrollview.ScrollToAsync(element._stacklayout.Children.Last(), ScrollToPosition.MakeVisible, false));
            //    }
            //}
        }
    }
}