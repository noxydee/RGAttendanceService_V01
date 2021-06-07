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

namespace RGAttendanceService_V00.DAL
{
    public class AtendanceSQLDBEntity : IAttendance
    {
        private IConfiguration _configuration;
        private String ConnectionString;
        SqlConnection Connection = new SqlConnection();

        public AtendanceSQLDBEntity(IConfiguration _configuration)
        {
            this._configuration = _configuration;
            ConnectionString = _configuration.GetConnectionString("RGAttendanceServiceEntity");
            Connection.ConnectionString = ConnectionString;
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
                //cmd.ExecuteNonQuery();
                int ID = Convert.ToInt32(cmd.ExecuteScalar());
                Connection.Close();
                return ID;
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
            System.Diagnostics.Debug.WriteLine("insideGetList");
            try
            {
                List<Attendance> List = new List<Attendance>();
                SqlCommand cmd = new SqlCommand("sp_GetAttendanceList", Connection);
                cmd.CommandType = CommandType.StoredProcedure;
                Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    Attendance x = new Attendance
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        ParticipantId = Convert.ToInt32(reader["ParticipantId"]),
                        GroupId = Convert.ToInt32(reader["GroupId"]),
                        DateOfClass = Convert.ToDateTime(reader["DateOfClass"]),
                        DateOfCheck = reader["DateOfCheck"] == DBNull.Value ? null : Convert.ToDateTime(reader["DateOfCheck"]),
                        AbsenceStatus = Convert.ToBoolean(reader["AbsenceStatus"]),
                        AbsenceInfo = reader["AbsenceInfo"] == DBNull.Value ? null : Convert.ToString(reader["AbsenceInfo"]),
                        CheckerId = reader["CheckerId"] == DBNull.Value ? null : Convert.ToInt32(reader["CheckerId"])
                    };
                    System.Diagnostics.Debug.WriteLine(x.DateOfClass);
                    List.Add(x);
                }

                Connection.Close();
                return List;
            }
            catch (Exception ex)
            {
                Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
                throw;
            }
        }

        public List<Attendance> GetParticipantAttendanceList(int _id)
        {
            try
            {
                List<Attendance> List = new List<Attendance>();
                SqlCommand cmd = new SqlCommand("sp_GetAttendanceByParticipant", Connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter IdParam = new SqlParameter("@Id", SqlDbType.Int);
                IdParam.Value = _id;
                cmd.Parameters.Add(IdParam);

                Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Attendance x = new Attendance
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        ParticipantId = Convert.ToInt32(reader["ParticipantId"]),
                        GroupId = Convert.ToInt32(reader["GroupId"]),
                        DateOfClass = Convert.ToDateTime(reader["DateOfClass"]),
                        DateOfCheck = reader["DateOfCheck"] == DBNull.Value ? null : Convert.ToDateTime(reader["DateOfCheck"]),
                        AbsenceStatus = Convert.ToBoolean(reader["AbsenceStatus"]),
                        AbsenceInfo = reader["AbsenceInfo"] == DBNull.Value ? null : Convert.ToString(reader["AbsenceInfo"]),
                        CheckerId = reader["CheckerId"] == DBNull.Value ? null : Convert.ToInt32(reader["CheckerId"])
                    };
                    List.Add(x);
                }

                Connection.Close();
                return List;
            }
            catch (Exception ex)
            {
                Connection.Close();
                return null;
            }
        }

        public int UpdateAttendance(Attendance attendance)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_UpdateAttendance", Connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter IdParam = new SqlParameter("@Id", SqlDbType.Int);
                IdParam.Value = attendance.Id;
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
                System.Diagnostics.Debug.WriteLine(ex.Message);
                Connection.Close();
                return 1;
            }
        }
    }
}
