//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Runtime;
//using Android.Views;
//using Android.Widget;
//using Tatbikat.Droid.Renderers;
//using Tatbikat.UI.Controls;
//using Xamarin.Forms;
//using Xamarin.Forms.Platform.Android;

//[assembly: ExportRenderer(typeof(Xamarin.Forms.Grid), typeof(SAVisualElementRenderer))]

//namespace Tatbikat.Droid.Renderers
//{
//   public class SAVisualElementRenderer : ViewRenderer 
//    {
//        public SAVisualElementRenderer(Context context) : base(context)
//        {
//        }
//        public override bool DispatchTouchEvent(MotionEvent e)
//        {
//            return base.DispatchTouchEvent(e);
//        }
//        public override bool OnTouchEvent(MotionEvent e)
//        {
//            if (Element.InputTransparent)
//            {
//                return false;
//            }
//            return base.OnTouchEvent(e);
//        }
//    }
//}