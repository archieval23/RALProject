using RALProject.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RALProject.Domain.Entities
{
    public class POEntity : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual int vendorNumber { get; set; }
        public virtual int masterPONumber { get; set; }
        public virtual int pONumber { get; set; }
        public virtual int orderDate { get; set; }
        public virtual string buyer { get; set; }
        public virtual int dept { get; set; }
        public virtual int subDept { get; set; }

        public virtual LoginEntity login_entity { get; set; }
    }
}
