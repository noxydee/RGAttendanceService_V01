using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RGAttendanceService_V00.DAL;
using RGAttendanceService_V00.Models;

namespace RGAttendanceService_V00.Pages.RGControl.RGRelation
{
    public class EditModel : PageModel
    {
        private readonly RGAttendanceService_V00.DAL.ParentContext _context;

        public EditModel(RGAttendanceService_V00.DAL.ParentContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ParticipantParents ParticipantParents { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ParticipantParents = await _context.ParticipantParents
                .Include(p => p.Kid)
                .Include(p => p.Parent).FirstOrDefaultAsync(m => m.ParticipantId == id);

            if (ParticipantParents == null)
            {
                return NotFound();
            }
           ViewData["ParticipantId"] = new SelectList(_context.Participant, "Id", "FirstName");
           ViewData["ParentId"] = new SelectList(_context.Parent, "Id", "FirstName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ParticipantParents).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticipantParentsExists(ParticipantParents.ParticipantId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ParticipantParentsExists(int id)
        {
            return _context.ParticipantParents.Any(e => e.ParticipantId == id);
        }
    }
}
