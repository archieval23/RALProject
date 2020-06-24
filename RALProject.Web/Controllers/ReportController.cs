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

        public void ReportGeneration(ReportModel reportModel)
        {
            try
            {
                ReportDto newReport = new ReportDto
                {
                    pONumber = reportModel.pONumber,
                    storeNumber = reportModel.storeNumber,
                    vendorCode = reportModel.vendorCode,
                    receivingDate = reportModel.receivingDate,
                    cancelDate = reportModel.cancelDate,
                    login_dto = new LoginDto
                    {
                        servername = Session["servername"].ToString(),
                        username = Session["username"].ToString(),
                        password = Session["password"].ToString(),
                        dBname = Session["databasename"].ToString()
                    },
                };

                var report = _reportServices.ReportAll(newReport);
                var newreportList = report.Select(a => new ReportDto
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
               
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "System Error: " + ex.Message;
            }
        }
        public ActionResult CreatePdf()
        {
            MemoryStream workStream = new MemoryStream();
            StringBuilder status = new StringBuilder("");
            DateTime dTime = DateTime.Now;
            //file name to be created 
            string strPDFFileName = string.Format("SamplePdf" + dTime.ToString("yyyyMMdd") + "-" + ".pdf");
            Document doc = new Document();
            doc.SetMargins(0f, 0f, 0f, 0f);
            //Create PDF Table with 5 columns
            PdfPTable tableLayout = new PdfPTable(5);
            doc.SetMargins(0f, 0f, 0f, 0f);
            //Create PDF Table

            //file will created in this path
            string strAttachment = Server.MapPath("~/Downloads/" + strPDFFileName);


            PdfWriter.GetInstance(doc, workStream).CloseStream = false;
            doc.Open();

            //Add Content to PDF 
            doc.Add(Add_Content_To_PDF(tableLayout));

            // Closing the document
            doc.Close();

            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;


            return File(workStream, "application/pdf", strPDFFileName);

        }

        protected PdfPTable Add_Content_To_PDF(PdfPTable tableLayout)
        {

            float[] headers = { 50, 24, 45, 35, 50 };  //Header Widths
            tableLayout.SetWidths(headers);        //Set the pdf headers
            tableLayout.WidthPercentage = 100;       //Set the PDF File witdh percentage
            tableLayout.HeaderRows = 1;

            //List<Employee> employees = _context.employees.ToList<Employee>();


            //Add Title to the PDF file at the top
            tableLayout.AddCell(new PdfPCell(new Phrase("Creating Pdf using ItextSharp", new Font(Font.FontFamily.HELVETICA, 8, 1, new iTextSharp.text.BaseColor(0, 0, 0)))) { Colspan = 12, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER });


            var login = new LoginModel();
            login.business_unit_List = _mapper.Map<IEnumerable<BusinessUnitDto>, IEnumerable<BusinessUnitModel>>
                    (_rALServices.BusinessUnitAll());

            ////Add header
            AddCellToHeader(tableLayout, "EmployeeId");
            AddCellToHeader(tableLayout, "Name");
            AddCellToHeader(tableLayout, "Gender");
            AddCellToHeader(tableLayout, "City");
            AddCellToHeader(tableLayout, "Hire Date");

            ////Add body

            foreach (var emp in login.business_unit_List)
            {

                AddCellToBody(tableLayout, emp.date_created.ToString());
                AddCellToBody(tableLayout, emp.name);
                AddCellToBody(tableLayout, emp.jda_ip_address);
                AddCellToBody(tableLayout, emp.code);
                AddCellToBody(tableLayout, emp.jda_linked_server_catalog);

            }

            return tableLayout;
        }
        // Method to add single cell to the Header
        private static void AddCellToHeader(PdfPTable tableLayout, string cellText)
        {

            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.YELLOW))) { HorizontalAlignment = Element.ALIGN_LEFT, Padding = 5, BackgroundColor = new iTextSharp.text.BaseColor(128, 0, 0) });
        }

        // Method to add single cell to the body
        private static void AddCellToBody(PdfPTable tableLayout, string cellText)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_LEFT, Padding = 5, BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255) });
        }

    }
}
