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
    public class EditGroupModel : PageModel
    {
        private IConfiguration _configuration;
        private IGroup GroupDB;
        private ICoach CoachDB;

        public Group EditGroup { get; set; }
        public List<Coach> CoachList = new List<Coach>();

        public EditGroupModel(IConfiguration _configuration, IGroup GroupDB, ICoach CoachDB)
        {
            this._configuration = _configuration;
            this.GroupDB = GroupDB;
            this.CoachDB = CoachDB;
            CoachList = CoachDB.GetList();
        }
        public void OnGet(int Id)
        {
            EditGroup = GroupDB.Get(Id);
        }

        public IActionResult OnPost(Group EditGroup)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    GroupDB.Update(EditGroup);
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
