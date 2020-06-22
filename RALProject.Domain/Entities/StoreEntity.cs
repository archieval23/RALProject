using RALProject.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RALProject.Domain.Entities
{
    public class StoreEntity : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual int store { get; set; }
        public virtual int region { get; set; }
        public virtual int district { get; set; }
        public virtual string description { get; set; }
        public virtual string manager { get; set; }
        public virtual string warehouse { get; set; }

        public virtual LoginEntity login_entity { get; set; }
    }
}
