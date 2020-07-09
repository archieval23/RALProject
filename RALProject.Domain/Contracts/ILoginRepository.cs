using RALProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RALProject.Domain.Contracts
{
    public interface ILoginRepository : IRepository<LoginEntity>
    {
        bool GetByConnectionString(LoginEntity entity);
        IEnumerable<LastLoginEntity> GetLastLoginByUsername(string username);
        void AddLastLogin(LastLoginEntity lastLoginEntity);
    }
}
