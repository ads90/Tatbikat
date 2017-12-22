using System;
using Tatbikat.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: Xamarin.Forms.ExportRenderer(typeof(ContentPage), typeof(ContentPageRenderer))]
namespace Tatbikat.iOS.Renderers
{
    public class ContentPageRenderer: PageRenderer
    {

        public override void ViewWillAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            var navctrl = this.ViewController.NavigationController;
            if (navctrl != null)
            {
                navctrl.InteractivePopGestureRecognizer.Delegate = new UIGestureRecognizerDelegate();
                navctrl.InteractivePopGestureRecognizer.Enabled = true;
            }
        }
    }
}
