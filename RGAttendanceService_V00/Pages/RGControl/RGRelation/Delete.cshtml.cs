using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RGAttendanceService_V00.DAL;
using RGAttendanceService_V00.Models;

namespace RGAttendanceService_V00.Pages.RGControl.RGRelation
{
    public class DeleteModel : PageModel
    {
        private readonly RGAttendanceService_V00.DAL.ParentContext _context;

        public DeleteModel(RGAttendanceService_V00.DAL.ParentContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ParticipantParents = await _context.ParticipantParents.FindAsync(id);

            if (ParticipantParents != null)
            {
                _context.ParticipantParents.Remove(ParticipantParents);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
