using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Tatbikat.UI.Controls
{
    [ContentProperty("ItemContent")]
    public class SAFlexLayout : ContentView
    {
        //private ScrollView _scrollview;
        private FlexLayout _stacklayout;
        public SAFlexLayout()
        { 
            _stacklayout = new FlexLayout() {Position=FlexPosition.Relative,IsClippedToBounds=true, VerticalOptions=LayoutOptions.Start, JustifyContent=FlexJustify.SpaceBetween,AlignContent=FlexAlignContent.Start,FlowDirection=FlowDirection.RightToLeft,AlignItems=FlexAlignItems.Start, Direction=FlexDirection.Row,Wrap=FlexWrap.Wrap, InputTransparent = true}; 
           
            Content = _stacklayout;
            
        }
        
        public static readonly BindableProperty ItemContentProperty = BindableProperty.Create("ItemContent", typeof(DataTemplate), typeof(SAStackLayout), default(DataTemplate));

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
              //  _scrollview.Orientation = value;
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
            var element = (bindable as SAFlexLayout);
            element._stacklayout.Children.Clear();
            foreach (object child in (newValue as IEnumerable))
            {
                View view = (View)element.ItemContent.CreateContent();
                view.InputTransparent = true;
                view.BindingContext = child;
                element._stacklayout.Children.Add(view);
               
            }
            element.InvalidateLayout();
        }
    }
}