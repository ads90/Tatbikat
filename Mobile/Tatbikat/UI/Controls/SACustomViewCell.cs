using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Tatbikat.UI.Controls
{
    public class SACustomViewCell : ViewCell
    {

        public static readonly BindableProperty SelectedBackgroundColorProperty =
          BindableProperty.Create("SelectedBackgroundColor", typeof(Color), typeof(SACustomViewCell), Color.Default);

        /// <summary>
        /// Gets or sets the SelectedBackgroundColor.
        /// </summary>
        public Color SelectedBackgroundColor
        {
            get { return (Color)GetValue(SelectedBackgroundColorProperty); }
            set { SetValue(SelectedBackgroundColorProperty, value); }
        }

    }
}