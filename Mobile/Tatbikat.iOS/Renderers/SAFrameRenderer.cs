using System;
using System.Drawing;
using CoreAnimation;
using Tatbikat.iOS.Renderers;
using Tatbikat.UI.Controls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SAFrame), typeof(SAFrameRenderer))]
namespace Tatbikat.iOS.Renderers
{
    public class SAFrameRenderer: FrameRenderer
    {
        private SAFrame myFrame;
        private void UpdateMask()
        {
            var layer = new CALayer();
            layer.Frame = new RectangleF(0, 0, (float)myFrame.Width, (float)myFrame.Height);
            layer.CornerRadius = myFrame.CornerRadius;

            layer.BackgroundColor = UIColor.White.CGColor;
            NativeView.Layer.Mask = layer;

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            if (Element == null)
            {
                return;
            }
            if (e.OldElement != e.NewElement)
            {
                e.NewElement.SizeChanged += NewElement_SizeChanged;
            }

            FixShadow();
        }

        private void NewElement_SizeChanged(object sender, EventArgs e)
        {
            myFrame = sender as SAFrame;
            //update mask when get the element size
            Frame frame = sender as Frame;

            if (frame != null && frame.Height > -1 && frame.Width > -1)
            {
                frame.SizeChanged -= NewElement_SizeChanged;
            }
            UpdateMask();
        }
        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
        
            if (e.PropertyName == SAFrame.ShadowOpacityProperty.PropertyName)
            {
                FixShadow();
            }

        }

        private void FixShadow()
        {
            SAFrame saFrame = (Element as SAFrame);
            if (saFrame.HasShadow)
            {

                Layer.ShadowRadius = 5;
                Layer.ShadowColor = saFrame.ShadowColor.ToCGColor();
                Layer.ShadowOpacity = saFrame.ShadowOpacity;
                Layer.ShadowOffset = new System.Drawing.SizeF();
                Layer.BorderColor = saFrame.BorderColor.ToCGColor();
                Layer.BorderWidth = saFrame.BorderWidth;

            }
        }

    }
}
