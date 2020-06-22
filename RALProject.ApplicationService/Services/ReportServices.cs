using RALProject.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using RALProject.Domain.Contracts;
using AutoMapper;
using RALProject.Domain.Entities;
using RALProject.Common.Logger;
using RALProject.Common.EmailHelper;
using RALProject.Infrastructure.EntityFramework.RAL;
using RALProject.ApplicationService.ServiceContract;

namespace RALProject.ApplicationService.Services
{
    public class ReportServices : IReportServices
    {
        private readonly IMapper _mapper;
        private readonly IEmailManager _emailManager;
        private readonly IReportRepository _reportRepository;
        private readonly IRALUnitOfWork _rALUnitOfWork;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(ReportServices));

        public ReportServices
        (
            IReportRepository reportRepository,
            IRALUnitOfWork rALUnitOfWork,
            IMapper mapper, 
            IEmailManager emailManager
        )
        {
            if (reportRepository == null) throw new ArgumentNullException("ReportRepository");
            if (rALUnitOfWork == null) throw new ArgumentNullException("RALUnitOfWork");
            if (mapper == null) throw new ArgumentNullException("Mapper");
            if (emailManager == null) throw new ArgumentNullException("EmailManager");

            _reportRepository = reportRepository;
            _rALUnitOfWork = rALUnitOfWork;
            _mapper = mapper;
            _emailManager = emailManager;
        }

        public IEnumerable<ReportDto> ReportAll(ReportDto reportdto)
        {
            try
            {
                ReportEntity reportEntity = new ReportEntity
                {
                    pONumber = reportdto.pONumber,
                    storeNumber = reportdto.storeNumber,
                    vendorCode = reportdto.vendorCode,
                    receivingDate = reportdto.receivingDate,
                    cancelDate = reportdto.cancelDate,
                    login_entity = new LoginEntity
                    {
                        servername = reportdto.login_dto.servername,
                        username = reportdto.login_dto.username,
                        password = reportdto.login_dto.password,
                        dBname = reportdto.login_dto.dBname
                    },
                };

                return _mapper.Map<IEnumerable<ReportEntity>, IEnumerable<ReportDto>>
                    (_reportRepository.GetReport(reportEntity));
            }
            catch (Exception ex)
            {
                logCentral.Error("BusinessUnitAll", ex);
                throw;
            }
        }

        public void Add(IEnumerable<ReportDto> reportdto)
        {
            try
            {
                List<ReportEntity> newReportList = new List<ReportEntity>();

                reportdto.ToList().ForEach(e =>
                {
                    newReportList.Add(ReportEntity.Create(_mapper.Map<ReportDto, ReportEntity>(e)));
                });

                if (newReportList != null)
                {
                    _reportRepository.AddReport(newReportList);
                    _rALUnitOfWork.Save();

                    newReportList.ToList().ForEach(e =>
                    {
                        logCentral.Info(string.Format("Report {0} has been created", e.id));
                    });
                }
                else
                {
                    logCentral.Info("Create Report: Invalid Role information");
                }
            }
            catch (Exception ex)
            {
                logCentral.Error("Create Report", ex);
                throw;
            }
        }

        public void Dispose()
        {
            _rALUnitOfWork.Dispose();
        }
    }
}
