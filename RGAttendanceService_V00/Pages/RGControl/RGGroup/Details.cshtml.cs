using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RGAttendanceService_V00.DAL;
using RGAttendanceService_V00.Models;

namespace RGAttendanceService_V00.Pages.RGControl.RGGroup
{
    public class DetailsModel : PageModel
    {
        private readonly RGAttendanceService_V00.DAL.ParentContext _context;
        public List<Participant> Participants { get; set; }

        public DetailsModel(RGAttendanceService_V00.DAL.ParentContext context)
        {
            _context = context;
        }

        public Group Group { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Group = await _context.Group
                .Include(@a => @a.Coach).FirstOrDefaultAsync(m => m.Id == id);

            Participants = await _context.Participant.Where(p => p.GroupId.Equals(id)).ToListAsync();

            if (Group == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
