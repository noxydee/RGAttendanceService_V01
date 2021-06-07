using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RGAttendanceService_V00.Models;
using RGAttendanceService_V00.DAL;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using RGAttendanceService_V00.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace RGAttendanceService_V00.Pages
{
    public class LastCheckedModel : PageModel
    {
        private ParentContext _context;
        private IAttendance AttendanceDB;
        private IUser UserDB;

        public List<Participant> List = new List<Participant>();
        public List<Attendance> AttendanceList = new List<Attendance>();
        public Group Group { get; set; }
        public AttendanceSession LastChecked;

        public string JsonSession { get; set; }

        public LastCheckedModel(ParentContext context,IAttendance AttendanceDB,IUser UserDB)
        {
            LastChecked = new AttendanceSession();
            _context = context;
            this.AttendanceDB = AttendanceDB;
            this.UserDB = UserDB;
        }
        

        private void LoadSession()
        {
            JsonSession = HttpContext.Session.GetString("JsonSession");
            LastChecked.Load(JsonSession);
        }
        private void SaveSassion()
        {
            JsonSession = LastChecked.Save();
            HttpContext.Session.SetString("JsonSession", JsonSession);
        }

        public void OnGet()
        {
            LoadSession();
            AttendanceList=LastChecked.List();
            SaveSassion();

            foreach(Attendance x in AttendanceList)
            {
                x.Participant = _context.Participant
                .FirstOrDefault(m => m.Id.Equals(x.ParticipantId));
                x.ParticipatingGroup = _context.Group.FirstOrDefault(g => g.Id.Equals(x.GroupId));
                x.Checker = _context.Coach.FirstOrDefault(c => c.Id.Equals(x.CheckerId));
            }
        }

        public IActionResult OnPost(List<Attendance> AttendanceList)
        {
            
            int UserId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value);
            UserModel userx = UserDB.Get(UserId);

            foreach (Attendance x in AttendanceList)
            {
                x.CheckerId = userx.CoachId ?? null;
                x.DateOfCheck = DateTime.Now;

                x.DateOfClass = DateTime.Now;

                AttendanceDB.UpdateAttendance(x);
            }

            LoadSession();
            LastChecked.AddAttendance(AttendanceList);
            SaveSassion();

            return RedirectToPage("/index");

        }
    }
}
