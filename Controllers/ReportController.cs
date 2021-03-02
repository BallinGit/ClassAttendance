using ClassAttendance.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassAttendance.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Index(int TermID)
        {
            ViewBag.ItemList = "Report Page";
            ReportHandler iHandler = new ReportHandler();

            TermHandler gHandler = new TermHandler();
            List<TermModel> termlist = new List<TermModel>();
            termlist = gHandler.GetTermList().ToList();
            termlist.Insert(0, new TermModel() { TermID = 0, TermName = "Select" });
            ViewBag.ListOfTerms = termlist;

            ModelState.Clear();
            return View(iHandler.GetReportList(TermID));
        }
    }
}
