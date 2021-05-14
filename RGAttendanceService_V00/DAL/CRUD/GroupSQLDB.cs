using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using RGAttendanceService_V00.Models;

namespace RGAttendanceService_V00.DAL.CRUD
{
    public class GroupSQLDB : IGroup
    {
        private IConfiguration _configuration;
        private string ConnectionString;
        SqlConnection Connection = new SqlConnection();

        public GroupSQLDB(IConfiguration _configuration)
        {
            this._configuration = _configuration;
            ConnectionString = _configuration.GetConnectionString("RGAttendanceService");
            Connection.ConnectionString = ConnectionString;
        }

        public int Add(Group _group)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_AddGroup", Connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter IdParam = new SqlParameter("@Id", SqlDbType.Int);
                SqlParameter NameParam = new SqlParameter("@Name", SqlDbType.NVarChar, 200);
                SqlParameter TypeParam = new SqlParameter("@Type", SqlDbType.NVarChar, 30);
                SqlParameter StreetParam = new SqlParameter("@Street", SqlDbType.NVarChar, 100);
                SqlParameter CityParam = new SqlParameter("@City", SqlDbType.NVarChar, 100);
                SqlParameter NumberParam = new SqlParameter("@Number", SqlDbType.NVarChar, 10);
                SqlParameter MainCoachParam = new SqlParameter("@MainCoach", SqlDbType.Int);

                IdParam.Direction = ParameterDirection.Output;
                NameParam.Value = _group.Name;

                TypeParam.Value = (_group.type ?? (object)DBNull.Value);
                StreetParam.Value = (_group.Street ?? (object)DBNull.Value);
                CityParam.Value = (_group.City ?? (object)DBNull.Value);
                NumberParam.Value = (_group.Number ?? (object)DBNull.Value);
                MainCoachParam.Value = (_group.CoachId ?? (object)DBNull.Value);

                cmd.Parameters.Add(IdParam);
                cmd.Parameters.Add(NameParam);
                cmd.Parameters.Add(TypeParam);
                cmd.Parameters.Add(StreetParam);
                cmd.Parameters.Add(CityParam);
                cmd.Parameters.Add(NumberParam);
                cmd.Parameters.Add(MainCoachParam);

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
                SqlCommand cmd = new SqlCommand("sp_DeleteGroup", Connection);
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
                Connection.Close();
                return 1;
            }
            return 0;
        }

        public Group Get(int _id)
        {
            try
            {
                Group TargetGroup = new Group();

                SqlCommand cmd = new SqlCommand("sp_GetGroup", Connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter IdParam = new SqlParameter("@Id", SqlDbType.Int);
                IdParam.Value = _id;
                cmd.Parameters.Add(IdParam);

                Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TargetGroup.Id = Convert.ToInt32(reader["Id"]);
                    TargetGroup.Name = Convert.ToString(reader["Name"]);
                    TargetGroup.type = reader["Type"] == DBNull.Value ? null : Convert.ToString(reader["Type"]);
                    TargetGroup.Street = reader["Street"] == DBNull.Value ? null : Convert.ToString(reader["Street"]);
                    TargetGroup.City = reader["City"] == DBNull.Value ? null : Convert.ToString(reader["City"]);
                    TargetGroup.Number = reader["Number"] == DBNull.Value ? null : Convert.ToString(reader["Number"]);
                    TargetGroup.CoachId = reader["MainCoach"] == DBNull.Value ? null : Convert.ToInt32(reader["MainCoach"]);
                }
                Connection.Close();
                return TargetGroup;
            }
            catch (Exception ex)
            {
                Connection.Close();
                return null;
            }
        }

        public List<Group> GetList()
        {
            try
            {
                List<Group> TargetList = new List<Group>();
                SqlCommand cmd = new SqlCommand("sp_GetGroupList", Connection);
                cmd.CommandType = CommandType.StoredProcedure;

                Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Group x = new Group
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = Convert.ToString(reader["Name"]),
                        type = reader["Type"] == DBNull.Value ? null : Convert.ToString(reader["Type"]),
                        Street = reader["Street"] == DBNull.Value ? null : Convert.ToString(reader["Street"]),
                        City = reader["City"] == DBNull.Value ? null : Convert.ToString(reader["City"]),
                        Number = reader["Number"] == DBNull.Value ? null : Convert.ToString(reader["Number"]),
                        CoachId = reader["MainCoach"] == DBNull.Value ? null : Convert.ToInt32(reader["MainCoach"])
                    };
                    TargetList.Add(x);
                }
                Connection.Close();
                return TargetList;
            }
            catch (Exception ex)
            {
                Connection.Close();
                List<Group> x = new List<Group>();
                return x;
            }
        }

        public int Update(Group _group)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_EditGroup", Connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter IdParam = new SqlParameter("@Id", SqlDbType.Int);
                SqlParameter NameParam = new SqlParameter("@Name", SqlDbType.NVarChar, 200);
                SqlParameter TypeParam = new SqlParameter("@Type", SqlDbType.NVarChar, 30);
                SqlParameter StreetParam = new SqlParameter("@Street", SqlDbType.NVarChar, 100);
                SqlParameter CityParam = new SqlParameter("@City", SqlDbType.NVarChar, 100);
                SqlParameter NumberParam = new SqlParameter("@Number", SqlDbType.NVarChar, 10);
                SqlParameter MainCoachParam = new SqlParameter("@MainCoach", SqlDbType.Int);

                IdParam.Value = _group.Id;
                NameParam.Value = _group.Name;

                TypeParam.Value = (_group.type ?? (object)DBNull.Value);
                StreetParam.Value = (_group.Street ?? (object)DBNull.Value);
                CityParam.Value = (_group.City ?? (object)DBNull.Value);
                NumberParam.Value = (_group.Number ?? (object)DBNull.Value);
                MainCoachParam.Value = (_group.CoachId ?? (object)DBNull.Value);

                cmd.Parameters.Add(IdParam);
                cmd.Parameters.Add(NameParam);
                cmd.Parameters.Add(TypeParam);
                cmd.Parameters.Add(StreetParam);
                cmd.Parameters.Add(CityParam);
                cmd.Parameters.Add(NumberParam);
                cmd.Parameters.Add(MainCoachParam);

                Connection.Open();
                cmd.ExecuteNonQuery();
                Connection.Close();
            }
            catch (Exception ex)
            {
                Connection.Close();
                return 1;
            }
            return 0;
        }
    }
}
