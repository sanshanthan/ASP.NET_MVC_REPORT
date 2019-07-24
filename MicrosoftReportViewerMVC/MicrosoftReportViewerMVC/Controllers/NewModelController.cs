using Microsoft.Reporting.WinForms;
using MicrosoftReportViewerMVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MicrosoftReportViewerMVC.Controllers
{
    public class NewModelController : Controller
    {
        ABC_Company_DBEntities dbcon = new ABC_Company_DBEntities();
        // GET: NewModel
        public ActionResult StateArea()
        {
            using (dbcon)
            {
                var v = dbcon.OutstandingAndActiveProjects.ToList();
                return View(v);
            }
        }

        public ActionResult Report(string id)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Reports"), "Report1.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }
            List<OutstandingAndActiveProject> cm = new List<OutstandingAndActiveProject>();
            using (dbcon)
            {
                cm = dbcon.OutstandingAndActiveProjects.ToList();
            }
            ReportDataSource rd = new ReportDataSource("Dataset", cm);
            lr.DataSources.Add(rd);
            string reportType = id;
            string mimeType;
            string encoding;
            string fileNameExtension;



            string deviceInfo =

            "<DeviceInfo>" +
            "  <OutputFormat>" + id + "</OutputFormat>" +
            "  <PageWidth>8.5in</PageWidth>" +
            "  <PageHeight>11in</PageHeight>" +
            "  <MarginTop>0.5in</MarginTop>" +
            "  <MarginLeft>1in</MarginLeft>" +
            "  <MarginRight>1in</MarginRight>" +
            "  <MarginBottom>0.5in</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);
            return File(renderedBytes, mimeType);
        }
    }
}