using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccessLayer
{
    public class clsTestsData
    {

        public static bool GetTestInfoByID(int TestID, ref int TestAppointmentID, ref bool TotalResult,
         ref string Notes,
          ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM Tests WHERE TestID = @TestID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestID", TestID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    TotalResult = (bool)reader["TotalResult"];
                    Notes = reader["Notes"] != DBNull.Value ? reader["Notes"].ToString() : string.Empty;
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
                throw new Exception("An error occurred while retrieving Test info: " + ex.Message, ex);
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }


        public static int AddNewTest(int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            int TestID = -1;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
        INSERT INTO Tests (TestAppointmentID, TestResult, Notes, CreatedByUserID)
        VALUES (@TestAppointmentID, @TestResult, @Notes, @CreatedByUserID);

        SELECT SCOPE_IDENTITY();";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
                command.Parameters.AddWithValue("@TestResult", TestResult);
                command.Parameters.AddWithValue("@Notes", Notes ?? string.Empty);
                command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    {
                        TestID = insertedID;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to insert Test: " + ex.Message, ex);
                }
            }

            return TestID;
        }

        public static bool UpdateTest(int TestID, int TestAppointmentID, bool TotalResult, string Notes, int CreatedByUserID)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
        UPDATE Tests
        SET TestAppointmentID = @TestAppointmentID,
            TotalResult = @TotalResult,
            Notes = @Notes,
            CreatedByUserID = @CreatedByUserID
        WHERE TestID = @TestID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
                command.Parameters.AddWithValue("@TotalResult", TotalResult);
                command.Parameters.AddWithValue("@Notes", Notes ?? string.Empty);
                command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                command.Parameters.AddWithValue("@TestID", TestID);

                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to update Test: " + ex.Message, ex);
                }
            }

            return rowsAffected > 0;
        }


        public static bool DeleteTest(int TestID)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"DELETE FROM Tests WHERE TestID = @TestID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TestID", TestID);

                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to delete Test: " + ex.Message, ex);
                }
            }

            return rowsAffected > 0;
        }

    }
}
