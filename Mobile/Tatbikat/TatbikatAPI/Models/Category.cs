﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TatbikatAPI.Models
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