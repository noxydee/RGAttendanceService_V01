using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RGAttendanceService_V00.Models
{
    public class Group
    {
        public int Id { get; set; }
        [Display(Name ="Nazwa Grupy")]
        [Required(ErrorMessage ="Pole nazwa jest wymagane")]
        public string Name { get; set; }
        [Display(Name ="Typ grupy")]
        public string type { get; set; }
        [Display(Name="Ulica")]
        public string Street { get; set; }
        [Display(Name ="Miasto")]
        public string City { get; set; }
        [Display(Name ="Numer")]
        public string Number { get; set; }
        //Navigation Properties
        public int? CoachId { get; set; }
        public Coach Coach { get; set; }
        public List<Participant> Participants { get; set; }
    }
}
