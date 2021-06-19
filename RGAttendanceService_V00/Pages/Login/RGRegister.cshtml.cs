using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RGAttendanceService_V00.Models;
using RGAttendanceService_V00.DAL;
using Microsoft.AspNetCore.Mvc.Rendering;
using RGAttendanceService_V00.DAL.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace RGAttendanceService_V00.Pages.Login
{
    public class RGRegisterModel : PageModel
    {
        private ParentContext _context;
        private IUser _UserDB;
        [BindProperty]
        public UserModel RGUser { get; set; }
        public List<Coach> Coaches { get; set; }
        public RGRegisterModel(ParentContext context, IUser UserDB)
        {
            _context = context;
            _UserDB = UserDB;
            Coaches = new List<Coach>();
        }

        public IActionResult OnGet()
        {
            Coaches = _context.Coach.ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(UserModel RGUser)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    if(RGUser.CoachId==0)
                    {
                        RGUser.CoachId = null;
                    }

                    var claims = new List<Claim>()
                    {
                    new Claim(ClaimTypes.Name,RGUser.UserName),
                    new Claim(ClaimTypes.Email,RGUser.Email),
                    new Claim(ClaimTypes.Sid,Convert.ToString(RGUser.Id))
                    };

                    var ClaimsIdentity = new ClaimsIdentity(claims, "CookieAuthentication");
                    await HttpContext.SignInAsync("CookieAuthentication", new ClaimsPrincipal(ClaimsIdentity));

                    _UserDB.Add(RGUser);
                    return RedirectToPage("../Index");
                }
                return Page();
            }
            catch (Exception ex)
            {
                return RedirectToPage("../Index");
                throw;
            }
        }
    }
}
