using RALProject.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RALProject.ApplicationService.DTOs
{
    public class LoginDto
    {
        public int id { get; set; }
        public string dBname { get; set; }
        public string servername { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}
