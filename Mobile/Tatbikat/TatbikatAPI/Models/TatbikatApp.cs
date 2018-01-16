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
        public string ImageSource
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
