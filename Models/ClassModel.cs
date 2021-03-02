using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClassAttendance.Models
{
    public class ClassModel
    {
        public int ClassID { get; set; }
        [Display(Name = "Class")]
        public string ClassName { get; set; }
        public GradeModel Grade { get; set; }

    }
}
