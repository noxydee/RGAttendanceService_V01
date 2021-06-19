using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using RGAttendanceService_V00.DAL;
using RGAttendanceService_V00.Models;
using RGAttendanceService_V00.DAL.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Text.Json;


namespace RGAttendanceService_V00.Pages.Login
{
   
    public class RGLoginModel : PageModel
    {
        private IConfiguration _configuration;
        private IUser UserDB;

        [BindProperty]
        public UserModel RGUser { get; set; }
        private List<UserModel> ListOfUsers { get; set; }
        private int IdOfFoundUser;

        public RGLoginModel(IConfiguration _configuration, IUser UserDB)
        {
            this._configuration = _configuration;
            this.UserDB = UserDB;
            ListOfUsers = UserDB.List();
        }
        private bool ValidateUser(UserModel RGUser)
        {
            PasswordHasher<string> PasswordHasher = new PasswordHasher<string>();

            foreach(UserModel checker in ListOfUsers)
            {
                if((RGUser.UserName == checker.UserName || RGUser.Email == checker.Email) && PasswordHasher.VerifyHashedPassword(RGUser.UserName,checker.Password,RGUser.Password)== PasswordVerificationResult.Success)
                {
                    IdOfFoundUser = checker.Id;
                    return true;
                }
            }
            return false;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            try
            {
                if(RGUser.Email == null)
                {
                    RGUser.Email = RGUser.UserName;
                }
                if (ValidateUser(RGUser))
                {
                    UserModel FoundUser = UserDB.Get(IdOfFoundUser);
                    var claims = new List<Claim>()
                    {
                    new Claim(ClaimTypes.Name,FoundUser.UserName),
                    new Claim(ClaimTypes.Email,FoundUser.Email),
                    new Claim(ClaimTypes.Sid,Convert.ToString(FoundUser.Id))
                    };

                    var ClaimsIdentity = new ClaimsIdentity(claims, "CookieAuthentication");
                    await HttpContext.SignInAsync("CookieAuthentication", new ClaimsPrincipal(ClaimsIdentity));

                    if (returnUrl == null)
                    {
                        return RedirectToPage("../Index");
                    }
                    return Redirect(returnUrl);
                }
                return Page();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return Page();
            }
        }
        

        public void OnGet()
        {

        }
    }
}
