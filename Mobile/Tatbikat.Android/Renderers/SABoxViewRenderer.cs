using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;
using Shop.Droid.UIRenderers;
using Tatbikat.UI.Controls;
using System.ComponentModel;
using System;

[assembly: ExportRenderer(typeof(Tatbikat.UI.Controls.SABoxView), typeof(SABoxViewRenderer))]
namespace Shop.Droid.UIRenderers
{
    public class SABoxViewRenderer : BoxRenderer
    {
        public SABoxViewRenderer(Android.Content.Context context) : base(context)
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<BoxView> e)
        {

            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                SetWillNotDraw(false);

                e.NewElement.SizeChanged += (sender, er) =>
                {
                    var box = e.NewElement as SABoxView;

                    if (box.IsCircular)
                    {
                        bool takeWidth = box.Height <= box.Width;

                        box.HeightRequest = takeWidth ? box.Width : box.Height;
                        box.WidthRequest = takeWidth ? box.Width : box.Height;
                    }

                    Invalidate();
                };

                Invalidate();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (Element == null || e == null) { return; }
            if (e.PropertyName == SABoxView.CornerRadiusProperty.PropertyName)
            {
                Invalidate();
            }
        }
        public override void Draw(Canvas canvas)
        {

            var box = Element as SABoxView;
            var cRadiou = (Math.Abs(box.CornerRadius - -9) < 1) ? box.Width / 2 : box.CornerRadius;

            if (box.IsCircular)
            {
                bool takeWidth = box.Height <= box.Width;

                box.HeightRequest = takeWidth ? box.Width : box.Height;
                box.WidthRequest = takeWidth ? box.Width : box.Height;

                cRadiou = box.WidthRequest / 2;
                Invalidate();
            }


            var rect = new Rect();

            var paint = new Paint()
            {
                Color = box.BackgroundColor.ToAndroid(),
                AntiAlias = true,
            };

            GetDrawingRect(rect);
            var radius = (float)(rect.Width() / box.Width * cRadiou); 
           
            canvas.DrawRoundRect(new RectF(rect), radius, radius, paint); 
        }
    }
}