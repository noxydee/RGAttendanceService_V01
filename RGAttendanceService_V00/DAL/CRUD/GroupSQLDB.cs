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
                IdParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(IdParam);

                SqlParameter NameParam = new SqlParameter("@Name", SqlDbType.NVarChar, 200);
                NameParam.Value = _group.Name;
                cmd.Parameters.Add(NameParam);

                SqlParameter TypeParam = new SqlParameter("@Type", SqlDbType.NVarChar, 30);
                TypeParam.Value = _group.type;
                cmd.Parameters.Add(TypeParam);

                SqlParameter StreetParam = new SqlParameter("@Street", SqlDbType.NVarChar, 100);
                StreetParam.Value = _group.Street;
                cmd.Parameters.Add(StreetParam);

                SqlParameter CityParam = new SqlParameter("@City", SqlDbType.NVarChar, 100);
                CityParam.Value = _group.City;
                cmd.Parameters.Add(CityParam);

                SqlParameter NumberParam = new SqlParameter("@Number", SqlDbType.NVarChar, 10);
                NumberParam.Value = _group.Number;
                cmd.Parameters.Add(NumberParam);

                Connection.Open();
                cmd.ExecuteNonQuery();
                Connection.Close();
            }
            catch (Exception ex)
            {
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
                    TargetGroup.type = Convert.ToString(reader["Type"]);
                    TargetGroup.Street = Convert.ToString(reader["Street"]);
                    TargetGroup.City = Convert.ToString(reader["City"]);
                    TargetGroup.Number = Convert.ToString(reader["Number"]);
                }
                Connection.Close();
                return TargetGroup;
            }
            catch (Exception ex)
            {

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
                        type = Convert.ToString(reader["Type"]),
                        Street = Convert.ToString(reader["Street"]),
                        City = Convert.ToString(reader["City"]),
                        Number = Convert.ToString(reader["Number"])
                    };
                    TargetList.Add(x);
                }
                Connection.Close();
                return TargetList;
            }
            catch (Exception ex)
            {
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
                IdParam.Value = _group.Id;
                cmd.Parameters.Add(IdParam);

                SqlParameter NameParam = new SqlParameter("@Name", SqlDbType.NVarChar, 200);
                NameParam.Value = _group.Name;
                cmd.Parameters.Add(NameParam);

                SqlParameter TypeParam = new SqlParameter("@Type", SqlDbType.NVarChar, 30);
                TypeParam.Value = _group.type;
                cmd.Parameters.Add(TypeParam);

                SqlParameter StreetParam = new SqlParameter("@Street", SqlDbType.NVarChar, 100);
                StreetParam.Value = _group.Street;
                cmd.Parameters.Add(StreetParam);

                SqlParameter CityParam = new SqlParameter("@City", SqlDbType.NVarChar, 100);
                CityParam.Value = _group.City;
                cmd.Parameters.Add(CityParam);

                SqlParameter NumberParam = new SqlParameter("@Number", SqlDbType.NVarChar, 10);
                NumberParam.Value = _group.Number;
                cmd.Parameters.Add(NumberParam);

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
