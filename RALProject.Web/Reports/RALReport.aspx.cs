using Microsoft.Reporting.WebForms;
using RALProject.Infrastructure.EntityFramework.RAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LoginData = RALProject.Infrastructure.EntityFramework.RAL;
namespace RALProject.Web.Reports
{
    public partial class RALReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var _context = new LoginData.RAL_DevEntities();
                int po = 0;
                int store = 0;
                int vendor = 0;
                string sdate = string.Empty;
                string edate = string.Empty;
                List<ReportTable> reportTable = null;
                reportTable = _context.ReportTables.ToList();
                RALListReportViewer.LocalReport.ReportPath = Server.MapPath("~/Reports/RDLC/RALReport.rdlc");
                RALListReportViewer.LocalReport.DataSources.Clear();
                ReportDataSource rdc = new ReportDataSource("DSRALReport", reportTable);
                RALListReportViewer.LocalReport.DataSources.Add(rdc);
                RALListReportViewer.LocalReport.Refresh();
                RALListReportViewer.DataBind();

                //if (Request.QueryString["searchText"] != null)
                //{
                //    searchText = Request.QueryString["searchText"].ToString();
                //}
                //if (Request.QueryString["po"] != null)
                //{
                //    po = Convert.ToInt32(Request.QueryString["po"]);

                //    //reportTable = _context.ReportTables.Where(a => a.PONUMB == po).ToList();
                //}
                //else
                //{

                //}
            }
        }
    }
}