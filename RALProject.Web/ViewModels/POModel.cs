using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RALProject.Web.ViewModels
{
    public class POModel
    {
        public int id { get; set; }
        public int vendorNumber { get; set; }
        public int masterPONumber { get; set; }
        public int pONumber { get; set; }
        public int orderDate { get; set; }
        public string buyer { get; set; }
        public int dept { get; set; }
        public int subDept { get; set; }

        public LoginModel login { get; set; }
    }
}
