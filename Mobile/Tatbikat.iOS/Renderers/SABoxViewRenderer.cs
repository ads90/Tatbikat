using CoreAnimation;
using CoreGraphics;
using System;
using System.ComponentModel;
using System.Drawing;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Tatbikat.iOS.Renderers;
using Tatbikat.UI.Controls;

[assembly: ExportRenderer(typeof(Tatbikat.UI.Controls.SABoxView), typeof(SABoxViewRenderer))]
namespace Tatbikat.iOS.Renderers
{
    public class SABoxViewRenderer : BoxRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<BoxView> e)
        {
            base.OnElementChanged(e);

            if (Element != null)
            {
                Layer.MasksToBounds = true;
                e.NewElement.SizeChanged += (sender, er) => { UpdateCornerRadius(Element as SABoxView); };
                UpdateCornerRadius(Element as SABoxView);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == SABoxView.CornerRadiusProperty.PropertyName)
            {
                UpdateCornerRadius(Element as SABoxView);
            }
        }

        void UpdateCornerRadius(SABoxView box)
        {
            if (box.IsCircular)
            {
                Layer.CornerRadius = (nfloat)box.Width / 2;
            }
            else
            {
                Layer.CornerRadius = (nfloat)box.CornerRadius;
            }
        }
    }
}