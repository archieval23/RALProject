//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RALProject.Infrastructure.EntityFramework.Common
{
    using System;
    using System.Collections.Generic;
    
    public partial class business_units
    {
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string short_name { get; set; }
        public string jda_library { get; set; }
        public string jda_ip_address { get; set; }
        public string jda_linked_server_catalog { get; set; }
        public string logo { get; set; }
        public string created_by { get; set; }
        public System.DateTime date_created { get; set; }
        public string edited_by { get; set; }
        public Nullable<System.DateTime> date_edited { get; set; }
        public bool deleted { get; set; }
        public string deleted_by { get; set; }
        public Nullable<System.DateTime> date_deleted { get; set; }
        public string jda_clone_ip { get; set; }
        public string jda_clone_db { get; set; }
        public string jda_clone_prefix { get; set; }
    }
}