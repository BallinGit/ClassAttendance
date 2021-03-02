using ClassAttendance.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassAttendance.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.ItemList = "Student List Page";
            StudentHandler iHandler = new StudentHandler();
            ModelState.Clear();
            return View(iHandler.GetStudentList());
        }


        [HttpGet]
        public ActionResult Create()
        {
            ClassHandler gHandler = new ClassHandler();
            List<ClassModel> classlist = new List<ClassModel>();
            classlist = gHandler.GetClassList().ToList();
            classlist.Insert(0, new ClassModel() { ClassID = 0, ClassName = "Select" });
            ViewBag.ListOfClasses = classlist;

            return View();
        }

        [HttpPost]
        public ActionResult Create(StudentModel iList)
        {
            if (ModelState.IsValid)
            {

                StudentHandler IHandler = new StudentHandler();
                if (IHandler.InsertStudent(iList))
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

            ClassHandler gHandler = new ClassHandler();
            List<ClassModel> classlist = new List<ClassModel>();
            classlist = gHandler.GetClassList().ToList();
            classlist.Insert(0, new ClassModel() { ClassID = 0, ClassName = "Select" });
            ViewBag.ListOfClasses = classlist;

            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            StudentHandler iHandler = new StudentHandler();
            StudentModel studentItem = iHandler.GetStudentList().Find(itemmmodel => itemmmodel.StudentID == id);

            ClassHandler gHandler = new ClassHandler();
            List<ClassModel> classlist = new List<ClassModel>();
            classlist = gHandler.GetClassList().ToList();
            classlist.Insert(studentItem.Classes.ClassID, new ClassModel() { ClassID = 0, ClassName = "Select" });
            ViewBag.ListOfClasses = classlist;

            return View(studentItem);
        }
        [HttpPost]
        public ActionResult Edit(int id, StudentModel iList)
        {
            try
            {
                StudentHandler iHnadler = new StudentHandler();
                iList.StudentID = id;
                iHnadler.UpdateStudent(iList);
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
                StudentHandler iHandler = new StudentHandler();
                if (iHandler.DeleteStudent(id))
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
