using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using RGAttendanceService_V00.DAL;
using RGAttendanceService_V00.Models;
using System.Text.Json;
using RGAttendanceService_V00.DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace RGAttendanceService_V00.Pages
{
    public class AttendanceCheckModel : PageModel
    {
        private IConfiguration _configuration;
        private IParticipant ParticipantDB;
        private IGroup GroupDB;
        private ICoach CoachDB;
        private IAttendance AttendanceDB;
        private ParentContext _context;

        public List<Participant> List = new List<Participant>();
        public List<Attendance> AttendanceList = new List<Attendance>();
        public AttendanceSession LastChecked = new AttendanceSession();
        public Group Group { get; set; }

        public string JsonSession { get; set; }

        public static DateTime DateOfClass;
        public static int GroupIdx;
        public static int? CoachIdx;
        public AttendanceCheckModel(IConfiguration _configuration, IParticipant ParticipantDB, IGroup GroupDB, ICoach CoachDB, IAttendance AttendanceDB,ParentContext context)
        {
            this._configuration = _configuration;
            this.ParticipantDB = ParticipantDB;
            this.GroupDB = GroupDB;
            this.CoachDB = CoachDB;
            this.AttendanceDB = AttendanceDB;
            _context = context;
        }


        public void OnGet(int GroupId)
        {
            //List = ParticipantDB.GetListByGroupId(GroupId);

            List = _context.Participant.Where(p => p.GroupId.Equals(GroupId)).ToList();

            for (int i=0; i<List.Count; i++)
            {
                Attendance x = new Attendance();
                x.Participant = List[i];
                x.ParticipantId = List[i].Id;
                AttendanceList.Add(x);
            }
        }

        public void OnGetCheckUpValues(PreCheckModel preCheck)
        {
            DateOfClass = preCheck.Date;
            GroupIdx = preCheck.GroupId;
            CoachIdx = preCheck.CoachId;
            OnGet(preCheck.GroupId);
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

        public IActionResult OnPost(List<Attendance> AttendanceList)
        {
            LoadSession();
            LastChecked.AddAttendance(AttendanceList);
            SaveSassion();

            foreach (Attendance x in AttendanceList)
            {
                x.Participant = _context.Participant
                .FirstOrDefault(m => m.Id.Equals(x.ParticipantId));

                x.DateOfCheck = DateTime.Now;
                x.DateOfClass = DateOfClass;

                x.GroupId = GroupIdx;
                x.ParticipatingGroup = _context.Group
                .Include(@a => @a.Coach).FirstOrDefault(m=>m.Id.Equals(GroupIdx));//GroupDB.Get(GroupIdx);

                x.CheckerId = CoachIdx ?? null;
                x.Checker = _context.Coach.FirstOrDefault(c=>c.Id.Equals(CoachIdx));//CoachDB.Get(CoachIdx ?? 0);

                x.Id=AttendanceDB.AddAttendance(x);
            }

            foreach (Attendance x in AttendanceList)
            {
                x.Participant = null;

                x.ParticipatingGroup = null;

                x.Checker = null;
            }

            LoadSession();
            LastChecked.AddAttendance(AttendanceList);
            SaveSassion();

            return RedirectToPage("/index");

        }
    }
}
