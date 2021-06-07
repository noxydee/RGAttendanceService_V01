using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using RGAttendanceService_V00.DataAnnotations;
using RGAttendanceService_V00.DAL;

namespace RGAttendanceService_V00.Models
{
    public class PreCheckModel
    {
        ParentContext _context { get; set; }
        [Display(Name ="Data zajęć")]
        [DataType(DataType.DateTime, ErrorMessage ="Błędny format daty")]
        public DateTime Date { get; set; }
        public int GroupId { get; set; }
        public int? CoachId { get; set; }
    }
}
