using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RGAttendanceService_V00.Models;
using RGAttendanceService_V00.DAL;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace RGAttendanceService_V00.DataAnnotations
{
    public class CoachAttributes : ValidationAttribute 
    {
        public ParentContext AttributeContext { get; set; }
        
        public List<Coach> CoachList { get; set; }
        public CoachAttributes(ParentContext context)
        {
            
        }


        public override bool IsValid(object value)
        {
            return false;
        }
    }
}
