//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TatbikatModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class App
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string IosUrl { get; set; }
        public string AndroidUrl { get; set; }
        public System.DateTime DateAdded { get; set; }
        public bool IsVerified { get; set; }
        public Nullable<System.DateTime> DateVerified { get; set; }
    }
}