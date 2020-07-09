using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RALProject.Web.ViewModels
{
    public class LastLoginModel
    {
        public int id { get; set; }
        public string username { get; set; }
        public string jda_connection { get; set; }
        public int jda_connection_id { get; set; }
    }
}