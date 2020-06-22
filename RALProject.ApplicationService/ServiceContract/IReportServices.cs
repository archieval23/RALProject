using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RALProject.ApplicationService.DTOs;

namespace RALProject.ApplicationService.ServiceContract
{
    public interface IReportServices : IDisposable
    {
        IEnumerable<ReportDto> ReportAll(ReportDto reportdto);
        void Add(IEnumerable<ReportDto> reportdto);
    }
}
