using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tatbikat.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tatbikat.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CategoriesSelectionScreen : ContentPage
	{
		public CategoriesSelectionScreen (bool IsNewApp)
		{
			InitializeComponent ();
            this.BindingContext = new CategoriesSelectionScreenViewModel(IsNewApp);
        }
	}
}