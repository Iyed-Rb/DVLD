using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLDBusinessLayer
{
    public class clsLicense
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }
        public clsDriver Driver { get; set; }
        public int LicenseClassID   { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }   
        public string Notes     { get; set; }
        public decimal PaidFees { get; set; }
        public bool IsActive  { get; set; }
        public int IssueReason { get; set; }
        public int CreatedByUserID { get; set; }

        public int DriverID
        {
            get => Driver.DriverID;
            set
            {
              
                Driver = clsDriver.FindDriverByID(value);

                if (Driver == null)
                    Driver = new clsDriver();
            }
        }

        public clsLicense()
        {
            LicenseID = -1;
            ApplicationID = -1;
            Driver = new clsDriver();
            LicenseClassID = -1;
            IssueDate = DateTime.MinValue;
            ExpirationDate = DateTime.MinValue;
            Notes = string.Empty;
            PaidFees = -1;
            IsActive = false;
            IssueReason = -1;
            CreatedByUserID = -1;
            Mode = enMode.AddNew;
        }

        private clsLicense(int licenseID, int applicationID, clsDriver driver, int licenseClassID,
            DateTime issueDate, DateTime expirationDate, string notes, decimal paidFees,
            bool isActive, int issueReason, int createdByUserID)
        {
            LicenseID = licenseID;
            ApplicationID = applicationID;
            this.Driver = driver;
            LicenseClassID = licenseClassID;
            IssueDate = issueDate;
            ExpirationDate = expirationDate;
            Notes = notes;
            PaidFees = paidFees;
            IsActive = isActive;
            IssueReason = issueReason;
            CreatedByUserID = createdByUserID;

            Mode = enMode.Update;
        }

        public static DataTable GetAllLicensesList(int PersonID)
        {
            return clsLicensesData.GetAllLocalLicencesByPersonID(PersonID);  
        }


        public static clsLicense FindLicenseByID(int LicenseID)
        {
            int applicationID = -1, driverID = -1, licenseClassID = -1,
                issueReason = -1, createdByUserID = -1;
            DateTime issueDate = DateTime.MinValue, expirationDate = DateTime.MinValue;
            string notes = string.Empty;
            decimal paidFees = -1;
            bool isActive = false;


            if (clsLicensesData.GetLicenseInfoByID(LicenseID, ref applicationID, ref driverID,ref licenseClassID,
            ref issueDate,ref expirationDate,ref notes,ref paidFees, ref isActive, ref issueReason,ref createdByUserID))
            {
                clsDriver Driver = clsDriver.FindDriverByID(driverID);  
          
                if (Driver == null)
                {
                    return null; // If application or license class is not found, return null
                }

                return new clsLicense(LicenseID, applicationID, Driver, licenseClassID,
                    issueDate, expirationDate, notes, paidFees, isActive, issueReason, createdByUserID);
            }
            else
                return null;

        }

        private bool _AddNewLicense()
        {
            LicenseID = clsLicensesData.AddNewLicense(ApplicationID, Driver.DriverID, LicenseClassID,
                IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUserID);

            return (LicenseID != -1);
        }

        private bool _UpdateLicense()
        {
            return clsLicensesData.UpdateLicense(LicenseID, ApplicationID, this.Driver.DriverID, LicenseClassID,
                IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUserID);
        }

        public static clsLicense FindLicenseByApplicationID(int ApplicationID)
        {
            int LicenseID = -1, driverID = -1, licenseClassID = -1,
                issueReason = -1, createdByUserID = -1;
            DateTime issueDate = DateTime.MinValue, expirationDate = DateTime.MinValue;
            string notes = string.Empty;
            decimal paidFees = -1;
            bool isActive = false;


            if (clsLicensesData.GetLicenseInfoByApplicationID(ref LicenseID, ApplicationID, ref driverID, ref licenseClassID,
            ref issueDate, ref expirationDate, ref notes, ref paidFees, ref isActive, ref issueReason, ref createdByUserID))
            {
                clsDriver Driver = clsDriver.FindDriverByID(driverID);

                if (Driver == null)
                {
                    return null; // If application or license class is not found, return null
                }

                return new clsLicense(LicenseID, ApplicationID, Driver, licenseClassID,
                    issueDate, expirationDate, notes, paidFees, isActive, issueReason, createdByUserID);
            }
            else
                return null;

        }

        
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:

                    // Ensure driver is added if not already existing
                    if (!clsDriver.IsDriverExistByPersonID(Driver.PersonID))
                    {
                        if (!Driver._AddNewDriver())
                            return false;
                    }

                    if (_AddNewLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }

                    return false;

                case enMode.Update:
                    return _UpdateLicense();
            }

            return false;
        }



    }
}
