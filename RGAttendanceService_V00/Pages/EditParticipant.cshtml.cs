using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RGAttendanceService_V00.Models;
using RGAttendanceService_V00.DAL;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace RGAttendanceService_V00.Pages
{
    public class EditParticipantModel : PageModel
    {
        private IConfiguration _configuartion;
        private IParticipant ParticipantDB;
        [BindProperty]
        public Participant EditParticipant { get; set; }

        public List<Group> GroupList = new List<Group>();

        public EditParticipantModel(IConfiguration _configuartion,IParticipant ParticipantDB)
        {
            this._configuartion = _configuartion;
            this.ParticipantDB = ParticipantDB;
        }
        public void OnGet(int _id)
        {
            EditParticipant = ParticipantDB.Get(_id);

        }
    }
}
