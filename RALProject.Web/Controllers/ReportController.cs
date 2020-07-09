using AutoMapper;

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using System.IO;
using System.Web.Security;
using System.Configuration;

using RALProject.ApplicationService.DTOs;
using RALProject.ApplicationService.ServiceContract;
using RALProject.Common.Logger;
using RALProject.Web.ActionFilters;
using RALProject.Web.ViewModels;
using Newtonsoft.Json;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace RALProject.Web.Controllers
{
    public class ReportController : Controller
    {
        private readonly IRALServices _rALServices;
        private readonly IReportServices _reportServices;
        private readonly IMapper _mapper;

        public ReportController
        (
            IRALServices rALServices,
            IReportServices reportServices,
            IMapper mapper
        )
        {
            if (rALServices == null) throw new ArgumentNullException("RALServices");
            if (reportServices == null) throw new ArgumentNullException("ReportServices");
            if (mapper == null) throw new ArgumentNullException("Mapper");

            _rALServices = rALServices;
            _reportServices = reportServices;
            _mapper = mapper;
        }
        //
        // GET: /Report/

        public ActionResult Index()
        {
            return View();
        }
        public int ReportGeneration(ReportModel reportModel)
        {
            try
            {
                string d1,d2;
                string rdate = reportModel.receivingDate;
                string cdate = reportModel.cancelDate;
                d1 = rdate.Replace("-", "");
                d2 = cdate.Replace("-", "");

                d1=d1.Substring(2);
                d2 = d2.Substring(2);

                ReportDto newReport = new ReportDto
                {
                    pONumber = reportModel.pONumber,
                    storeNumber = reportModel.storeNumber,
                    vendorCode = reportModel.vendorCode,
                    receivingDate = d1,
                    cancelDate = d2,
                    login_dto = new LoginDto
                    {
                        servername = Session["servername"].ToString(),
                        username = Session["username"].ToString(),
                        password = Session["password"].ToString(),
                        dBname = Session["databasename"].ToString()
                    },
                };

                var report = _reportServices.ReportAll(newReport);
                IEnumerable<ReportDto> newreportList = report.Select(a => new ReportDto
                {
                    report_id = Session["reportId"].ToString(),
                    pONumber = a.pONumber,
                    rANumber = a.rANumber,
                    aSAuto = a.aSAuto,
                    storeNumber = a.storeNumber,
                    storeName = a.storeName,
                    vendorCode = a.vendorCode,
                    vendorName = a.vendorName,
                    receivingDate = a.receivingDate,
                    cancelDate = a.cancelDate,
                    ti = a.ti,
                    hi = a.hi,
                    Location = a.Location,
                    iNumber = a.iNumber,
                    iDecription = a.iDecription,
                    upc = a.upc,
                    um = a.um,
                    orderQty = a.orderQty
                });


                _reportServices.Add(newreportList);
                var reportcount = newreportList.Count();

                if(reportcount > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "System Error: " + ex.Message;
                return 0;
            }
           
        }

        public bool CreatePdf()
        {
            return true;
        }
    }
}
