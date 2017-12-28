using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Tatbikat.Models.Enums;
using Xamarin.Forms;

namespace Tatbikat.UI.Converters
{
    public class SubCategoriesSelectionStateColorConverter : IValueConverter

    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            Color _categoryFlagColor = Color.Transparent;
            SubCategoriesStatus flag;
            Enum.TryParse(value.ToString(), out flag);
 
            switch (flag)
            {
                case SubCategoriesStatus.ManySelected:
                    _categoryFlagColor = (Color)App.Current.Resources["SilverColor"];

                    break;
                case SubCategoriesStatus.AllSelected:
                    _categoryFlagColor = (Color)App.Current.Resources["IdentityColor"];
                    break;
                default:
                    _categoryFlagColor = Color.Transparent;
                    break;
            }
            return _categoryFlagColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
