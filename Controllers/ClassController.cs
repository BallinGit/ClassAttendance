using ClassAttendance.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassAttendance.Controllers
{
    public class ClassController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.ItemList = "Class List Page";
            ClassHandler iHandler = new ClassHandler();
            ModelState.Clear();
            return View(iHandler.GetClassList());

        }

        [HttpGet]
        public ActionResult Create()
        {
            GradeHandler gHandler = new GradeHandler();
            List<GradeModel> gradelist = new List<GradeModel>();
            gradelist = gHandler.GetGradeList().ToList();
            gradelist.Insert(0, new GradeModel() { GradeID = 0, GradeName = "Select" });
            ViewBag.ListOfGrades = gradelist;

            return View();
        }

        [HttpPost]
        public ActionResult Create(ClassModel iList)
        {
            if (ModelState.IsValid)
            {

                ClassHandler IHandler = new ClassHandler();
                if (IHandler.InsertClass(iList))
                {
                    ViewBag.AlertMsg = "Item Added successfully";
                    ModelState.Clear();
                }
                else
                {
                    ViewBag.AlertMsg = "Item Add Failed";
                    ModelState.Clear();
                }
            }

            GradeHandler gHandler = new GradeHandler();
            List<GradeModel> gradelist = new List<GradeModel>();
            gradelist = gHandler.GetGradeList().ToList();
            gradelist.Insert(0, new GradeModel() { GradeID = 0, GradeName = "Select" });
            ViewBag.ListOfGrades = gradelist;

            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ClassHandler iHandler = new ClassHandler();
            ClassModel classItem = iHandler.GetClassList().Find(itemmmodel => itemmmodel.ClassID == id);

            GradeHandler gHandler = new GradeHandler();
            List<GradeModel> gradelist = new List<GradeModel>();
            gradelist = gHandler.GetGradeList().ToList();

            gradelist.Insert(classItem.Grade.GradeID, new GradeModel() { GradeID = 0, GradeName = "Select" });
            ViewBag.ListOfGrades = gradelist;

            return View(classItem);
        }
        [HttpPost]
        public ActionResult Edit(int id, ClassModel iList)
        {
            try
            {
                ClassHandler iHnadler = new ClassHandler();
                iList.ClassID = id;
                iHnadler.UpdateClass(iList);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                return View();
            }

        }

        public ActionResult Delete(int id)
        {
            try
            {
                ClassHandler iHandler = new ClassHandler();
                if (iHandler.DeleteClass(id))
                {
                    ViewBag.AlertMsg = "Item Deleted successfully";
                }
                return RedirectToAction("Index");

            }
            catch (Exception)
            {

                return View();
            }
        }
    }
}
