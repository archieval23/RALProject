//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RALProject.Infrastructure.EntityFramework.RAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserServerDefult
    {
        public int id { get; set; }
        public string UserName { get; set; }
        public string ServerName { get; set; }
        public string created_by { get; set; }
        public System.DateTime date_created { get; set; }
        public string edited_by { get; set; }
        public Nullable<System.DateTime> date_edited { get; set; }
        public bool deleted { get; set; }
        public string deleted_by { get; set; }
        public Nullable<System.DateTime> date_deleted { get; set; }
    }
}
