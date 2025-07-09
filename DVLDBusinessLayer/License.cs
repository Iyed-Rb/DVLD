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
        public clsApplication Application { get; set; }
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

        public int ApplicationID
        {
            get => Application.ApplicationID;
            set
            {
                Application = clsApplication.FindApplicationByID(value);

                if (Application == null)
                    Application = new clsApplication();
            }
        }


        public clsLicense()
        {
            LicenseID = -1;
            Application = new clsApplication();
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

        private clsLicense(int licenseID, int applicationID, int driverID, int licenseClassID,
            DateTime issueDate, DateTime expirationDate, string notes, decimal paidFees,
            bool isActive, int issueReason, int createdByUserID)
        {
            LicenseID = licenseID;
            this.ApplicationID = applicationID;
            this.DriverID = driverID;
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
                clsApplication Application = clsApplication.FindApplicationByID(applicationID);
          
                if (Driver == null || Application == null)
                {
                    return null; // If application or license class is not found, return null
                }

                return new clsLicense(LicenseID, applicationID, driverID, licenseClassID,
                    issueDate, expirationDate, notes, paidFees, isActive, issueReason, createdByUserID);
            }
            else
                return null;

        }

        private bool _AddNewLicense()
        {
            this.LicenseID = clsLicensesData.AddNewLicense(this.ApplicationID, this.Driver.DriverID, this.LicenseClassID,
                this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive, this.IssueReason, this.CreatedByUserID);

            return (this.LicenseID != -1);
        }

        private bool _UpdateLicense()
        {
            return clsLicensesData.UpdateLicense(this.LicenseID, this.ApplicationID, this.Driver.DriverID, this.LicenseClassID,
                this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive, this.IssueReason, this.CreatedByUserID);
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

                return new clsLicense(LicenseID, ApplicationID, driverID, licenseClassID,
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

                    if (Application.Save()) // Save Application first
                    {

                        if (_AddNewLicense())
                        {
                            Mode = enMode.Update;
                            return true;
                        }
                    }
                    return false;

                case enMode.Update:
                    return _UpdateLicense();
            }

            return false;
        }

    }
}
