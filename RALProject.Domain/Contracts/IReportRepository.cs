using RALProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RALProject.Domain.Contracts
{
    public interface IReportRepository : IRepository<ReportEntity>
    {
        IEnumerable<ReportEntity> GetReport(ReportEntity entity);
        void AddReport(IEnumerable<ReportEntity> entity);
    }
}
