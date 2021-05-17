using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using RGAttendanceService_V00.DAL;
using RGAttendanceService_V00.Models;
using RGAttendanceService_V00.DAL.Interfaces;

namespace RGAttendanceService_V00.Pages
{
    public class EditCoachModel : PageModel
    {
        private IConfiguration _configuration;
        private ICoach CoachDB;

        [BindProperty]
        public Coach EditCoach { get; set; }

        public EditCoachModel(IConfiguration _configuration, ICoach CoachDB)
        {
            this._configuration = _configuration;
            this.CoachDB = CoachDB;
        }

        public void OnGet(int Id)
        {
            EditCoach = CoachDB.Get(Id);

        }

        public IActionResult OnPost(Coach EditCoach)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    CoachDB.Update(EditCoach);
                    return RedirectToPage("CoachesList");
                }
                return Page();
            }
            catch (Exception ex)
            {

                return RedirectToPage("CoachesList");
            }
        }
    }
}
