using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using RGAttendanceService_V00.DataAnnotations;

namespace RGAttendanceService_V00.Models
{
    public class UserModel
    {
        [Display(Name ="Id")]
        public int Id { get; set; }
        [Display(Name ="Nazwa użytkownika")]
        [Required(ErrorMessage ="Pole nazwa użytkownika jest wymagane")]
        public string UserName { get; set; }
        [Display(Name ="Hasło")]
        [Required(ErrorMessage ="Hasło jest wymagane")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name ="Adres email")]
        [Required(ErrorMessage ="Email jest wymagany")]
        public string Email { get; set; }
        public int? CoachId { get; set; }

    }
}
