using System;
using Xamarin.Forms;

namespace Tatbikat.UI.Controls
{
    public class SABoxView : BoxView
    {
        public static readonly BindableProperty CornerRadiusProperty =
         BindableProperty.Create("CornerRadius", typeof(double), typeof(SABoxView), 0.0);

        /// <summary>
        /// Gets or sets the corner radius.
        /// </summary>
        public double CornerRadius
        {
            get { return (double)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public bool IsCircular { get; set; }
    }
}
