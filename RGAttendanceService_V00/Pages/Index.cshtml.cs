using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RGAttendanceService_V00.DAL.Interfaces;
using RGAttendanceService_V00.Models;

namespace RGAttendanceService_V00.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private IUser userDB;

        public IndexModel(ILogger<IndexModel> logger,IUser userDB)
        {
            _logger = logger;
            this.userDB = userDB;
        }

        public void OnGet()
        {
           
        }
    }
}
