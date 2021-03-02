using ClassAttendance.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassAttendance.Controllers
{
    public class GradeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.ItemList = "Grade List Page";
            GradeHandler iHandler = new GradeHandler();
            ModelState.Clear();
            return View(iHandler.GetGradeList());
        }
    }
}
