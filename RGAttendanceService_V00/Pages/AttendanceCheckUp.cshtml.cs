using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using RGAttendanceService_V00.DAL;
using RGAttendanceService_V00.Models;
using System.Security.Claims;
using RGAttendanceService_V00.DAL.Interfaces;
using System.Text.Json;

namespace RGAttendanceService_V00.Pages
{
    public class AttendanceCheckUpModel : PageModel
    {
        private IConfiguration _configuration;
        private IGroup GroupDB;
        private ICoach CoachDB;
        private IUser UserDB;

        [BindProperty]
        public PreCheckModel PreCheck { get; set; }
        public List<Group> GroupList = new List<Group>();
        public AttendanceCheckUpModel(IConfiguration _configuration,IGroup GroupDB,ICoach CoachDB,IUser UserDB)
        {
            this._configuration = _configuration;
            this.GroupDB = GroupDB;
            this.CoachDB = CoachDB;
            this.UserDB = UserDB;
            GroupList = GroupDB.GetList();
        }

        public void OnGet()
        {
            
        }

        public IActionResult OnPost(PreCheckModel preCheck)
        {
            try
            {
                int UserId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value);
                System.Diagnostics.Debug.WriteLine("in onpost ->"+UserId);
                UserModel x = UserDB.Get(UserId);

                System.Diagnostics.Debug.WriteLine("gitara -> " +x.CoachId);

                if (x.CoachId == null || x.CoachId == 0)
                {

                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("gitara siema -> " + x.CoachId);
                    preCheck.CoachId = x.CoachId ?? default(int);
                    System.Diagnostics.Debug.WriteLine("gitara siema 2 -> " + preCheck.CoachId);
                }

                if(ModelState.IsValid)
                {
                    return RedirectToPage("/AttendanceCheck","CheckUpValues",preCheck);
                }
                return Page();
            }
            catch (Exception ex)
            {

                return RedirectToPage("/Index");
            }

        }

    }
}
