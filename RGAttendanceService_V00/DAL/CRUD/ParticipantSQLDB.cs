using RGAttendanceService_V00.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using RGAttendanceService_V00.DAL.CRUD;

namespace RGAttendanceService_V00.DAL.CRUD
{
    public class ParticipantSQLDB : IParticipant
    {
        private IConfiguration _configuration;
        private string ConnectionString;
        SqlConnection Connection = new SqlConnection();
        GroupSQLDB GroupDB;

        public ParticipantSQLDB(IConfiguration _configuration)
        {
            this._configuration = _configuration;
            ConnectionString = _configuration.GetConnectionString("RGAttendanceService");
            Connection.ConnectionString = ConnectionString;
            GroupDB = new GroupSQLDB(_configuration);
        }
        public int Add(Participant _participant)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_AddParticipant", Connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter IdParam = new SqlParameter("@Id", SqlDbType.Int);
                SqlParameter FirstNameParam = new SqlParameter("@FirstName", SqlDbType.NVarChar, 50);
                SqlParameter LastNameParam = new SqlParameter("@LastName", SqlDbType.NVarChar, 100);
                SqlParameter DateOfBirthParam = new SqlParameter("@DateOfBirth", SqlDbType.Date);
                SqlParameter GenderParam = new SqlParameter("@Gender", SqlDbType.NVarChar, 20);
                SqlParameter PhoneNumberParam = new SqlParameter("@PhoneNumber", SqlDbType.NVarChar, 15);
                SqlParameter AgeParam = new SqlParameter("@Age", SqlDbType.Int);
                SqlParameter CountryParam = new SqlParameter("@Country", SqlDbType.NVarChar, 100);
                SqlParameter AddressCityParam = new SqlParameter("@AddressCity", SqlDbType.NVarChar, 100);
                SqlParameter AddressStreetParam = new SqlParameter("@AddressStreet", SqlDbType.NVarChar, 100);
                SqlParameter AddressNumberParam = new SqlParameter("@AddressNumber", SqlDbType.NVarChar, 20);
                SqlParameter GroupIdParam = new SqlParameter("@GroupId", SqlDbType.Int);

                IdParam.Direction = ParameterDirection.Output;

                FirstNameParam.Value = _participant.FirstName;
                LastNameParam.Value = _participant.LastName;
                GenderParam.Value = _participant.Gender;

                DateOfBirthParam.Value = (_participant.DateOfBirth ?? (object)DBNull.Value);
                PhoneNumberParam.Value = (_participant.PhoneNumber ?? (object)DBNull.Value);
                AgeParam.Value = (_participant.Age ?? (object)DBNull.Value);
                CountryParam.Value = (_participant.Country ?? (object)DBNull.Value);
                AddressCityParam.Value = (_participant.AddressCity ?? (object)DBNull.Value);
                AddressStreetParam.Value = (_participant.AddressStreet ?? (object)DBNull.Value);
                AddressNumberParam.Value = (_participant.AddressNumber ?? (object)DBNull.Value);
                //GroupIdParam.Value = (_participant.GroupId ?? (object)DBNull.Value);

                cmd.Parameters.Add(IdParam);
                cmd.Parameters.Add(FirstNameParam);
                cmd.Parameters.Add(LastNameParam);
                cmd.Parameters.Add(GenderParam);
                cmd.Parameters.Add(DateOfBirthParam);
                cmd.Parameters.Add(PhoneNumberParam);
                cmd.Parameters.Add(AgeParam);
                cmd.Parameters.Add(CountryParam);
                cmd.Parameters.Add(AddressCityParam);
                cmd.Parameters.Add(AddressStreetParam);
                cmd.Parameters.Add(AddressNumberParam);
                cmd.Parameters.Add(GroupIdParam);

