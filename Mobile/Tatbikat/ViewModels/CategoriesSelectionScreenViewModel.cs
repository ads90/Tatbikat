using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tatbikat.Models;
using Tatbikat.Models.Enums;
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

            Categories = new List<Category>() {
                new Category(){ID=0,Name="خدمات",ImageSource="https://png.icons8.com/ios/50/267F00/air-pilot-hat-filled.png",SubCategories=new List<Category>(){ new Category() { Name="صيانة منزلية" }, new Category() { Name = "غسيل سيارات" }, new Category() { Name = "توصيل مشاوير" }, new Category() { Name = "صيانة مركبات" }, } },
                new Category(){ID=0,Name="طلبات",ImageSource="https://png.icons8.com/ios/50/267F00/delivery-filled.png",SubCategories=new List<Category>(){ } },
                new Category(){ID=0,Name="حجوزات",ImageSource="https://png.icons8.com/material/50/267F00/ticket.png",SubCategories=new List<Category>(){ } },
                new Category(){ID=0,Name="اخرى",ImageSource="https://png.icons8.com/windows/50/267F00/content.png",SubCategories=new List<Category>(){ } }
            };
            //SelectedCategory = Categories.First();
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
                    if (Categories.SelectMany(item => item.SubCategories.Where(sub => sub.IsSelected)).Count() > 3)
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
            if (salescategory.SubCategories.All(item => item.IsSelected))
            {
                salescategory.SelectionStatus = SubCategoriesStatus.AllSelected;
            }
            else if (salescategory.SubCategories.Any(item => item.IsSelected))
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
                if (SelectedCategory == null || SelectedCategory.SubCategories == null)
                {
                    return false;
                }

                return SelectedCategory.SubCategories.All(S => S.IsSelected);
            }
        }

        private void SelectAllSubCategoriesCommandFunction()
        {
            if (SelectedCategory?.SubCategories == null)
            {
                InvokePropertyChanged(nameof(IsAllSubCategoriesSelected));
                return;
            }

            if (SelectedCategory.SubCategories.All(item => !item.IsSelected))
            {
                for (int sc = 0; sc < SelectedCategory.SubCategories.Count; sc++)
                {
                    SelectedCategory.SubCategories[sc].IsSelected = true;
                }

                InvokePropertyChanged(nameof(IsAllSubCategoriesSelected));
            }
            else
            {
                for (int sc = 0; sc < SelectedCategory.SubCategories.Count; sc++)
                {
                    SelectedCategory.SubCategories[sc].IsSelected = false;

                }

                InvokePropertyChanged(nameof(IsAllSubCategoriesSelected));
            }

            RefreshCategorySelectionStatus(SelectedCategory);

        }


        private void SaveCommandFunction()
        {
            List<Category> selectedCategories = Categories.SelectMany(item => item.SubCategories.Where(sub => sub.IsSelected)).ToList();

            if (selectedCategories.Count() == 0)
            {
                Application.Current.MainPage.DisplayAlert("تنبيه", "الرجاء اختيار تصنيف واحد واحد عالاقل", "موافق");
                return;
            }
            if (selectedCategories.Count() > 3)
            {
                Application.Current.MainPage.DisplayAlert("تنبيه", "لايمكن اختيار اكثر من 3 تصيفات للتطبيق", "موافق");
                return;
            }

            //for (int catIndex = 0; catIndex < selectedCategories.Count; catIndex++)
            //{
            //    selectedCategories[catIndex].SubCategories.RemoveAll(cat => !cat.IsSelected);
            //}

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
