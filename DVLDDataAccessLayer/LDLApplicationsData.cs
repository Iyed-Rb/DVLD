using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace DVLDDataAccessLayer
{
    public class clsLDLApplicationData
    {


        public static int GetPassedTestsCount(int LDLApplicationID)
        {
            int count = 0;
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
            SELECT COUNT(DISTINCT TestAppointments.TestAppointmentID)
            FROM TestAppointments 
            INNER JOIN Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
            WHERE TestAppointments.LocalDrivingLicenseApplicationID = @LDLApplicationID
              AND Tests.TestResult = 1";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@LDLApplicationID", LDLApplicationID);

                conn.Open();
                object result = cmd.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int res))
                    count = res;
            }
            return count;
        }



        public static DataTable GetAllLDLApplications()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT 
    LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID AS [L.D.LAppID], 
    LicenseClasses.ClassName AS [Driving Class], 
    People.NationalNo,
    (People.FirstName + ' ' + People.SecondName + ' ' + People.ThirdName + ' ' + People.LastName) AS [FULL Name], 
    Applications.ApplicationDate,
    
    (SELECT COUNT(DISTINCT TestAppointments.TestAppointmentID)
     FROM TestAppointments 
     INNER JOIN Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
     WHERE TestAppointments.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID  
       AND Tests.TestResult = 1) AS [Passed Tests], 

    CASE 
        WHEN Applications.ApplicationStatus = 1 THEN 'New'
        WHEN Applications.ApplicationStatus = 2 THEN 'Canceled' 
        WHEN Applications.ApplicationStatus = 3 THEN 'Completed' 
    END AS Status

FROM LocalDrivingLicenseApplications 
LEFT JOIN Applications 
    ON LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID 
LEFT JOIN LicenseClasses 
    ON LocalDrivingLicenseApplications.LicenseClassID = LicenseClasses.LicenseClassID   
LEFT JOIN People 
    ON Applications.ApplicantPersonID = People.PersonID 
LEFT JOIN TestAppointments 
    ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID 
LEFT JOIN Tests 
    ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID

GROUP BY 
    LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID,
    LicenseClasses.ClassName,
    People.NationalNo,
    People.FirstName,
    People.SecondName,
    People.ThirdName,
    People.LastName,
    Applications.ApplicationDate,
    Applications.ApplicationStatus";


            SqlCommand command = new SqlCommand(query, connection);

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

        public static bool GetLDLApplicationInfoByID(int LDLApplicationID, ref int ApplicationID,ref int LicenseClassID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM LocalDrivingLicenseApplications 
                     WHERE LocalDrivingLicenseApplicationID = @LDLApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LDLApplicationID", LDLApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    ApplicationID = (int)reader["ApplicationID"];
                    LicenseClassID = (int)reader["LicenseClassID"];
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
                // Optional: log the error
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static int AddNewLDLApplication(int ApplicationID, int LicenseClassID)
        {
            int LDLApplicationID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"
             INSERT INTO LocalDrivingLicenseApplications (ApplicationID, LicenseClassID)
             VALUES (@ApplicationID, @LicenseClassID);
             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    LDLApplicationID = insertedID;
                }
            }
            catch (Exception ex)
            {
                //throw new Exception("Failed to insert LDL application: " + ex.Message, ex);
            }
            finally
            {
                connection.Close();
            }

            return LDLApplicationID;
        }

        public static bool UpdateLDLApplication(int LDLApplicationID, int ApplicationID, int LicenseClassID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE LocalDrivingLicenseApplications
                 SET ApplicationID = @ApplicationID,
                     LicenseClassID = @LicenseClassID
                 WHERE LocalDrivingLicenseApplicationID = @LDLApplicationID";


            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@LDLApplicationID", LDLApplicationID);

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

        public static bool DeleteLDLApplication(int LDLApplicationID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"DELETE FROM LocalDrivingLicenseApplications 
                     WHERE LocalDrivingLicenseApplicationID = @LDLApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LDLApplicationID", LDLApplicationID);

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

        public static bool HasSameLDLApplication(string NationalNo, int LicenseClassID)
        {

            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select Found = 1  from LocalDrivingLicenseApplications inner join Applications
	        On LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID
	        Inner join People On People.PersonID = Applications.ApplicantPersonID
	        where People.NationalNo = @NationalNo and LicenseClassID = @LicenseClassID and
            (Applications.ApplicationStatus = 1 or Applications.ApplicationStatus = 3)";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;


        }
    }
}
