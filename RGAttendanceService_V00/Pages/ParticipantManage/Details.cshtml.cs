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

namespace RGAttendanceService_V00.Pages.ParticipantManage
{
    public class DetailsModel : PageModel
    {
        private IAttendance AttendanceDB;
        private IConfiguration _configuration;
        private IParticipant ParticipantDB;

        [BindProperty]
        public Participant DetailParticipant { get; set; }
        public List<Attendance> AttendanceList { get; set; }

        public DetailsModel(IConfiguration _configuration,IAttendance AttendanceDB,IParticipant ParticipantDB)
        {
            this._configuration = _configuration;
            this.AttendanceDB = AttendanceDB;
            this.ParticipantDB = ParticipantDB;
        }
        public void OnGet(int Id)
        {
            DetailParticipant = ParticipantDB.Get(Id);
            AttendanceList = AttendanceDB.GetParticipantAttendanceList(Id);

        }
    }
}