                Connection.Open();
                cmd.ExecuteNonQuery();
                Connection.Close();
            }
            catch (Exception ex)
            {
                //future error log listing
                System.Diagnostics.Debug.WriteLine(ex.Message);
                Connection.Close();
                return 1;
            }
            return 0;
        }

        public int Delete(int _id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_DeleteParticipant", Connection);
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
                //future error log
                return 1;
            }
            return 0;
        }

        public Participant Get(int _id)
        {
            try
            {
                Participant TargetParticipant = new Participant();
                SqlCommand cmd = new SqlCommand("sp_GetParticipant", Connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter IdParam = new SqlParameter("@Id", SqlDbType.Int);
                IdParam.Value = _id;
                cmd.Parameters.Add(IdParam);

                Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TargetParticipant.Id = Convert.ToInt32(reader["Id"]);
                    TargetParticipant.FirstName = Convert.ToString(reader["FirstName"]);
                    TargetParticipant.LastName = Convert.ToString(reader["LastName"]);
                    TargetParticipant.DateOfBirth = reader["DateOfBirth"] == DBNull.Value ? null : Convert.ToDateTime(reader["DateOfBirth"]);
                    TargetParticipant.Gender = Convert.ToString(reader["Gender"]);
                    TargetParticipant.PhoneNumber = reader["PhoneNumber"] == DBNull.Value ? null : Convert.ToString(reader["PhoneNumber"]);
                    TargetParticipant.Age = reader["Age"] == DBNull.Value ? null : Convert.ToInt32(reader["Age"]);
                    TargetParticipant.Country = reader["Country"] == DBNull.Value ? null : Convert.ToString(reader["Country"]);
                    TargetParticipant.AddressCity = reader["AddressCity"] == DBNull.Value ? null : Convert.ToString(reader["AddressCity"]);
                    TargetParticipant.AddressStreet = reader["AddressStreet"] == DBNull.Value ? null : Convert.ToString(reader["AddressStreet"]);
                    TargetParticipant.AddressNumber = reader["AddressNumber"] == DBNull.Value ? null : Convert.ToString(reader["AddressNumber"]);
                    //TargetParticipant.GroupId = reader["GroupId"] == DBNull.Value ? null : Convert.ToInt32(reader["GroupId"]);
                    //TargetParticipant.Group = reader["GroupId"] == DBNull.Value ? null : GroupDB.Get(Convert.ToInt32(reader["GroupId"]));
                }
                Connection.Close();
                return TargetParticipant;
            }
            catch (Exception ex)
            {
                //future error log
                return null;
            }
        }

        public List<Participant> GetList()
        {
            try
            {
                List<Participant> TargetList = new List<Participant>();
                SqlCommand cmd = new SqlCommand("sp_GetParticipantList", Connection);
                cmd.CommandType = CommandType.StoredProcedure;

                Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Participant x = new Participant
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        FirstName = Convert.ToString(reader["FirstName"]),
                        LastName = Convert.ToString(reader["LastName"]),
                        DateOfBirth = reader["DateOfBirth"] == DBNull.Value ? null : Convert.ToDateTime(reader["DateOfBirth"]),
                        Gender = Convert.ToString(reader["Gender"]),
                        PhoneNumber = reader["PhoneNumber"] == DBNull.Value ? null : Convert.ToString(reader["PhoneNumber"]),
                        Age = reader["Age"] == DBNull.Value ? null : Convert.ToInt32(reader["Age"]),
                        Country = reader["Country"] == DBNull.Value ? null : Convert.ToString(reader["Country"]),
                        AddressCity = reader["AddressCity"] == DBNull.Value ? null : Convert.ToString(reader["AddressCity"]),
                        AddressStreet = reader["AddressStreet"] == DBNull.Value ? null : Convert.ToString(reader["AddressStreet"]),
                        AddressNumber = reader["AddressNumber"] == DBNull.Value ? null : Convert.ToString(reader["AddressNumber"]),
                        //GroupId = reader["GroupId"] == DBNull.Value ? null : Convert.ToInt32(reader["GroupId"]),
                        //Group = reader["GroupId"] == DBNull.Value ? null : GroupDB.Get(Convert.ToInt32(reader["GroupId"])),
                    };
                    TargetList.Add(x);
                }
                Connection.Close();
                return TargetList;
            }
            catch (Exception ex)
            {
                //future error log
                System.Diagnostics.Debug.WriteLine(ex.Message);
                List<Participant> x = new List<Participant>();
                return x;
            }
        }

        public int Update(Participant _participant)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_EditParticipant", Connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter IdParam = new SqlParameter("@Id", SqlDbType.Int);
                SqlParameter FirstNameParam = new SqlParameter("@FirstName", SqlDbType.NVarChar, 50);
                SqlParameter LastNameParam = new SqlParameter("@LastName", SqlDbType.NVarChar, 100);
                SqlParameter DateOfBirthParam = new SqlParameter("@DateOfBirth", SqlDbType.Date);
                SqlParameter GenderParam = new SqlParameter("@Gender", SqlDbType.NVarChar, 20);
                SqlParameter PhoneNumberParam = new SqlParameter("@PhoneNumber", SqlDbType.NVarChar, 15);
                SqlParameter AgeParam = new SqlParameter("@Age", SqlDbType.Int);
                SqlParameter CountryParam = new SqlParameter("@Country", SqlDbType.NVarChar, 100);
                SqlParameter AddressCityParam = new SqlParameter("@AddressCity", SqlDbType.NVarChar, 100);
                SqlParameter AddressStreetParam = new SqlParameter("@AddressStreet", SqlDbType.NVarChar, 100);
                SqlParameter AddressNumberParam = new SqlParameter("@AddressNumber", SqlDbType.NVarChar, 20);
                SqlParameter GroupIdParam = new SqlParameter("@GroupId", SqlDbType.Int);

                IdParam.Value = _participant.Id;
                FirstNameParam.Value = _participant.FirstName;
                LastNameParam.Value = _participant.LastName;
                GenderParam.Value = _participant.Gender;

                DateOfBirthParam.Value = (_participant.DateOfBirth ?? (object)DBNull.Value);
                PhoneNumberParam.Value = (_participant.PhoneNumber ?? (object)DBNull.Value);
                AgeParam.Value = (_participant.Age ?? (object)DBNull.Value);
                CountryParam.Value = (_participant.Country ?? (object)DBNull.Value);
                AddressCityParam.Value = (_participant.AddressCity ?? (object)DBNull.Value);
                AddressStreetParam.Value = (_participant.AddressStreet ?? (object)DBNull.Value);
                AddressNumberParam.Value = (_participant.AddressNumber ?? (object)DBNull.Value);
                //GroupIdParam.Value = (_participant.GroupId ?? (object)DBNull.Value);

                cmd.Parameters.Add(IdParam);
                cmd.Parameters.Add(FirstNameParam);
                cmd.Parameters.Add(LastNameParam);
                cmd.Parameters.Add(GenderParam);
                cmd.Parameters.Add(DateOfBirthParam);
                cmd.Parameters.Add(PhoneNumberParam);
                cmd.Parameters.Add(AgeParam);
                cmd.Parameters.Add(CountryParam);
                cmd.Parameters.Add(AddressCityParam);
                cmd.Parameters.Add(AddressStreetParam);
                cmd.Parameters.Add(AddressNumberParam);
                cmd.Parameters.Add(GroupIdParam);

                Connection.Open();
                cmd.ExecuteNonQuery();
                Connection.Close();
            }
            catch (Exception ex)
            {
                //future error log
                return 1;
            }
            return 0;
        }

        public List<Participant> GetListByGroupId(int _id)
        {
            try
            {
                List<Participant> List = new List<Participant>();
                SqlCommand cmd = new SqlCommand("sp_GetParticipantsByGroupId", Connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter IdParam = new SqlParameter("@GroupId", SqlDbType.Int);
                IdParam.Value = _id;
                cmd.Parameters.Add(IdParam);

                Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    Participant x = new Participant
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        FirstName = Convert.ToString(reader["FirstName"]),
                        LastName = Convert.ToString(reader["LastName"]),
                        DateOfBirth = reader["DateOfBirth"] == DBNull.Value ? null : Convert.ToDateTime(reader["DateOfBirth"]),
                        Gender = Convert.ToString(reader["Gender"]),
                        PhoneNumber = reader["PhoneNumber"] == DBNull.Value ? null : Convert.ToString(reader["PhoneNumber"]),
                        Age = reader["Age"] == DBNull.Value ? null : Convert.ToInt32(reader["Age"]),
                        Country = reader["Country"] == DBNull.Value ? null : Convert.ToString(reader["Country"]),
                        AddressCity = reader["AddressCity"] == DBNull.Value ? null : Convert.ToString(reader["AddressCity"]),
                        AddressStreet = reader["AddressStreet"] == DBNull.Value ? null : Convert.ToString(reader["AddressStreet"]),
                        AddressNumber = reader["AddressNumber"] == DBNull.Value ? null : Convert.ToString(reader["AddressNumber"]),
                        //GroupId = reader["GroupId"] == DBNull.Value ? null : Convert.ToInt32(reader["GroupId"]),
                        //Group = reader["GroupId"] == DBNull.Value ? null : GroupDB.Get(Convert.ToInt32(reader["GroupId"])),
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

    }
}





