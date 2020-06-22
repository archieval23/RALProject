using RALProject.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RALProject.Domain.Entities
{
    public class UserServerDefaultEntity : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual string UserName { get; set; }
        public virtual string ServerName { get; set; }
        public virtual string created_by { get; set; }
        public virtual System.DateTime date_created { get; set; }
        public virtual string edited_by { get; set; }
        public virtual Nullable<System.DateTime> date_edited { get; set; }
        public virtual bool deleted { get; set; }
        public virtual string deleted_by { get; set; }
        public virtual Nullable<System.DateTime> date_deleted { get; set; }
    }
}
