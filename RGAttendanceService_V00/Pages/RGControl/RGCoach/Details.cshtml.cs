using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RGAttendanceService_V00.DAL;
using RGAttendanceService_V00.Models;

namespace RGAttendanceService_V00.Pages.RGControl.RGCoach
{
    public class DetailsModel : PageModel
    {
        private readonly RGAttendanceService_V00.DAL.ParentContext _context;

        public DetailsModel(RGAttendanceService_V00.DAL.ParentContext context)
        {
            _context = context;
        }

        public Coach Coach { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Coach = await _context.Coach.FirstOrDefaultAsync(m => m.Id == id);

            if (Coach == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
