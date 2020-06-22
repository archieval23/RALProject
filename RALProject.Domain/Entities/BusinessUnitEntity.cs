using RALProject.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RALProject.Domain.Entities
{
    public class BusinessUnitEntity : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual string code { get; set; }
        public virtual string name { get; set; }
        public virtual string short_name { get; set; }
        public virtual string jda_library { get; set; }
        public virtual string jda_ip_address { get; set; }
        public virtual string jda_linked_server_catalog { get; set; }
        public virtual string logo { get; set; }
        public virtual string created_by { get; set; }
        public virtual System.DateTime date_created { get; set; }
        public virtual string edited_by { get; set; }
        public virtual Nullable<System.DateTime> date_edited { get; set; }
        public virtual bool deleted { get; set; }
        public virtual string deleted_by { get; set; }
        public virtual Nullable<System.DateTime> date_deleted { get; set; }
        public virtual string jda_clone_ip { get; set; }
        public virtual string jda_clone_db { get; set; }
        public virtual string jda_clone_prefix { get; set; }
    }
}
