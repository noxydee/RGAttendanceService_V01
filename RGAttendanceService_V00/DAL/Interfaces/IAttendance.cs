using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RGAttendanceService_V00.Models;

namespace RGAttendanceService_V00.DAL.Interfaces
{
    public interface IAttendance
    {
        public List<Attendance> GetList();
        public Attendance Get(int _id);
        public int AddAttendance(Attendance attendance);
        public int UpdateAttendance(Attendance attendance);
        public int DeleteAttendance(int _id);
        public List<Attendance> GetParticipantAttendanceList(int _id);
    }
}
