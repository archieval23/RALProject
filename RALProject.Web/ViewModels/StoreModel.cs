using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RALProject.Web.ViewModels
{
    public class StoreModel
    {
        public int id { get; set; }
        public int store { get; set; }
        public int region { get; set; }
        public int district { get; set; }
        public string description { get; set; }
        public string manager { get; set; }
        public string warehouse { get; set; }

        public LoginModel login { get; set; }
    }
}
