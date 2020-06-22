using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RALProject.Web.ViewModels
{
    public class VendorModel
    {
        public int id { get; set; }
        public string mnemonic { get; set; }
        public int vendorNumber { get; set; }
        public string vendorName { get; set; }
        public string phoneNumber { get; set; }

        public LoginModel login { get; set; }
    }
}
