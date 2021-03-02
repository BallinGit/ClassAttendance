using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassAttendance.Models;

namespace ClassAttendance.Controllers
{
    public class TermController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.ItemList = "Term List Page";
            TermHandler iHandler = new TermHandler();
            ModelState.Clear();
            return View(iHandler.GetTermList());
        }
    }
}
