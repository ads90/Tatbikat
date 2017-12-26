using System;
using Xamarin.Forms;

namespace Tatbikat.UI.Controls
{
    public class SAFrame : Frame
    {
        public static readonly BindableProperty ShadowColorProperty =
        BindableProperty.Create("ShadowColor", typeof(Color), typeof(SAFrame), Color.Gray);

        public Color ShadowColor
        {
            get { return (Color)GetValue(ShadowColorProperty); }
            set { SetValue(ShadowColorProperty, value); }
        }


        public static readonly BindableProperty ShadowOpacityProperty =
        BindableProperty.Create("ShadowOpacity", typeof(float), typeof(SAFrame), 0f);

        public float ShadowOpacity
        {
            get { return (float)GetValue(ShadowOpacityProperty); }
            set { SetValue(ShadowOpacityProperty, value); }
        }


        public static readonly BindableProperty BorderWidthProperty =
  BindableProperty.Create("BorderWidth", typeof(float), typeof(SAFrame), 0f);

        public float BorderWidth
        {
            get { return (float)GetValue(BorderWidthProperty); }
            set { SetValue(BorderWidthProperty, value); }
        }


        public static readonly BindableProperty BorderColorProperty =
        BindableProperty.Create("BorderColor", typeof(Color), typeof(SAFrame), Color.Gray);

        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }

    }
}
