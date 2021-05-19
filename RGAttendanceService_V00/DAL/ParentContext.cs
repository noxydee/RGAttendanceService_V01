using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RGAttendanceService_V00.Models;
using Microsoft.EntityFrameworkCore;

namespace RGAttendanceService_V00.DAL
{
    public class ParentContext : DbContext
    {
        public ParentContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Parent> Parent { get; set; }
    }
}
