using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClassAttendance.Models
{
    public class TermModel
    {
        public int TermID { get; set; }
        [Display(Name = "Term")]
        public string TermName { get; set; }

    }
}
