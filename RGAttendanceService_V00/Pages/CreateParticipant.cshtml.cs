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
    public class CreateParticipantModel : PageModel
    {
        private IConfiguration _configuration;
        private IParticipant ParticipantDB;

        public List<Group> GroupList = new List<Group>();

        [BindProperty]
        public Participant NewParticipant { get; set; }
        public CreateParticipantModel(IConfiguration _configuration,IParticipant ParticipantDB)
        {
            this._configuration = _configuration;
            this.ParticipantDB = ParticipantDB;
        }
        public void OnGet()
        {

        }

        public IActionResult OnPost(Participant NewParticipant)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    System.Diagnostics.Debug.WriteLine(JsonSerializer.Serialize(NewParticipant));
                    ParticipantDB.Add(NewParticipant);
                    return RedirectToPage("/Index");
                }
                return Page();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return RedirectToPage("/CreateParticipant");
            }
        }


    }
}
