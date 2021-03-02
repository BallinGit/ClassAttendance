using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClassAttendance.Models
{
    public class StudentModel
    {
        public int StudentID { get; set; }
        [Display(Name = "Student")]
        public string StudentFullName { get; set; }
        public ClassModel Classes { get; set; }

    }
}
