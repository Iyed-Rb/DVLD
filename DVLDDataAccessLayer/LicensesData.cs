using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLDDataAccessLayer
{
    public class clsLicensesData
    {

        public static DataTable GetAllInternationalLicencesByPersonID(int PersonID)
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @" select I.InternationalLicenseID as [Int.License ID], I.ApplicationID as [Application ID], I.IssuedUsingLocalLicenseID as [L.License ID],
I.IssueDate as [Issue Date], I.ExpirationDate as [Expiration Date], I.IsActive as [Is Active]
  from InternationalLicenses I inner Join Applications On I.ApplicationID = Applications.ApplicationID
  where Applications.ApplicantPersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    dt.Load(reader);
                    Debug.WriteLine("✅ Rows returned for PersonID = " + PersonID);
                    Debug.WriteLine("Total rows loaded: " + dt.Rows.Count);

                }
                else
                {


                    Debug.WriteLine("❌ No rows returned for PersonID = " + PersonID);

                }

                reader.Close();


            }

            catch (Exception ex)
            {
                throw new Exception("Query failed: " + ex.Message, ex);
            }
            finally
            {
                connection.Close();
            }

            return dt;

        }


        public static DataTable GetAllLocalLicencesByPersonID(int PersonID)
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @" select Licenses.LicenseID as [Lic.ID], Licenses.ApplicationID as [App.ID], LicenseClasses.ClassName as [Class Name],
 Licenses.IssueDate as [Issue Date],Licenses.ExpirationDate as [Expiration Date],
 Licenses.IsActive as [Is Active] from Licenses inner Join LicenseClasses
 ON Licenses.LicenseClassID = LicenseClasses.LicenseClassID
 Inner Join Applications On Applications.ApplicationID = Licenses.ApplicationID
 Inner Join LocalDrivingLicenseApplications L On L.ApplicationID = Applications.ApplicationID
 where Applications.ApplicantPersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);
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

        public static bool GetLicenseInfoByID(int LicenseID,
        ref int ApplicationID, ref int DriverID, ref int LicenseClassID,
        ref DateTime IssueDate, ref DateTime ExpirationDate,
        ref string Notes, ref decimal PaidFees, ref bool IsActive,
        ref int IssueReason, ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"SELECT * FROM Licenses WHERE LicenseID = @LicenseID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    LicenseClassID = (int)reader["LicenseClassID"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    Notes = reader["Notes"].ToString();
                    PaidFees = (decimal)reader["PaidFees"];
                    IsActive = (bool)reader["IsActive"];
                    IssueReason = (int)reader["IssueReason"];
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

        public static bool GetLicenseInfoByApplicationID(ref int LicenseID,
        int ApplicationID, ref int DriverID, ref int LicenseClassID,
       ref DateTime IssueDate, ref DateTime ExpirationDate,
       ref string Notes, ref decimal PaidFees, ref bool IsActive,
       ref int IssueReason, ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"SELECT * FROM Licenses
            INNER JOIN Applications ON Licenses.ApplicationID = Applications.ApplicationID
            INNER JOIN LocalDrivingLicenseApplications L ON Applications.ApplicationID = L.ApplicationID
            WHERE Licenses.ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    LicenseID = (int)reader["LicenseID"];
                    DriverID = (int)reader["DriverID"];
                    LicenseClassID = (int)reader["LicenseClassID"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    Notes = reader["Notes"].ToString();
                    PaidFees = (decimal)reader["PaidFees"];
                    IsActive = (bool)reader["IsActive"];
                    IssueReason = Convert.ToInt32(reader["IssueReason"]);
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

        public static int AddNewLicense(int ApplicationID, int DriverID, int LicenseClassID,
            DateTime IssueDate, DateTime ExpirationDate, string Notes,
            decimal PaidFees, bool IsActive, int IssueReason, int CreatedByUserID)
        {
            int LicenseID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Licenses 
        (ApplicationID, DriverID, LicenseClassID, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUserID)
        VALUES 
        (@ApplicationID, @DriverID, @LicenseClassID, @IssueDate, @ExpirationDate, @Notes, @PaidFees, @IsActive, @IssueReason,
        @CreatedByUserID);
        SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            //command.Parameters.AddWithValue("@Notes", Notes);
            command.Parameters.AddWithValue("@Notes", string.IsNullOrWhiteSpace(Notes) ? DBNull.Value : (object)Notes);

            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@IssueReason", IssueReason);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    LicenseID = insertedID;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to insert new license. " + ex.Message, ex);
            }
            finally
            {
                connection.Close();
            }

            return LicenseID;
        }

        public static bool UpdateLicense(int LicenseID, int ApplicationID, int DriverID, int LicenseClassID,
            DateTime IssueDate, DateTime ExpirationDate, string Notes,
            decimal PaidFees, bool IsActive, int IssueReason, int CreatedByUserID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE Licenses SET ApplicationID = @ApplicationID,DriverID = @DriverID,
            LicenseClassID = @LicenseClassID,
            IssueDate = @IssueDate,
            ExpirationDate = @ExpirationDate,
            Notes = @Notes,
            PaidFees = @PaidFees,
            IsActive = @IsActive,
            IssueReason = @IssueReason,
            CreatedByUserID = @CreatedByUserID
            WHERE LicenseID = @LicenseID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@Notes", Notes);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@IssueReason", IssueReason);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update license. " + ex.Message, ex);
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }








    }
}
