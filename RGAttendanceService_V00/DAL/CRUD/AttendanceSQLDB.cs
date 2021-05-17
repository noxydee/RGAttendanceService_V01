using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RGAttendanceService_V00.DAL;
using RGAttendanceService_V00.DAL.Interfaces;
using RGAttendanceService_V00.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace RGAttendanceService_V00.DAL.CRUD
{
    public class AttendanceSQLDB : IAttendance
    {
        private IConfiguration _configuration;
        private String ConnectionString;
        SqlConnection Connection = new SqlConnection();

        public AttendanceSQLDB(IConfiguration _configuration)
        {
            this._configuration = _configuration;
            ConnectionString = _configuration.GetConnectionString("RGAttendanceService");
            Connection.ConnectionString = ConnectionString;
        }
        private int ConvertToBit(bool status)
        {
            return status == true ? 1 : 0;
        }

        public int AddAttendance(Attendance attendance)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_AddAttendanceRecord", Connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter IdParam = new SqlParameter("@Id", SqlDbType.Int);
                IdParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(IdParam);

                SqlParameter ParticipantIdParam = new SqlParameter("@ParticipantId", SqlDbType.Int);
                ParticipantIdParam.Value = attendance.ParticipantId;
                cmd.Parameters.Add(ParticipantIdParam);

                SqlParameter GroupIdParam = new SqlParameter("@GroupId", SqlDbType.Int);
                GroupIdParam.Value = attendance.GroupId;
                cmd.Parameters.Add(GroupIdParam);

                SqlParameter DateOfClassParam = new SqlParameter("@DateOfClass", SqlDbType.DateTime);
                DateOfClassParam.Value = attendance.DateOfClass;
                cmd.Parameters.Add(DateOfClassParam);

                SqlParameter DateOfCheckParam = new SqlParameter("@DateOfCheck", SqlDbType.DateTime);
                DateOfCheckParam.Value = (attendance.DateOfCheck ?? (object)DBNull.Value);
                cmd.Parameters.Add(DateOfCheckParam);

                SqlParameter AbsenceStatusParam = new SqlParameter("@PresentStatus", SqlDbType.Bit);
                AbsenceStatusParam.Value = attendance.AbsenceStatus;
                cmd.Parameters.Add(AbsenceStatusParam);

                SqlParameter AbsenceInfoParam = new SqlParameter("@AbsenceInfo", SqlDbType.NVarChar, 500);
                AbsenceInfoParam.Value = (attendance.AbsenceInfo ?? (object)DBNull.Value);
                cmd.Parameters.Add(AbsenceInfoParam);

                SqlParameter CheckerIdParam = new SqlParameter("@CheckerId", SqlDbType.Int);
                CheckerIdParam.Value = attendance.CheckerId;
                cmd.Parameters.Add(CheckerIdParam);

                Connection.Open();
                cmd.ExecuteNonQuery();
                Connection.Close();
                return 0;
            }
            catch (Exception ex)
            {
                Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return 1;
            }
        }

        public int DeleteAttendance(int _id)
        {
            throw new NotImplementedException();
        }

        public Attendance Get(int _id)
        {
            throw new NotImplementedException();
        }

        public List<Attendance> GetList()
        {
            throw new NotImplementedException();
        }

        public int UpdateAttendance(Attendance attendance)
        {
            throw new NotImplementedException();
        }
    }
}
