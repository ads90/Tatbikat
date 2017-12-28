using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Tatbikat.Models.Enums;

namespace Tatbikat.Models
{
    public class Category : UIObject
    {
        public int ID
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public string ImageSource
        {
            get;
            set;
        }
        public List<Category> SubCategories
        {
            get;
            set;
        }
        private SubCategoriesStatus _selectionStatus;
        [JsonIgnore]
        public SubCategoriesStatus SelectionStatus
        {
            get { return _selectionStatus; }
            set
            {
                Notify(ref _selectionStatus, value);
            }
        }
        private bool _isSelected;
        [JsonIgnore]
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                Notify(ref _isSelected, value);
            }
        }
    }
}
