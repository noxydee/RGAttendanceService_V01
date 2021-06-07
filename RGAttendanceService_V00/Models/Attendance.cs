using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RGAttendanceService_V00.Models
{
    public class Attendance
    {
        [Display(Name ="Id")]
        public int Id { get; set; }
        public int ParticipantId { get; set; }
        public int GroupId { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name ="Data zajęć")]
        public DateTime DateOfClass { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? DateOfCheck { get; set; }
        [Display(Name ="Nieobecny")]
        public bool AbsenceStatus { get; set; }
        [Display(Name ="Powód nieobecności")]
        public string AbsenceInfo { get; set; }
        public int? CheckerId { get; set; }
 

        public Group ParticipatingGroup { get; set; }
        public Participant Participant { get; set; }
        public Coach Checker { get; set; }
    }
}
