﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RGAttendanceService_V00.Models
{
    public class Coach
    {
        public int Id { get; set; }
        [Display(Name = "Imię")]
        [Required(ErrorMessage = "Pole imie jest wymagane")]
        public string FirstName { get; set; }
        [Display(Name = "Nazwisko")]
        [Required(ErrorMessage = "Pole nazwisko jest wymagane")]
        public string LastName { get; set; }
        [Display(Name = "Wiek")]
        public int Age { get; set; }

    }
}