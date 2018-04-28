using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TatbikatAPI.Models
{
    public class Category
    {
        public int Id
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        [JsonProperty("Image")]
        public string ImageSource
        {
            get;
            set;
        }
       
        //if sub is null means cat is sub
        public IList<Category> SubCategory
        {
            get;
            set;
        } = null;
    }
}
