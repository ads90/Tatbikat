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
        public override void WillDrawLayer(CALayer layer)
        {
            UpdateCornerRadius(Element as SABoxView);
            base.WillDrawLayer(layer);
        }

        void UpdateCornerRadius(SABoxView box)
        {
            if (box.IsCircular)
            {
                Layer.CornerRadius = (nfloat)box.Width / 2;
                UIBezierPath maskPath = UIBezierPath.FromRoundedRect(this.Bounds, UIRectCorner.AllCorners, new CGSize(Layer.CornerRadius, Layer.CornerRadius));
                CAShapeLayer maskLayer = new CAShapeLayer();
                maskLayer.Frame = this.Bounds;
                maskLayer.Path = maskPath.CGPath;
                NativeView.Layer.Mask = maskLayer;
            }
            else
            {
                UIBezierPath maskPath = UIBezierPath.FromRoundedRect(this.Bounds,UIRectCorner.AllCorners , new CGSize(box.CornerRadius, box.CornerRadius));
                CAShapeLayer maskLayer = new CAShapeLayer();
                maskLayer.Frame = this.Bounds;
                maskLayer.Path = maskPath.CGPath;
                NativeView.Layer.Mask = maskLayer;
            }
        }
    }
}