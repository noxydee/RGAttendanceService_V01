using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RGAttendanceService_V00.DAL;
using RGAttendanceService_V00.Models;
using RGAttendanceService_V00.DAL.Interfaces;
using System.Text.Json;

namespace RGAttendanceService_V00.Pages.RGControl.RGParticipant
{
    public class DetailsModel : PageModel
    {
        private readonly RGAttendanceService_V00.DAL.ParentContext _context;
        private IAttendance AttendanceDB;
        public List<Attendance> AttendanceList { get; set; }
        public List<Parent> ParentList = new List<Parent>();

        public DetailsModel(RGAttendanceService_V00.DAL.ParentContext context,IAttendance AttendanceDB)
        {
            _context = context;
            this.AttendanceDB = AttendanceDB;
        }

        public Participant Participant { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            AttendanceList = AttendanceDB.GetParticipantAttendanceList(id ?? default(int));

            Participant = await _context.Participant
                .Include(p => p.Group).FirstOrDefaultAsync(m => m.Id == id);


            List<ParticipantParents> RelationList = _context.ParticipantParents.Where(o => o.ParticipantId.Equals(id)).ToList();

            foreach(ParticipantParents x in RelationList)
            {
                ParentList.Add(_context.Parent.FirstOrDefault(p => p.Id.Equals(x.ParentId)));
            }
               
            

            if (Participant == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnGetAddx(int id)
        {
            Participant Participantx = _context.Participant.FirstOrDefault(p => p.Id.Equals(id));
            return RedirectToPage("../RGRelation/Create", "AddToParticipant", Participantx);
        }
    }
}
