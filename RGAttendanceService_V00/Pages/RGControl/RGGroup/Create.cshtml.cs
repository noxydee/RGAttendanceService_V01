using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RGAttendanceService_V00.DAL;
using RGAttendanceService_V00.Models;

namespace RGAttendanceService_V00.Pages.RGControl.RGGroup
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
            var items = new SelectList(_context.Coach.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.FirstName + " " + s.LastName }), "Value", "Text").ToList();
            items.Insert(0, new SelectListItem { Value = "0", Text = "Brak" });
            ViewData["CoachId"] = items;
            return Page();
        }

        [BindProperty]
        public Group Group { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Group.CoachId = (Group.CoachId == 0 ? null : Group.CoachId);
            _context.Group.Add(Group);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
