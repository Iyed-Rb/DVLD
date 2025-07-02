using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsTestAppointment
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int TestAppointmentID { get; set; }
        public int TestTypeID { get; set; }
        public clsLDLApplication LDLApplication { get; set; }
        public DateTime AppointmentDate { get; set; }
        public decimal PaidFees { get; set; }

        public int CreatedByUserID { get; set; }
        public bool IsLocked { get; set; }
        public int RetakeTestApplicationID { get; set; }

        //public int LDLApplicationID
        //{
        //    get { return LDLApplication.LDLApplicationID; }
        //    set
        //    {
        //        LDLApplication = clsLDLApplication.FindLDLApplicationByID(value);
        //        if (LDLApplication == null)
        //            throw new Exception($"LDLApplication with ID {value} not found.");
        //    }
        //}

        public clsTestAppointment()
        {
            TestAppointmentID = -1;
            TestTypeID = -1;
            LDLApplication = new clsLDLApplication();
            AppointmentDate = DateTime.Now;
            PaidFees = -1;
            CreatedByUserID = -1;
            IsLocked = false;
            RetakeTestApplicationID = -1;
            Mode = enMode.AddNew;

        }

        clsTestAppointment(int TestAppointmentID, int TestTypeID, clsLDLApplication lDLApplication, DateTime AppointmentDate,
            decimal PaidFees, int CreatedByUserID, bool IsLocked, int RetakeTestApplicationID)
        {
            this.TestAppointmentID = TestAppointmentID;
            this.TestTypeID = TestTypeID;
            this.LDLApplication = lDLApplication;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsLocked = IsLocked;
            this.RetakeTestApplicationID = RetakeTestApplicationID;
            Mode = enMode.Update;

        }

        public static DataTable GetAllAppointmentsByTestTypeID(int TestTypeID, int PersonID)
        {
            return clsTestAppointmentData.GetAllAppointmentsByTestTypeID(TestTypeID, PersonID);
        }

        public static clsTestAppointment FindTestAppointmentByID(int TestAppointmentID)
        {
            int TestTypeID = -1, LDLApplicationID = -1, CreatedByUserID = -1, RetakeTestApplicationID = -1;
            DateTime AppointmentDate = DateTime.MinValue;
            decimal PaidFees = -1;
            bool IsLocked = false;

            if (clsTestAppointmentData.GetTestAppointmentInfoByID(TestAppointmentID,
                ref TestTypeID, ref LDLApplicationID, ref AppointmentDate, ref PaidFees,
                ref CreatedByUserID, ref IsLocked, ref RetakeTestApplicationID))
            {
                clsLDLApplication ldlApplication = clsLDLApplication.FindLDLApplicationByID(LDLApplicationID);
                if (ldlApplication == null)
                    return null;

                return new clsTestAppointment(TestAppointmentID, TestTypeID, ldlApplication, AppointmentDate,
                    PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);
            }

            return null;
        }

        public  bool _AddNewTestAppointment()
        {
            this.TestAppointmentID = clsTestAppointmentData.AddNewTestAppointment(this.TestTypeID, this.LDLApplication.LDLApplicationID,
            this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked, this.RetakeTestApplicationID);

            return (this.TestAppointmentID != -1);

        }

        public  bool _UpdateTestAppointment()
        {
            return clsTestAppointmentData.UpdateTestAppointment(this.TestAppointmentID, this.TestTypeID,
               LDLApplication.LDLApplicationID, this.AppointmentDate,
                this.PaidFees, this.CreatedByUserID, this.IsLocked, this.RetakeTestApplicationID);
        }

        public static bool DeleteTestAppointment(int TestAppointmentID)
        {

            return clsTestAppointmentData.DeleteTestAppointment(TestAppointmentID);
        }

        public bool Save()
        {

            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestAppointment())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateTestAppointment();

            }

            return false;
        }

    }
}
