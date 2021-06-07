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
    public class IndexModel : PageModel
    {
        private readonly RGAttendanceService_V00.DAL.ParentContext _context;

        public IndexModel(RGAttendanceService_V00.DAL.ParentContext context)
        {
            _context = context;
        }

        public IList<Coach> Coach { get;set; }

        public async Task OnGetAsync()
        {
            Coach = await _context.Coach.ToListAsync();
        }
    }
}
