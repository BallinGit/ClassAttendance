using ClassAttendance.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ClassAttendance.Controllers
{
    public class AttendanceController : Controller
    {
        public IActionResult Index(int ClassID)
        {
            ViewBag.ItemList = "Attendance List Page";
            AttendanceHandler iHandler = new AttendanceHandler();

            ClassHandler gHandler = new ClassHandler();
            List<ClassModel> classlist = new List<ClassModel>();
            classlist = gHandler.GetClassList().ToList();
            classlist.Insert(0, new ClassModel() { ClassID = 0, ClassName = "Select" });
            ViewBag.ListOfClasses = classlist;

            ModelState.Clear();
            return View(iHandler.GetAttendanceList(ClassID));
        }

        [HttpGet]
        public ActionResult Create()
        {
            ClassHandler gHandler = new ClassHandler();
            List<ClassModel> classlist = new List<ClassModel>();
            classlist = gHandler.GetClassList().ToList();
            classlist.Insert(0, new ClassModel() { ClassID = 0, ClassName = "Select" });
            ViewBag.ListOfClasses = classlist;


            List<StudentModel> studentlist = new List<StudentModel>();
            studentlist.Insert(0, new StudentModel() { StudentID = 0, StudentFullName = "Select" });
            ViewBag.ListOfStudents = studentlist;

            return View();
        }

        [HttpPost]
        public ActionResult Create(AttendanceModel iList)
        {
            if (ModelState.IsValid)
            {

                AttendanceHandler IHandler = new AttendanceHandler();
                if (IHandler.InsertAttendance(iList))
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


            List<StudentModel> studentlist = new List<StudentModel>();
            studentlist.Insert(0, new StudentModel() { StudentID = 0, StudentFullName = "Select" });
            ViewBag.ListOfStudents = studentlist;

            return View();
        }

        [HttpGet]
        public List<StudentModel> GetStudentList(int id)
        {
            List<StudentModel> studentlist = new List<StudentModel>();
            if (id > 0)
            {
                StudentHandler gHandler = new StudentHandler();
                studentlist = gHandler.GetStudentList().ToList().Where(x => x.Classes.ClassID == id).ToList();
                ViewBag.ListOfStudents = studentlist;
            }
            studentlist.Insert(0, new StudentModel() { StudentID = 0, StudentFullName = "Select" });

            return studentlist;

        }

        public ActionResult Delete(int id)
        {
            try
            {
                AttendanceHandler iHandler = new AttendanceHandler();
                if (iHandler.DeleteAttendance(id))
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
