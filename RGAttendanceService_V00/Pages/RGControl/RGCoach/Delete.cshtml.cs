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

namespace RGAttendanceService_V00.Pages.RGControl.RGCoach
{
    public class DeleteModel : PageModel
    {
        private readonly RGAttendanceService_V00.DAL.ParentContext _context;
        private IAttendance _AttendanceDB;

        public DeleteModel(RGAttendanceService_V00.DAL.ParentContext context,IAttendance _AttendanceDB)
        {
            _context = context;
            this._AttendanceDB = _AttendanceDB;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Coach = await _context.Coach.FindAsync(id);

            if (Coach != null)
            {
                //_AttendanceDB.SetCheckerId_Null(Coach.Id);
                _context.Coach.Remove(Coach);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
