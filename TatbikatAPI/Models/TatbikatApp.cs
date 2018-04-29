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
     
        public DateTime DateAdded { get; set; }
        public bool IsVerified { get; set; }
        public DateTime DateVerified { get; set; }

        public string IosAppID
        {
            get;
            set;
        }
        public string AndroidAppID
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
