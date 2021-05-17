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
    public class GroupsListModel : PageModel
    {
        public List<Group> List = new List<Group>();

        private IConfiguration _configuartion;
        private IGroup GroupDB;
        private ICoach CoachDB;

        public GroupsListModel(IConfiguration _configuartion, IGroup GroupDB, ICoach CoachDB)
        {
            this._configuartion = _configuartion;
            this.GroupDB = GroupDB;
            this.CoachDB = CoachDB;
            List = GroupDB.GetList();
        }
        public void OnGet()
        {
            foreach(Group g in List)
            {
                g.Coach = CoachDB.Get(g.CoachId ?? 0);
            }
        }

        public IActionResult OnGetDeleteGroup(int Id)
        {
            try
            {
                GroupDB.Delete(Id);
                return RedirectToPage("GroupsList");
            }
            catch (Exception ex)
            {

                return RedirectToPage("GroupsList");
            }
        }

    }
}
