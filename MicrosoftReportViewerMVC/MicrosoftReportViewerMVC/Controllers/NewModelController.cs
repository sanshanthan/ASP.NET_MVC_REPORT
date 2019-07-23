using MicrosoftReportViewerMVC.Models;
using System;
using System.Collections.Generic;
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
    }
}