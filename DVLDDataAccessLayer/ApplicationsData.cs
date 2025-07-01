using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom;
using static System.Net.Mime.MediaTypeNames;

namespace DVLDDataAccessLayer
{
    public class clsApplicationsData
    {

        //public static DataTable GetAllApplications()
        //{

        //    DataTable dt = new DataTable();
        //    SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

        //    string query = @"SELECT L.LocalDrivingLicenseApplicationID as LDLAppID, LC.ClassName as [Driving Class], 
        //  P.NationalNo as [National No] ,
        //  P.FirstName + ' ' + P.SecondName + ' ' + P.ThirdName + ' ' + P.LastName AS [Full Name],
        //  A1.ApplicationDate as [Application Date],
     
        //  ( SELECT COUNT(*) FROM TestAppointments T2
        //    WHERE T2.LocalDrivingLicenseApplicationID = L.LocalDrivingLicenseApplicationID
        //    AND T2.RetakeTestApplicationID IS NOT NULL
        //  ) AS [Passed Tests],
        //  CASE A1.ApplicationStatus
        //    WHEN 1 THEN 'New'
        //    WHEN 2 THEN 'Cancelled'
        //    WHEN 3 THEN 'Completed'
        //    ELSE 'Unknown'
        //  END AS Status

        //  FROM LocalDrivingLicenseApplications L

        //  INNER JOIN LicenseClasses LC 
        //    ON L.LicenseClassID = LC.LicenseClassID

        //  INNER JOIN Applications A1 
        //    ON L.ApplicationID = A1.ApplicationID

        //  INNER JOIN People P 
        //    ON A1.ApplicantPersonID = P.PersonID;";

        //    SqlCommand command = new SqlCommand(query, connection);

        //    try
        //    {
        //        connection.Open();

        //        SqlDataReader reader = command.ExecuteReader();

        //        if (reader.HasRows)

        //        {
        //            dt.Load(reader);
        //        }

        //        reader.Close();


        //    }

        //    catch (Exception ex)
        //    {
        //        // Console.WriteLine("Error: " + ex.Message);
        //    }
        //    finally
        //    {
        //        connection.Close();
        //    }

        //    return dt;

        //}


        public static bool GetApplicationInfoByID(int ApplicationID, ref int ApplicantPersonID, ref DateTime ApplicationDate,ref int ApplicationTypeID,
          ref int ApplicationStatusID, ref DateTime LastStatusDate, ref decimal PaidFees, ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM Applications WHERE ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    ApplicantPersonID = (int)reader["ApplicantPersonID"];
                    ApplicationDate = (DateTime)reader["ApplicationDate"];
                    ApplicationTypeID = (int)reader["ApplicationTypeID"];
                    ApplicationStatusID = Convert.ToInt32(reader["ApplicationStatus"]);
                    LastStatusDate = (DateTime)reader["LastStatusDate"];
                    PaidFees = (decimal)reader["PaidFees"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
                throw new Exception("An error occurred: " + ex.Message, ex);

            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static int AddNewApplication(int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID,
          int ApplicationStatus, DateTime LastStatusDate,  decimal PaidFees, int CreatedByUserID)
        {
            int ApplicationID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"
        INSERT INTO Applications 
        (ApplicantPersonID, ApplicationDate, ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID)
        VALUES 
        (@ApplicantPersonID, @ApplicationDate, @ApplicationTypeID, @ApplicationStatus, @LastStatusDate, @PaidFees, @CreatedByUserID);

        SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees",PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID",CreatedByUserID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    ApplicationID = insertedID;
                }
            }
            catch (Exception ex)
            {
                // Optional: throw or log for debugging
                throw new Exception("Failed to insert new application. " + ex.Message, ex);
            }
            finally
            {
                connection.Close();
            }

            return ApplicationID;
        }

        public static bool UpdateApplication(int ApplicationID, int ApplicantPersonID,
         int ApplicationTypeID, int ApplicationStatus, DateTime LastStatusDate, decimal PaidFees, int CreatedByUserID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE Applications  
                     SET ApplicantPersonID = @ApplicantPersonID,
                         ApplicationTypeID = @ApplicationTypeID,
                         ApplicationStatus = @ApplicationStatus,
                         LastStatusDate = GETDATE(),
                         PaidFees = @PaidFees,
                         CreatedByUserID = @CreatedByUserID
                     WHERE ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

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

        public static bool DeleteApplication(int ApplicationID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"DELETE FROM Applications 
                     WHERE ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Optional: log or throw
                // throw new Exception("Delete failed: " + ex.Message, ex);
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static int GetExistingApplicationID(int ApplicantPersonID, int LicenseClassID)
        {
            int existingApplicationID = -1;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
            SELECT TOP 1 Applications.ApplicationID 
            FROM Applications 
            INNER JOIN LocalDrivingLicenseApplications L ON Applications.ApplicationID = L.ApplicationID
            INNER JOIN LicenseClasses ON LicenseClasses.LicenseClassID = L.LicenseClassID
            WHERE Applications.ApplicationTypeID = 1
              AND ApplicantPersonID = @ApplicantPersonID
              AND LicenseClasses.LicenseClassID = @LicenseClassID
              AND (Applications.ApplicationStatus = 1 OR Applications.ApplicationStatus = 3)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
                command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar(); // Only gets the first column of the first row

                    if (result != null && int.TryParse(result.ToString(), out int appID))
                    {
                        existingApplicationID = appID;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error fetching existing ApplicationID: " + ex.Message, ex);
                }
            }

            return existingApplicationID;
        }

        public static bool CancelApplication(int ApplicationID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "UPDATE Applications SET ApplicationStatus = 2, LastStatusDate = GETDATE() WHERE ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
 
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

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



