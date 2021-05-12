using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RGAttendanceService_V00.Models;
using RGAttendanceService_V00.DAL;
using Microsoft.Extensions.Configuration;

namespace RGAttendanceService_V00.Pages
{
    public class ParticipantsListModel : PageModel
    {
        private IConfiguration _configuration;
        private IParticipant ParticipantDB;

        [BindProperty]
        public List<Participant> List { get; set; }
        public List<Group> GroupList { get; set; }
        public ParticipantsListModel(IConfiguration _configuration, IParticipant ParticipantDB)
        {
            this._configuration = _configuration;
            this.ParticipantDB = ParticipantDB;
            List = ParticipantDB.GetList();
        }
        public void OnGet()
        {



        }
    }
}
