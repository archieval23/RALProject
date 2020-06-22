using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RALProject.Web.ViewModels
{
    public class UserServerDefaultModel
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
