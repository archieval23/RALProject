using RALProject.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RALProject.ApplicationService.DTOs
{
    public class StoreDto
    {
        public int id { get; set; }
        public int store { get; set; }
        public int region { get; set; }
        public int district { get; set; }
        public string description { get; set; }
        public string manager { get; set; }
        public string warehouse { get; set; }

        public LoginDto login_dto { get; set; }
    }
}
