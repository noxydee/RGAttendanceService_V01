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
using Microsoft.EntityFrameworkCore;

namespace RGAttendanceService_V00.Pages
{
    public class AttendanceCheckUpModel : PageModel
    {
        private IConfiguration _configuration;
        private IGroup GroupDB;
        private ICoach CoachDB;
        private IUser UserDB;
        private ParentContext _context;

        [BindProperty]
        public PreCheckModel PreCheck { get; set; }
        public IList<Group> GroupList { get; set; }
        public AttendanceCheckUpModel(IConfiguration _configuration,IGroup GroupDB,ICoach CoachDB,IUser UserDB,ParentContext context)
        {
            this._configuration = _configuration;
            this.GroupDB = GroupDB;
            this.CoachDB = CoachDB;
            this.UserDB = UserDB;
            _context = context;

            GroupList = _context.Group
                .Include(@a => @a.Coach).ToList();
            
        }

        public async Task OnGetAsync()
        {
            GroupList = await _context.Group
                .Include(@a => @a.Coach).ToListAsync();
        }

        public IActionResult OnPost(PreCheckModel PreCheck)
        {
            try
            {
                int UserId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value);
                UserModel x = UserDB.Get(UserId);

                PreCheck.CoachId = x.CoachId;

                if (ModelState.IsValid)
                {
                    return RedirectToPage("/AttendanceCheck", "CheckUpValues", PreCheck);
                }
                else if(!ModelState.IsValid)
                {
                    //System.Diagnostics.Debug.WriteLine();
                    return Page();
                }
                
                return RedirectToPage("/AttendanceCheckUp");
            }
            catch (Exception ex)
            {
                return RedirectToPage("/Index");
            }

        }

    }
}
