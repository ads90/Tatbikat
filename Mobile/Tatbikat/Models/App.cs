using System;
using System.Collections.Generic;

namespace Tatbikat.Models
{
    public class TatbikatApp
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
        public string AndroidStoreLink
        {
            get;
            set;
        }
        public string IOSStoreLink
        {
            get;
            set;
        }
        public List<Category> AppCategories
        {
            get;
            set;
        }
    }
}
