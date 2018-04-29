using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TatbikatAPI.Models
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
        public string IosUrl
        {
            get;
            set;
        }
        public string AndroidUrl
        {
            get;
            set;
        }
        public DateTime DateAdded { get; set; }
        public bool IsVerified { get; set; }
        public DateTime DateVerified { get; set; }

        public int IosAppID
        {
            get;
            set;
        }
        public int AndroidAppID
        {
            get;
            set;
        }
        public List<Category> Category
        {
            get;
            set;
        }
        [Newtonsoft.Json.JsonIgnore]
        public string AppDescription
        {
            get;
            set;
        }
    }
}
