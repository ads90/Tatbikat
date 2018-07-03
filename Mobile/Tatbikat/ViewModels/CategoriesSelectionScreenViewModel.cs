using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tatbikat.Models;
using Tatbikat.Models.Enums;
using Tatbikat.Operations;
using Tatbikat.UI.Interfaces;
using Xamarin.Forms;

namespace Tatbikat.ViewModels
{
    public class CategoriesSelectionScreenViewModel : ViewModelsBase, ICallbackEnabledScreen<List<Category>>
    {
        public Command SelectAllSubCategoriesCommand { get; set; }
        public Command SaveCommand { get; set; }
        TaskCompletionSource<List<Category>> _pageTcs;

        public bool IsNewApp { get; set; }
        public CategoriesSelectionScreenViewModel(bool isNewApp)
        {
            IsNewApp = isNewApp;
            SelectAllSubCategoriesCommand = new Command(SelectAllSubCategoriesCommandFunction);
            SaveCommand = new Command(SaveCommandFunction);

            _pageTcs = new TaskCompletionSource<List<Category>>();

            GetAllCategoriesAsync();
        }

        private async void GetAllCategoriesAsync()
        {
            IsLoading = true;
            Categories = await Connector.Current.GetAllCategories();
            if (Categories == null)
            {
                IsLoading = false;
                await App.Current.MainPage.DisplayAlert("خطا", "حدث خطا", "موافق");
                return;
            }
            SelectedCategory = Categories.FirstOrDefault();
            IsLoading = false;
        }
        #region Properties
        private List<string> _salesmanSelectedSubcategories = new List<string>();
        private List<Category> _categories;
        public List<Category> Categories
        {
            get { return _categories; }
            set { RefreshUIProperty(ref _categories, value); }
        }
        private Category _selectedItemSubCategory;
        public Category SelectedSubCategory
        {
            get { return _selectedItemSubCategory; }
            set
            {
                if (value != null)
                {

                    value.IsSelected = !value.IsSelected;
                }
                RefreshUIProperty(ref _selectedItemSubCategory, value);
                _selectedItemSubCategory = null;
                RefreshCategorySelectionStatus(SelectedCategory);
                InvokePropertyChanged(nameof(IsAllSubCategoriesSelected));

                if (IsNewApp)
                {
                    if (Categories.SelectMany(item => item.SubCategory.Where(sub => sub.IsSelected)).Count() > 3)
                    {
                        App.Current.MainPage.DisplayAlert(":(", "عفوا لايمكن اختيار اكثر من 3 تصنيفات", "موافق");
                        value.IsSelected = false;
                        return;
                    }
                }
            }
        }
        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                if (_selectedCategory != null)
                {
                    _selectedCategory.IsSelected = false;
                }
                if (value != null)
                {
                    value.IsSelected = true;
                }

                RefreshUIProperty(ref _selectedCategory, value);

                InvokePropertyChanged(nameof(IsAllSubCategoriesSelected));

            }
        }
        #endregion
        private void RefreshCategorySelectionStatus(Category salescategory)
        {
            if (salescategory.SubCategory.All(item => item.IsSelected))
            {
                salescategory.SelectionStatus = SubCategoriesStatus.AllSelected;
            }
            else if (salescategory.SubCategory.Any(item => item.IsSelected))
            {
                salescategory.SelectionStatus = SubCategoriesStatus.ManySelected;
            }
            else
            {
                salescategory.SelectionStatus = SubCategoriesStatus.NonSelected;
            }
        }

        public bool IsAllSubCategoriesSelected
        {
            get
            {
                if (SelectedCategory == null || SelectedCategory.SubCategory == null)
                {
                    return false;
                }

                return SelectedCategory.SubCategory.All(S => S.IsSelected);
            }
        }

        private void SelectAllSubCategoriesCommandFunction()
        {
            if (SelectedCategory?.SubCategory == null)
            {
                InvokePropertyChanged(nameof(IsAllSubCategoriesSelected));
                return;
            }

            if (SelectedCategory.SubCategory.All(item => !item.IsSelected))
            {
                for (int sc = 0; sc < SelectedCategory.SubCategory.Count; sc++)
                {
                    SelectedCategory.SubCategory[sc].IsSelected = true;
                }

                InvokePropertyChanged(nameof(IsAllSubCategoriesSelected));
            }
            else
            {
                for (int sc = 0; sc < SelectedCategory.SubCategory.Count; sc++)
                {
                    SelectedCategory.SubCategory[sc].IsSelected = false;

                }

                InvokePropertyChanged(nameof(IsAllSubCategoriesSelected));
            }

            RefreshCategorySelectionStatus(SelectedCategory);

        }


        private void SaveCommandFunction()
        {
            List<Category> selectedCategories = Categories.SelectMany(item => item.SubCategory.Where(sub => sub.IsSelected)).ToList();
            if (selectedCategories.Count() == 0)
            {
                Application.Current.MainPage.DisplayAlert("تنبيه", "الرجاء اختيار تصنيف واحد واحد عالاقل", "موافق");
                return;
            }
            if (IsNewApp)
            {
                if (selectedCategories.Count() > 3)
                {
                    Application.Current.MainPage.DisplayAlert("تنبيه", "لايمكن اختيار اكثر من 3 تصيفات للتطبيق", "موافق");
                    return;
                }
            }

            _pageTcs?.TrySetResult(selectedCategories);
        }
        public override void NavigateBackRequested()
        {
            _pageTcs?.TrySetResult(null);
        }
        public Task<List<Category>> Wait()
        {
            return _pageTcs.Task;
        }
    }
}
