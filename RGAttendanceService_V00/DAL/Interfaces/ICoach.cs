using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RGAttendanceService_V00.Models;

namespace RGAttendanceService_V00.DAL
{
    public interface ICoach
    {
        public List<Coach> GetList();
        public Coach Get(int _id);
        public int Add(Coach _coach);
        public int Update(Coach _coach);
        public int Delete(int _id);
    }
}
