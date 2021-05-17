using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RGAttendanceService_V00.Models
{
    public class PreCheckModel
    {
        [Display(Name ="Data zajęć")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        public int GroupId { get; set; }
        [Required(ErrorMessage ="Zalogowany użytkownik nie jest trenerem")]
        public int CoachId { get; set; }
    }
}
