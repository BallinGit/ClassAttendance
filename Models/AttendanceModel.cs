using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClassAttendance.Models
{
    public class AttendanceModel
    {
        public int AttendanceID { get; set; }
        public ClassModel Classes { get; set; }
        public StudentModel Students { get; set; }
        [Display(Name = "DateTime")]
        public DateTime AttendanceDateTime { get; set; }
    }
}
