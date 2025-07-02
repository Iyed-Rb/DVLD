using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLDBusinessLayer
{
    public class clsTest
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int TestID { get; set; }
        public  clsTestAppointment TestAppointment { get; set; }
        public bool TotalResult { get; set; }
        public string Notes { get; set; } 
        public int CreatedByUserID { get; set; }

        clsTest()
        {
            this.TestID = -1;
            this.TestAppointment = new clsTestAppointment();
            this.TotalResult = false;
            this.Notes = string.Empty;
            this.CreatedByUserID = -1;
            Mode = enMode.AddNew;
        }

        clsTest(int testID, clsTestAppointment testAppointment, bool totalResult, string notes, int createdByUserID)
        {
            this.TestID = testID;
            this.TestAppointment = testAppointment;
            this.TotalResult = totalResult;
            this.Notes = notes;
            this.CreatedByUserID = createdByUserID;
            Mode = enMode.Update;
        }

        public static clsTest FindTestByID(int TestID)
        {
            int TestAppointmentID = -1, CreatedByUserID = -1;
            bool TotalResult = false;
            string Notes = "";

            if (clsTestsData.GetTestInfoByID(TestID, ref TestAppointmentID, ref TotalResult, ref Notes, ref CreatedByUserID))
            {
                clsTestAppointment testAppointment = clsTestAppointment.FindTestAppointmentByID(TestAppointmentID);
                if (testAppointment == null)
                    return null;

                return new clsTest(TestID, testAppointment, TotalResult, Notes, CreatedByUserID);
            }

            return null;
        }

        public bool _AddNewTest()
        {
           this.TestID =  clsTestsData.AddNewTest(this.TestAppointment.TestAppointmentID, TotalResult, Notes, CreatedByUserID);
            return (this.TestID != -1);
        }

        public  bool _UpdateTest()
        {
            return clsTestsData.UpdateTest(this.TestID, this.TestAppointment.TestAppointmentID, this.TotalResult, this.Notes, this.CreatedByUserID);
        }

        public static bool _DeleteTest(int TestID)
        {
       

            clsTest Test = FindTestByID(TestID);

            if (Test == null)
                return false;

            // 1. Delete the LDL application first (child)
            if (!clsTest._DeleteTest(TestID))
                return false;

            // 2. Then delete the related application (parent)
            return clsTestAppointment.DeleteTestAppointment(Test.TestAppointment.TestAppointmentID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (TestAppointment.Save()) // Save Application first
                    {

                        if (_AddNewTest())
                        {
                            Mode = enMode.Update;
                            return true;
                        }
                    }
                    return false;

                case enMode.Update:
                    if (TestAppointment.Save()) // Save Application (updated)
                    {
                        return _UpdateTest(); // Then update LDL row
                    }
                    return false;
            }

            return false;
        }



    }
}
