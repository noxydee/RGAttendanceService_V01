using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using RGAttendanceService_V00.Models;


namespace RGAttendanceService_V00.DAL
{
    public class AttendanceSession
    {
        private List<Attendance> AttendanceList;

        public void Load(string JsonAttendance)
        {
            if(JsonAttendance == null)
            {
                AttendanceList = new List<Attendance>();
            }
            else
            {
                AttendanceList = JsonSerializer.Deserialize<List<Attendance>>(JsonAttendance);
            }
        }
        public string Save()
        {
            return JsonSerializer.Serialize(AttendanceList);
        }

        public void AddAttendance(List<Attendance> AttendanceList)
        {
            this.AttendanceList = AttendanceList;
        }

        public List<Attendance> List()
        {
            return AttendanceList;
        }

    }
}
