using RALProject.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RALProject.ApplicationService.DTOs
{
    public class BusinessUnitDto
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
