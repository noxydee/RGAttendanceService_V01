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

namespace RGAttendanceService_V00.Pages
{
    public class AttendanceCheckModel : PageModel
    {
        private IConfiguration _configuration;
        private IParticipant ParticipantDB;
        private IGroup GroupDB;
        private ICoach CoachDB;
        private IAttendance AttendanceDB;

        public List<Participant> List = new List<Participant>();
        public List<Attendance> AttendanceList = new List<Attendance>();
        public Group Group { get; set; }

        public static DateTime DateOfClass;
        public static int GroupIdx;
        public static int CoachIdx;
        public AttendanceCheckModel(IConfiguration _configuration, IParticipant ParticipantDB, IGroup GroupDB, ICoach CoachDB, IAttendance AttendanceDB)
        {
            this._configuration = _configuration;
            this.ParticipantDB = ParticipantDB;
            this.GroupDB = GroupDB;
            this.CoachDB = CoachDB;
            this.AttendanceDB = AttendanceDB;
        }


        public void OnGet(int GroupId)
        {
            List = ParticipantDB.GetListByGroupId(GroupId);

            for(int i=0; i<List.Count; i++)
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

        public void OnPost(List<Attendance> AttendanceList)
        {
            foreach(Attendance x in AttendanceList)
            {
                x.Participant = ParticipantDB.Get(x.ParticipantId);

                x.DateOfCheck = DateTime.Now;
                x.DateOfClass = DateOfClass;

                x.GroupId = GroupIdx;
                x.ParticipatingGroup = GroupDB.Get(GroupIdx);

                x.CheckerId = CoachIdx;
                x.Checker = CoachDB.Get(CoachIdx);

                AttendanceDB.AddAttendance(x);
            }

        }
    }
}
