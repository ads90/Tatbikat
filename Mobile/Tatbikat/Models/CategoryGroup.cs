using System;
using System.Collections.Generic;

namespace Tatbikat.Models
{
    public class CategoryGroup : List<Category>
    {
        public string Title { get; set; }

        public CategoryGroup(string title)
        {
            Title = title;
        }

       //    public static IList<Category> All { private set; get; }
    }
}
