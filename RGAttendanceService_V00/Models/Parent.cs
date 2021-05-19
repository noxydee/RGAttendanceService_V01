using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RGAttendanceService_V00.Models
{
    public class Parent
    {
        [Display(Name = "Numer ID")]
        public int Id { get; set; }

        [Display(Name = "Imię")]
        [Required(ErrorMessage = "Pole imię jest wymagane")]
        public string FirstName { get; set; }

        [Display(Name = "Nazwisko")]
        [Required(ErrorMessage = "Pole nazwisko jest wymagane")]
        public string LastName { get; set; }
        [Display(Name = "Data Urodzenia")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
        [Display(Name = "Płeć")]
        [Required(ErrorMessage = "Pole płeć jest wymagane")]
        public string Gender { get; set; }
        [Display(Name = "Numer Telefonu")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Błędy number telefonu")]
        public string PhoneNumber { get; set; }
        [Display(Name ="Pokrewieństwo")]
        public string Relation { get; set; }
        [Display(Name = "Wiek")]
        public int? Age { get; set; }
        [Display(Name = "Kraj")]
        public string Country { get; set; }
        [Display(Name = "Miasto")]
        public string AddressCity { get; set; }
        [Display(Name = "Ulica")]
        public string AddressStreet { get; set; }
        [Display(Name = "Numer Domu")]
        public string AddressNumber { get; set; }
        



    }
}
