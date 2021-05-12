using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RGAttendanceService_V00.Models;

namespace RGAttendanceService_V00.DAL
{
    public interface IParticipant
    {
        public List<Participant> GetList();
        public Participant Get(int _id);
        public int Add(Participant _participant);
        public int Update(Participant _participant);
        public int Delete(int _id);
    }
}
