using System;
using System.Collections.Generic;

namespace Tatbikat.Models
{
    public class Category
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
    }
}
