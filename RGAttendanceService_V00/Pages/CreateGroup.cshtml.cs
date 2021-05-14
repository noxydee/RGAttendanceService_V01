using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RGAttendanceService_V00.DAL;
using RGAttendanceService_V00.Models;
using Microsoft.Extensions.Configuration;

namespace RGAttendanceService_V00.Pages
{
    public class CreateGroupModel : PageModel
    {
        private IConfiguration _configuration;
        private IGroup GroupDB;
        private ICoach CoachDB;

        [BindProperty]
        public Group NewGroup { get; set; }

        public List<Coach> CoachList = new List<Coach>();

        public CreateGroupModel(IConfiguration _configuration,IGroup GroupDB,ICoach CoachDB)
        {
            this._configuration = _configuration;
            this.GroupDB = GroupDB;
            this.CoachDB = CoachDB;
            CoachList = CoachDB.GetList();
        }
        public void OnGet()
        {


        }
        public IActionResult OnPost(Group NewGroup)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    GroupDB.Add(NewGroup);
                    return RedirectToPage("GroupsList");
                }
                return Page();
            }
            catch (Exception ex)
            {

                return RedirectToPage("Index");
            }

        }
    }
}
