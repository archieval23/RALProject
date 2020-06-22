using RALProject.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RALProject.ApplicationService.DTOs
{
    public class ReportDto
    {
        public int id { get; set; }
        public string report_id { get; set; }
        public int pONumber { get; set; }
        public int rANumber { get; set; }
        public string aSAuto { get; set; }
        public int storeNumber { get; set; }
        public string storeName { get; set; }
        public int vendorCode { get; set; }
        public string vendorName { get; set; }
        public string receivingDate { get; set; }
        public string cancelDate { get; set; }
        public int ti { get; set; }
        public int hi { get; set; }
        public string Location { get; set; }
        public int iNumber { get; set; }
        public string iDecription { get; set; }
        public string upc { get; set; }
        public string um { get; set; }
        public int orderQty { get; set; }

        public LoginDto login_dto { get; set; }
    }
}
