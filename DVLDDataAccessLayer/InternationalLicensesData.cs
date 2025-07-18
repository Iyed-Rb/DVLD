﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccessLayer
{
    public class clsInternationalLicenseData
    {

        public static DataTable GetAllInternationalLicences()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @" select I.InternationalLicenseID as [Int.License ID], I.ApplicationID as [Application ID],
            DriverID as [Driver ID], I.IssuedUsingLocalLicenseID as [L.License ID],
I.IssueDate as [Issue Date], I.ExpirationDate as [Expiration Date], I.IsActive as [Is Active]  from InternationalLicenses I";  

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
                throw new Exception("Query failed: " + ex.Message, ex);
            }
            finally
            {
                connection.Close();
            }

            return dt;

        }

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

        public static int CheckIfHadBeforeInternationalLicenseByDriverID(int DriverID)
        {
            int InternationalLicenseID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"  
                            SELECT Top 1 InternationalLicenseID
                            FROM InternationalLicenses 
                            where DriverID=@DriverID and GetDate() between IssueDate and ExpirationDate 
                            order by ExpirationDate Desc;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    InternationalLicenseID = insertedID;
                }
            }

            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

            }

            finally
            {
                connection.Close();
            }


            return InternationalLicenseID;
        }

        //public static int GetApplicationIDByInternationalLicenseID(int InternationalLicenseID)
        //{


        //    int ApplicationID = -1;

        //    SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

        //    string query = @"  
        //                    SELECT Top 1 InternationalLicenseID
        //                    FROM InternationalLicenses 
        //                    where DriverID=@DriverID and GetDate() between IssueDate and ExpirationDate 
        //                    order by ExpirationDate Desc;";

        //    SqlCommand command = new SqlCommand(query, connection);

        //    command.Parameters.AddWithValue("@DriverID", DriverID);

        //    try
        //    {
        //        connection.Open();

        //        object result = command.ExecuteScalar();

        //        if (result != null && int.TryParse(result.ToString(), out int insertedID))
        //        {
        //            InternationalLicenseID = insertedID;
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        //Console.WriteLine("Error: " + ex.Message);

        //    }

        //    finally
        //    {
        //        connection.Close();
        //    }


        //    return InternationalLicenseID;

        //}


        public static bool GetInternationalLicenseInfoByID(int InternationalLicenseID, ref int ApplicationID, ref int DriverID, ref int LocalLicenseID,
  ref DateTime IssueDate, ref DateTime ExpirationDate, ref bool IsActive, ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM InternationalLicenses WHERE InternationalLicenseID = @InternationalLicenseID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    LocalLicenseID = (int)reader["IssuedUsingLocalLicenseID"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    IsActive = (bool)reader["IsActive"];
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


        public static bool GetInternationalLicenseInfoByApplicationID(
    ref int InternationalLicenseID,
    int ApplicationID,
    ref int DriverID,
    ref int LocalLicenseID,
    ref DateTime IssueDate,
    ref DateTime ExpirationDate,
    ref bool IsActive,
    ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"SELECT * FROM InternationalLicenses
                     WHERE ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    InternationalLicenseID = (int)reader["InternationalLicenseID"];
                    DriverID = (int)reader["DriverID"];
                    LocalLicenseID = (int)reader["IssuedUsingLocalLicenseID"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    IsActive = (bool)reader["IsActive"];
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

        public static int AddNewInternationalLicense(
            int ApplicationID,
            int DriverID,
            int LocalLicenseID,
            DateTime IssueDate,
            DateTime ExpirationDate,
            bool IsActive,
            int CreatedByUserID)
        {
            int InternationalLicenseID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"INSERT INTO InternationalLicenses
        (ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID)
        VALUES
        (@ApplicationID, @DriverID, @LocalLicenseID, @IssueDate, @ExpirationDate, @IsActive, @CreatedByUserID);
        SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LocalLicenseID", LocalLicenseID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    InternationalLicenseID = insertedID;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to insert new international license. " + ex.Message, ex);
            }
            finally
            {
                connection.Close();
            }

            return InternationalLicenseID;
        }

        public static bool UpdateInternationalLicense(
            int InternationalLicenseID,
            int ApplicationID,
            int DriverID,
            int LocalLicenseID,
            DateTime IssueDate,
            DateTime ExpirationDate,
            bool IsActive,
            int CreatedByUserID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"UPDATE InternationalLicenses SET
        ApplicationID = @ApplicationID,
        DriverID = @DriverID,
        IssuedUsingLocalLicenseID = @LocalLicenseID,
        IssueDate = @IssueDate,
        ExpirationDate = @ExpirationDate,
        IsActive = @IsActive,
        CreatedByUserID = @CreatedByUserID
        WHERE InternationalLicenseID = @InternationalLicenseID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LocalLicenseID", LocalLicenseID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update international license. " + ex.Message, ex);
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }






    }
}
