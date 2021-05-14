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
                SqlParameter FirstNameParam = new SqlParameter("@FirstName", SqlDbType.NVarChar, 50);
                SqlParameter LastNameParam = new SqlParameter("@LastName", SqlDbType.NVarChar, 100);
                SqlParameter AgeParam = new SqlParameter("@Age", SqlDbType.Int);

                IdParam.Direction = ParameterDirection.Output;

                FirstNameParam.Value = _coach.FirstName;
                LastNameParam.Value = _coach.LastName;
                AgeParam.Value = (_coach.Age ?? (object)DBNull.Value);

                cmd.Parameters.Add(IdParam);
                cmd.Parameters.Add(FirstNameParam);
                cmd.Parameters.Add(LastNameParam);
                cmd.Parameters.Add(AgeParam);

                Connection.Open();
                cmd.ExecuteNonQuery();
                Connection.Close();
            }
            catch (Exception ex)
            {
                Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
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
                    TargetCoach.Age = reader["Age"] == DBNull.Value ? null : Convert.ToInt32(reader["Age"]);
                }
                Connection.Close();
                return TargetCoach;
            }
            catch (Exception ex)
            {
                Connection.Close();
                return null;
            }
        }

        public List<Coach> GetList()
        {
            try
            {
                List<Coach> TargetList = new List<Coach>();
                SqlCommand cmd = new SqlCommand("sp_GetCoachList", Connection);
                cmd.CommandType = CommandType.StoredProcedure;

                Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    Coach x = new Coach();
                    x.Id = Convert.ToInt32(reader["Id"]);
                    x.FirstName = Convert.ToString(reader["FirstName"]);
                    x.LastName = Convert.ToString(reader["LastName"]);
                    x.Age = reader["Age"] == DBNull.Value ? null : Convert.ToInt32(reader["Age"]);
                    TargetList.Add(x);
                }
                Connection.Close();
                return TargetList;
            }
            catch (Exception ex)
            {
                Connection.Close();
                List<Coach> List = new List<Coach>();
                return List;
            }
        }

        public int Update(Coach _coach)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_EditCoach", Connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter IdParam = new SqlParameter("@Id", SqlDbType.Int);
                SqlParameter FirstNameParam = new SqlParameter("@FirstName", SqlDbType.NVarChar, 50);
                SqlParameter LastNameParam = new SqlParameter("@LastName", SqlDbType.NVarChar, 100);
                SqlParameter AgeParam = new SqlParameter("@Age", SqlDbType.Int);

                IdParam.Value = _coach.Id;
                FirstNameParam.Value = _coach.FirstName;
                LastNameParam.Value = _coach.LastName;
                AgeParam.Value = (_coach.Age ?? (object)DBNull.Value);

                cmd.Parameters.Add(IdParam);
                cmd.Parameters.Add(FirstNameParam);
                cmd.Parameters.Add(LastNameParam);
                cmd.Parameters.Add(AgeParam);

                Connection.Open();
                cmd.ExecuteNonQuery();
                Connection.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("siema");
                Connection.Close();
                return 1;
            }
            return 0;
        }
    }
}
