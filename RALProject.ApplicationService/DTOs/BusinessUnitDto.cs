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
        public string jda_library { get; set; }
        public string jda_ip_address { get; set; }

    }
}
