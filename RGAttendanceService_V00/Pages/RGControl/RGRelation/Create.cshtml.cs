using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RGAttendanceService_V00.DAL;
using RGAttendanceService_V00.Models;

namespace RGAttendanceService_V00.Pages.RGControl.RGRelation
{
    public class CreateModel : PageModel
    {
        private readonly RGAttendanceService_V00.DAL.ParentContext _context;

        public CreateModel(RGAttendanceService_V00.DAL.ParentContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["ParticipantId"] = new SelectList(_context.Participant, "Id", "FirstName");
            ViewData["ParentId"] = new SelectList(_context.Parent, "Id", "FirstName");
            return Page();
        }

        [BindProperty]
        public ParticipantParents ParticipantParents { get; set; }
        public void OnGetAddToParticipant(Participant Participantx)
        {

            int id = Participantx.Id;
            var Participantxx = _context.Participant.Where(p => p.Id.Equals(id));

            ViewData["ParticipantId"] = new SelectList(Participantxx, "Id", "FirstName");

            ViewData["ParentId"] = new SelectList(_context.Parent, "Id", "FirstName");
            //return Page();
        }

        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ParticipantParents.Add(ParticipantParents);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
