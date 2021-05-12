using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RGAttendanceService_V00.Models;

namespace RGAttendanceService_V00.DAL
{
    public interface IGroup
    {
        public List<Group> GetList();
        public Group Get(int _id);
        public int Add(Group _group);
        public int Update(Group _group);
        public int Delete(int _id);
    }
}
