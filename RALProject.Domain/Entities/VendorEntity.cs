using RALProject.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RALProject.Domain.Entities
{
    public class VendorEntity : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual string mnemonic { get; set; }
        public virtual int vendorNumber { get; set; }
        public virtual string vendorName { get; set; }
        public virtual string phoneNumber { get; set; }

        public virtual LoginEntity login_entity { get; set; }
    }
}
