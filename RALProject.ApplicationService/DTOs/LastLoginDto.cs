using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RALProject.ApplicationService.DTOs
{
    public class LastLoginDto
    {
        public int id { get; set; }
        public string username { get; set; }
        public string jda_connection { get; set; }
        public int jda_connection_id { get; set; }
    }
}
