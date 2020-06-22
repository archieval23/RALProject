using RALProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RALProject.Domain.Contracts
{
    public interface IPORepository : IRepository<POEntity>
    {
        IEnumerable<POEntity> GetPO(POEntity entity);
    }
}
