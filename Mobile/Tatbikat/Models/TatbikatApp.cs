using Newtonsoft.Json;
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
        public string Image
        {
            get;
            set;
        }
        public string IOSStoreLink
        {
            get;
            set;
        }
        public string AndroidStoreLink
        {
            get;
            set;
        }
        public DateTime DateAdded { get; set; }
        public bool IsVerified { get; set; }
        public DateTime DateVerified { get; set; }

        public int IDForIOSApp
        {
            get;
            set;
        }
        public int IDForAndroidApp
        {
            get;
            set;
        }
        [JsonProperty("category")]
        public List<Category> AppCategories
        {
            get;
            set;
        }
        public string AppDescription
        {
            get;
            set;
        }
    }
}
