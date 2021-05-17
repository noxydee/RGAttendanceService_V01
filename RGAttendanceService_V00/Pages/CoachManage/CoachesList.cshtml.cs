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
    public class CoachesListModel : PageModel
    {
        private IConfiguration _configuration;
        private ICoach CoachDB;

        public List<Coach> List = new List<Coach>();

        public CoachesListModel(IConfiguration _configuration,ICoach CoachDB)
        {
            this._configuration = _configuration;
            this.CoachDB = CoachDB;
            List = CoachDB.GetList();
        }
        public void OnGet()
        {

        }

        public IActionResult OnGetDeleteCoach(int Id)
        {
            try
            {
                CoachDB.Delete(Id);
                return RedirectToPage("CoachesList");
            }
            catch (Exception ex)
            {

                return RedirectToPage("CoachesList");
            }

        }


    }
}
