using RALProject.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RALProject.Domain.Entities
{
    public class LastLoginEntity : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual string username { get; set; }
        public virtual  string jda_connection { get; set; }
        public virtual int jda_connection_id { get; set; }

        public static LastLoginEntity Create(LastLoginEntity lastLogin)
        {
            //Place your Business logic here
            lastLogin.id = lastLogin.id;
            return lastLogin;
        }
    }
}
