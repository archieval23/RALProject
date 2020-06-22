using RALProject.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RALProject.Domain.Entities
{
    public class LoginEntity : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual string dBname { get; set; }
        public virtual string servername { get; set; }
        public virtual string username { get; set; }
        public virtual string password { get; set; }
    }
}
