using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RALProject.Domain.Entities
{
    public abstract class EntityBase : IAuditable
    {
        private DateTime _modifiedDate;

        public virtual DateTime ModifiedDate
        {
            get { return _modifiedDate < DateTime.Parse("1753-01-01") ? DateTime.Now : _modifiedDate; }
            set { _modifiedDate = value; }
        }

        public virtual string ModifiedBy { get; set; }

        private DateTime _createdDate;

        public virtual DateTime CreatedDate
        {
            get { return _createdDate < DateTime.Parse("1753-01-01") ? DateTime.Now : _createdDate; }
            set { _createdDate = value; }
        }

        public virtual string CreatedBy { get; set; }
    }

    public interface IAuditable
    {
        DateTime ModifiedDate { get; set; }
        string ModifiedBy { get; set; }
        DateTime CreatedDate { get; set; }
        string CreatedBy { get; set; }
    }
}
