using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClassAttendance.Models
{
    public class GradeModel
    {
        public int GradeID { get; set; }
        [Display(Name = "Grade")]
        public string GradeName { get; set; }

    }
}
