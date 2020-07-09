using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RALProject.Web.ViewModels
{
    public class BusinessUnitModel
    {
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string jda_library { get; set; }
        public string jda_ip_address { get; set; }
    }
}
