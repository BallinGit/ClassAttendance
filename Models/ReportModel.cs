using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassAttendance.Models
{
    public class ReportModel
    {
        public ClassModel Classes { get; set; }
        public GradeModel Grades { get; set; }
        public StudentModel Students { get; set; }
        public int ClassesAttended { get; set; }
        public int ClassesMissed { get; set; }
    }
}
