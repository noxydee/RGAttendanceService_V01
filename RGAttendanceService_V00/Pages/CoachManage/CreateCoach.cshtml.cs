using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using RGAttendanceService_V00.DAL;
using RGAttendanceService_V00.Models;

namespace RGAttendanceService_V00.Pages
{
    public class CreateCoachModel : PageModel
    {
        private IConfiguration _configuration;
        private ICoach CoachDB;

        [BindProperty]
        public Coach NewCoach{get;set;}

        public CreateCoachModel(IConfiguration _configuration,ICoach CoachDB)
        {
            this._configuration = _configuration;
            this.CoachDB = CoachDB;

        }
        public void OnGet()
        {

        }
        public IActionResult OnPost(Coach NewCoach)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CoachDB.Add(NewCoach);
                    return RedirectToPage("CoachesList");
                }
                return Page();
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
