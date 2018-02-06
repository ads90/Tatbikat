using Newtonsoft.Json;
using System;
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
        [JsonProperty("IMAGE")]
        public string ImageSource
        {
            get;
            set;
        }
        public IList<Category> SubCategory
        {
            get;
            set;
        } = null;
    }
}
