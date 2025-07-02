using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccessLayer
{
    public class clsTestAppointmentData
    {
        public static DataTable GetAllAppointmentsByTestTypeID(int TestTypeID, int PersonID)
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select T.TestAppointmentID,T.AppointmentDate,T.PaidFees, T.IsLocked, T.TestTypeID from TestAppointments T
	Inner Join LocalDrivingLicenseApplications L On T.LocalDrivingLicenseApplicationID = L.LocalDrivingLicenseApplicationID
	Inner Join Applications A On A.ApplicationID = L.LocalDrivingLicenseApplicationID 
	Inner Join People P On P.PersonID = A.ApplicantPersonID
	where T.TestTypeID = @TestTypeID and PersonID = @PersonID";



            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    dt.Load(reader);
                }

                reader.Close();


            }

            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dt;

        }

        public static bool GetTestAppointmentInfoByID(int TestAppointmentID,ref int TestTypeID,
        ref int LDLApplicationID,ref DateTime AppointmentDate,ref decimal PaidFees,ref int CreatedByUserID,
        ref bool IsLocked,
        ref int RetakeTestApplicationID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM TestAppointments WHERE TestAppointmentID = @TestAppointmentID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    TestTypeID = (int)reader["TestTypeID"];
                    LDLApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                    AppointmentDate = (DateTime)reader["AppointmentDate"];
                    PaidFees = (decimal)reader["PaidFees"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsLocked = (bool)reader["IsLocked"];

                    // Handle NULL value for RetakeTestApplicationID
                    if (reader["RetakeTestApplicationID"] != DBNull.Value)
                        RetakeTestApplicationID = (int)reader["RetakeTestApplicationID"];
                    else
                        RetakeTestApplicationID = -1; // Default value
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
                throw new Exception("An error occurred while retrieving Test Appointment info: " + ex.Message, ex);
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }



        public static int AddNewTestAppointment(int TestTypeID, int LDLApplicationID,
      DateTime AppointmentDate, decimal PaidFees, int CreatedByUserID, bool IsLocked)
        {
            int AppointmentID = -1;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
        INSERT INTO TestAppointments 
        (TestTypeID, LicenseClassID, LocalDrivingLicenseApplicationID, AppointmentDate, 
         PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID)
        VALUES 
        (@TestTypeID, @LicenseClassID, @LDLApplicationID, @AppointmentDate, 
         @PaidFees, @CreatedByUserID, @IsLocked, NULL);

        SELECT SCOPE_IDENTITY();";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                command.Parameters.AddWithValue("@LDLApplicationID", LDLApplicationID);
                command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
                command.Parameters.AddWithValue("@PaidFees", PaidFees);
                command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                command.Parameters.AddWithValue("@IsLocked", IsLocked);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    {
                        AppointmentID = insertedID;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to insert Test Appointment: " + ex.Message, ex);
                }
            }

            return AppointmentID;
        }


        public static bool UpdateTestAppointment(int TestAppointmentID, int TestTypeID, int LDLApplicationID,
            DateTime AppointmentDate, decimal PaidFees, int CreatedByUserID, bool IsLocked, int RetakeTestApplicationID)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"UPDATE TestAppointments
                  SET TestTypeID = @TestTypeID,
                      LicenseClassID = @LicenseClassID,
                      LocalDrivingLicenseApplicationID = @LDLApplicationID,
                      AppointmentDate = @AppointmentDate,
                      PaidFees = @PaidFees,
                      CreatedByUserID = @CreatedByUserID,
                      IsLocked = @IsLocked,
                      RetakeTestApplicationID = @RetakeTestApplicationID
                  WHERE TestAppointmentID = @TestAppointmentID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                command.Parameters.AddWithValue("@LDLApplicationID", LDLApplicationID);
                command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
                command.Parameters.AddWithValue("@PaidFees", PaidFees);
                command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                command.Parameters.AddWithValue("@IsLocked", IsLocked);
                command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID == -1 ? (object)DBNull.Value : RetakeTestApplicationID);
                command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to update Test Appointment: " + ex.Message, ex);
                }
            }

            return rowsAffected > 0;
        }

        public static bool DeleteTestAppointment(int TestAppointmentID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"DELETE FROM TestAppointments 
                     WHERE TestAppointmentID = @TestAppointmentID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }
    }
}
