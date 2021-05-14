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
        private IGroup GroupDB;
        public Participant EditParticipant { get; set; }

        public List<Group> GroupList = new List<Group>();

        public EditParticipantModel(IConfiguration _configuartion,IParticipant ParticipantDB,IGroup GroupDB)
        {
            this._configuartion = _configuartion;
            this.ParticipantDB = ParticipantDB;
            this.GroupDB = GroupDB;
            GroupList = GroupDB.GetList();
        }
        public void OnGet(int Id)
        {
            EditParticipant = ParticipantDB.Get(Id);

        }

        public IActionResult OnPost (Participant EditParticipant)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    ParticipantDB.Update(EditParticipant);
                    return RedirectToPage("ParticipantsList");
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
