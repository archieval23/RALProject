using RALProject.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RALProject.ApplicationService.DTOs
{
    public class VendorDto
    {
        public int id { get; set; }
        public string mnemonic { get; set; }
        public int vendorNumber { get; set; }
        public string vendorName { get; set; }
        public string phoneNumber { get; set; }

        public LoginDto login_dto { get; set; }
    }
}
