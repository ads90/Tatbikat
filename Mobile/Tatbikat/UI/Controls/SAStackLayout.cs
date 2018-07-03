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
            _stacklayout = new StackLayout() { InputTransparent = true, HorizontalOptions = LayoutOptions.End };

        }

        public static readonly BindableProperty ItemContentProperty = BindableProperty.Create("ItemContent", typeof(DataTemplate), typeof(SAStackLayout), default(DataTemplate));

        public DataTemplate ItemContent
        {
            get { return (DataTemplate)GetValue(ItemContentProperty); }
            set { SetValue(ItemContentProperty, value); }
        }
        private bool _isScrollable  ;

        public bool IsNotScrollable
        {
            get { return _isScrollable; }
            set
            {
                _isScrollable = value;
                if (value)
                {
                    Content = _stacklayout; 
                }
                else
                {
                    _scrollview = new ScrollView()
                    {
                        Content = _stacklayout,
                        Padding = 0
                    };
                    Content = _scrollview;
                }
            }
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
                _stacklayout.Orientation = value == ScrollOrientation.Horizontal ? StackOrientation.Horizontal : StackOrientation.Vertical;
                if (_scrollview != null)
                {
                    _scrollview.Orientation = value;
                }
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
            if (newValue == null)
            {
                return;
            }
            var element = (bindable as SAStackLayout);
            element._stacklayout.Children.Clear();
            foreach (object child in (newValue as IEnumerable))
            {
                View view = (View)element.ItemContent.CreateContent();
                view.InputTransparent = true;
                view.BindingContext = child;
                element._stacklayout.Children.Add(view);
                if (element.IsNotScrollable && element._stacklayout.Children.Count > 0)
                {
                    Device.BeginInvokeOnMainThread(async () => await element._scrollview.ScrollToAsync(element._stacklayout.Children.Last(), ScrollToPosition.MakeVisible, false));
                }
            }
        }
    }
}