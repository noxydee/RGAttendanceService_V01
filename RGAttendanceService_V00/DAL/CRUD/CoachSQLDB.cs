using RGAttendanceService_V00.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace RGAttendanceService_V00.DAL.CRUD
{
    public class CoachSQLDB : ICoach
    {
        private IConfiguration _configuration;
        private string ConnectionString;
        SqlConnection Connection = new SqlConnection();
        public CoachSQLDB(IConfiguration _configuration)
        {
            this._configuration = _configuration;
            ConnectionString = _configuration.GetConnectionString("RGAttendanceService");
            Connection.ConnectionString = ConnectionString;
        }

        public int Add(Coach _coach)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_AddCoach", Connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter IdParam = new SqlParameter("@Id", SqlDbType.Int);
                IdParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(IdParam);

                SqlParameter FirstNameParam = new SqlParameter("@FirstName", SqlDbType.NVarChar, 50);
                FirstNameParam.Value = _coach.FirstName;
                cmd.Parameters.Add(FirstNameParam);

                SqlParameter LastNameParam = new SqlParameter("@LastName", SqlDbType.NVarChar, 100);
                LastNameParam.Value = _coach.LastName;
                cmd.Parameters.Add(LastNameParam);

                SqlParameter AgeParam = new SqlParameter("@Age", SqlDbType.Int);
                AgeParam.Value = _coach.Age;
                cmd.Parameters.Add(AgeParam);

                Connection.Open();
                cmd.ExecuteNonQuery();
                Connection.Close();
            }
            catch (Exception ex)
            {

                return 1;
            }
            return 0;
        }

        public int Delete(int _id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_DeleteCoach", Connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter IdParam = new SqlParameter("@Id", SqlDbType.Int);
                IdParam.Value = _id;
                cmd.Parameters.Add(IdParam);

                Connection.Open();
                cmd.ExecuteNonQuery();
                Connection.Close();
            }
            catch (Exception ex)
            {

                return 1;
            }
            return 0;
        }

        public Coach Get(int _id)
        {
            try
            {
                Coach TargetCoach = new Coach();
                SqlCommand cmd = new SqlCommand("sp_GetCoach", Connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter IdParam = new SqlParameter("@Id", SqlDbType.Int);
                IdParam.Value = _id;
                cmd.Parameters.Add(IdParam);

                Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    TargetCoach.Id = Convert.ToInt32(reader["Id"]);
                    TargetCoach.FirstName = Convert.ToString(reader["FirstName"]);
                    TargetCoach.LastName = Convert.ToString(reader["LastName"]);
                    TargetCoach.Age = Convert.ToInt32(reader["Age"]);
                }
                Connection.Close();
                return TargetCoach;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<Coach> GetList()
        {
            try
            {
                List<Coach> TargetList = new List<Coach>();
                SqlCommand cmd = new SqlCommand("sp_GetCoachList", Connection);

                Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    Coach x = new Coach();
                    x.Id = Convert.ToInt32(reader["Id"]);
                    x.FirstName = Convert.ToString(reader["FirstName"]);
                    x.LastName = Convert.ToString(reader["LastName"]);
                    x.Age = Convert.ToInt32(reader["Age"]);
                    TargetList.Add(x);
                }
                Connection.Close();
                return TargetList;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public int Update(Coach _coach)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_EditCoach", Connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter IdParam = new SqlParameter("@Id", SqlDbType.Int);
                IdParam.Value = _coach.Id;
                cmd.Parameters.Add(IdParam);

                SqlParameter FirstNameParam = new SqlParameter("@FirstName", SqlDbType.NVarChar, 50);
                FirstNameParam.Value = _coach.FirstName;
                cmd.Parameters.Add(FirstNameParam);

                SqlParameter LastNameParam = new SqlParameter("@LastName", SqlDbType.NVarChar, 100);
                LastNameParam.Value = _coach.LastName;
                cmd.Parameters.Add(LastNameParam);

                SqlParameter AgeParam = new SqlParameter("@Age", SqlDbType.Int);
                AgeParam.Value = _coach.Age;
                cmd.Parameters.Add(AgeParam);

                Connection.Open();
                cmd.ExecuteNonQuery();
                Connection.Close();
            }
            catch (Exception ex)
            {

                return 1;
            }
            return 0;
        }
    }
}
