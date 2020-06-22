using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RALProject.Web.ViewModels
{
    public class LoginModel
    {
        public int id { get; set; }
        public string dBname { get; set; }
        public string servername { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        public IEnumerable<BusinessUnitModel> business_unit_List { get; set; }
    }
}
