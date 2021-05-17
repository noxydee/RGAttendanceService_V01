using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RGAttendanceService_V00.Models;

namespace RGAttendanceService_V00.DAL.Interfaces
{
    public interface IUser
    {
        public List<UserModel> List();
        public UserModel Get(int _id);
        public int Add(UserModel _siteuser);
        public int Delete(int _id);
        public int Update(UserModel _siteuser);
    }
}
