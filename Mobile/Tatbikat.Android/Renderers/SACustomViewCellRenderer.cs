using System.ComponentModel;
using Android.Graphics.Drawables;
using Tatbikat.Droid.Renderers;
using Tatbikat.UI.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SACustomViewCell), typeof(SACustomViewCellRenderer))]

namespace Tatbikat.Droid.Renderers
{
    class SACustomViewCellRenderer : ViewCellRenderer
    {
        private Android.Views.View cellCore;
        private Drawable unselectedBackground;
        private Android.Graphics.Color selectedBackground;
        private bool selected;

        protected override Android.Views.View GetCellCore(Cell item, Android.Views.View convertView, Android.Views.ViewGroup parent, Android.Content.Context context)
        {
            Android.Views.View cell = base.GetCellCore(item, convertView, parent, context);
            // Save original background to rollback to it when not selected,
            // We assume that no cells will be selected on creation.
            var view = item as SACustomViewCell;
            selectedBackground = view.SelectedBackgroundColor.ToAndroid();
            selected = false;
            unselectedBackground = cell.Background;
            cellCore = cell;
            return cell;
        }
        protected override void OnCellPropertyChanged(object sender, PropertyChangedEventArgs args)
        { 
            base.OnCellPropertyChanged(sender, args);
            if (args.PropertyName == "")
            {

            }
            if (args.PropertyName == "IsSelected")
            {
                // I had to create a property to track the selection because cellCore.Selected is always false.
                // Toggle selection
                selected = !selected;

                if (selected)
                {
                    if (selectedBackground == Color.Transparent.ToAndroid())
                    {
                        cellCore.SetBackgroundColor(Color.White.ToAndroid());
                        return;
                    }
                    cellCore.SetBackgroundColor(selectedBackground);
                }
                else
                {
                    cellCore.SetBackgroundColor(Color.White.ToAndroid());
                }
            }
        }
    }
}