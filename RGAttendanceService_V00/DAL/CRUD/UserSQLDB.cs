using RGAttendanceService_V00.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using RGAttendanceService_V00.DAL.CRUD;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using RGAttendanceService_V00.DAL.Interfaces;

namespace RGAttendanceService_V00.DAL.CRUD
{
    public class UserSQLDB : IUser
    {
        private IConfiguration _configuration;
        string ConnectionString;
        SqlConnection Connection = new SqlConnection();

        public UserSQLDB(IConfiguration _configuration)
        {
            this._configuration = _configuration;
            ConnectionString = _configuration.GetConnectionString("RGAttendanceServiceEntity");
            Connection.ConnectionString = ConnectionString;
        }
        private string HashPassword(UserModel user)
        {
            PasswordHasher<string> PasswordHasher = new PasswordHasher<string>();
            user.Password = PasswordHasher.HashPassword(user.UserName, user.Password);
            return user.Password;
        }

        public int Add(UserModel _siteuser)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_AddRGUser", Connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter IdParam = new SqlParameter("@Id", SqlDbType.Int);
                SqlParameter UserNameParam = new SqlParameter("@UserName", SqlDbType.NVarChar, 150);
                SqlParameter PasswordParam = new SqlParameter("@Password", SqlDbType.NVarChar, 300);
                SqlParameter EmailParam = new SqlParameter("@Email", SqlDbType.NVarChar, 200);
                SqlParameter CoachIdParam = new SqlParameter("@CoachId", SqlDbType.Int);

                IdParam.Direction = ParameterDirection.Output;
                UserNameParam.Value = _siteuser.UserName;
                PasswordParam.Value = HashPassword(_siteuser);
                EmailParam.Value = _siteuser.Email;
                CoachIdParam.Value = (_siteuser.CoachId ?? (object)DBNull.Value);

                cmd.Parameters.Add(IdParam);
                cmd.Parameters.Add(UserNameParam);
                cmd.Parameters.Add(PasswordParam);
                cmd.Parameters.Add(EmailParam);
                cmd.Parameters.Add(CoachIdParam);

                Connection.Open();
                cmd.ExecuteNonQuery();
                Connection.Close();
                return 0;
            }
            catch (Exception ex)
            {
                Connection.Close();
                return 1;
            }


        }

        public int Delete(int _id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_DeleteRGUser", Connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter IdParam = new SqlParameter("@Id", SqlDbType.Int);
                IdParam.Value = _id;
                cmd.Parameters.Add(IdParam);

                Connection.Open();
                cmd.ExecuteNonQuery();
                Connection.Close();
                return 0;
            }
            catch (Exception ex)
            {
                Connection.Close();
                return 1;
            }
        }

        public UserModel Get(int _id)
        {
            try
            {
                UserModel TargetUser = new UserModel();
                SqlCommand cmd = new SqlCommand("sp_GetRGUser", Connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter _IdParam = new SqlParameter("@Id", SqlDbType.Int);
                _IdParam.Value = _id;
                cmd.Parameters.Add(_IdParam);

                Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TargetUser.Id = Convert.ToInt32(reader["Id"]);
                    TargetUser.UserName = Convert.ToString(reader["UserName"]);
                    TargetUser.Password = Convert.ToString(reader["Password"]);
                    TargetUser.Email = Convert.ToString(reader["Email"]);
                    TargetUser.CoachId = reader["CoachId"] == DBNull.Value ? null : Convert.ToInt32(reader["CoachId"]);
                }
                Connection.Close();
                return TargetUser;
            }
            catch (Exception ex)
            {
                Connection.Close();
                return null;
            }
        }

        public List<UserModel> List()
        {
            try
            {
                List<UserModel> UserList = new List<UserModel>();
                SqlCommand cmd = new SqlCommand("sp_GetRGUserList", Connection);
                cmd.CommandType = CommandType.StoredProcedure;
                Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    UserModel NewRecord = new UserModel
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        UserName = Convert.ToString(reader["UserName"]),
                        Password = Convert.ToString(reader["Password"]),
                        Email = Convert.ToString(reader["Email"]),
                        CoachId = reader["CoachId"] == DBNull.Value ? null : Convert.ToInt32(reader["CoachId"])
                    };
                    UserList.Add(NewRecord);
                }
                Connection.Close();


                return UserList;
            }
            catch (Exception ex)
            {
                Connection.Close();
                return null;
            }
        }

        public int Update(UserModel _siteuser)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_EditRGUser", Connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter _IdParam = new SqlParameter("@Id", SqlDbType.Int);
                _IdParam.Value = _siteuser.Id;
                cmd.Parameters.Add(_IdParam);

                SqlParameter _UserNameParam = new SqlParameter("@UserName", SqlDbType.NVarChar, 100);
                _UserNameParam.Value = _siteuser.UserName;
                cmd.Parameters.Add(_UserNameParam);

                SqlParameter _PasswordParam = new SqlParameter("@Password", SqlDbType.NVarChar, 260);
                _PasswordParam.Value = HashPassword(_siteuser);
                cmd.Parameters.Add(_PasswordParam);

                SqlParameter CoachIdParam = new SqlParameter("@CoachId", SqlDbType.Int);
                CoachIdParam.Value = (_siteuser.CoachId ?? (object)DBNull.Value);
                cmd.Parameters.Add(CoachIdParam);

                Connection.Open();
                cmd.ExecuteNonQuery();
                Connection.Close();

                return 0;
            }
            catch (Exception ex)
            {
                Connection.Close(); 
                return 1;
            }
        }
    }
}
