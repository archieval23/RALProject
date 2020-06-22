using RALProject.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RALProject.Domain.Entities
{
    public class ReportEntity : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual string report_id { get; set; }
        public virtual int pONumber { get; set; }
        public virtual int rANumber { get; set; }
        public virtual string aSAuto { get; set; }
        public virtual int storeNumber { get; set; }
        public virtual string storeName { get; set; }
        public virtual int vendorCode { get; set; }
        public virtual string vendorName { get; set; }
        public virtual string receivingDate { get; set; }
        public virtual string cancelDate { get; set; }
        public virtual int ti { get; set; }
        public virtual int hi { get; set; }
        public virtual string Location { get; set; }
        public virtual int iNumber { get; set; }
        public virtual string iDecription { get; set; }
        public virtual string upc { get; set; }
        public virtual string um { get; set; }
        public virtual int orderQty { get; set; }

        public virtual LoginEntity login_entity { get; set; }

        public static ReportEntity Create(ReportEntity report)
        {
            //Place your Business logic here
            report.id = report.id;
            return report;
        }
    }
}
