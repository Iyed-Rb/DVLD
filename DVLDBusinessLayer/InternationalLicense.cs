using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsInternationalLicense
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int InternationalLicenseID { get; set; }

        public  clsApplication Application { get; set; }
        public int DriverID { get; set; }   
        public int LocalLicenseID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }  
        public int CreatedByUserID { get; set; }

        public int ApplicationID
        {
            get => Application.ApplicationID;
            set
            {
                Application = clsApplication.FindApplicationByID(value);
                if (Application == null)
                    Application = new clsApplication();

                Application.ApplicationID = value;

            }
        }
      
        public clsInternationalLicense()
        {
            InternationalLicenseID= -1;
            Application = new clsApplication(); 
            DriverID = -1;
            LocalLicenseID = -1;
            IssueDate = DateTime.Now;
            ExpirationDate = DateTime.Now;
            IsActive = false;
            CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }

        clsInternationalLicense(int  internationalLicenseID, int ApplicationID, int DriverID, int LocalLicenseID, DateTime IssueDate,
        DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            this.InternationalLicenseID = internationalLicenseID;
            this.ApplicationID =  ApplicationID;
            this.DriverID = DriverID;
            this.LocalLicenseID = LocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;
            this.CreatedByUserID = CreatedByUserID;

            Mode = enMode.Update;

        }

        //public static int GetApplicationIDByInternationalLicenseID()
        //{

        //}

        public static clsInternationalLicense FindInternationalLicenseByID(int InternationalLicenseID)
        {
            int ApplicationID = -1;
            int DriverID = -1;
            int LocalLicenseID = -1;
            DateTime IssueDate = DateTime.MinValue;
            DateTime ExpirationDate = DateTime.MinValue;
            bool IsActive = false;
            int CreatedByUserID = -1;

            if (clsInternationalLicenseData.GetInternationalLicenseInfoByID(InternationalLicenseID, ref ApplicationID, ref DriverID, ref LocalLicenseID,
              ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID))
            {

                clsApplication application = clsApplication.FindApplicationByID(ApplicationID);

                if (application == null)
                {
                    return null; // If application or license class is not found, return null
                }

                return new clsInternationalLicense(InternationalLicenseID, ApplicationID, DriverID, LocalLicenseID, IssueDate,
                                 ExpirationDate, IsActive, CreatedByUserID);
            }
            else
                return null;
        }

        public static DataTable GetAllInternationalLicenses()
        {
            return clsInternationalLicenseData.GetAllInternationalLicences();
        }

        public static DataTable GetInternationalLicensesByPersonID(int PersonID)
        {
            return clsInternationalLicenseData.GetAllInternationalLicencesByPersonID(PersonID);
        }

        public static int GetActiveInternationalLicenseIDByDriverID(int DriverID)
        {

            return clsInternationalLicenseData.CheckIfHadBeforeInternationalLicenseByDriverID(DriverID);

        }

        public bool _AddNewInternationaLicense()
        {
            this.InternationalLicenseID = clsInternationalLicenseData.AddNewInternationalLicense(
                this.Application.ApplicationID, this.DriverID, this.LocalLicenseID, this.IssueDate, 
                this.ExpirationDate, this.IsActive, this.CreatedByUserID);
            return (this.InternationalLicenseID != -1);
        }

        public bool _UpdateInternationalLicense()
        {
            return clsInternationalLicenseData.UpdateInternationalLicense(
                this.InternationalLicenseID, this.Application.ApplicationID, this.DriverID, 
                this.LocalLicenseID, this.IssueDate, this.ExpirationDate, 
                this.IsActive, this.CreatedByUserID);

        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (Application.Save()) // Save Application first
                    {

                        if (_AddNewInternationaLicense())
                        {
                            Mode = enMode.Update;
                            return true;
                        }
                    }
                    return false;

                case enMode.Update:
                    if (Application.Save()) // Save Application (updated)
                    {
                        return _UpdateInternationalLicense(); // Then update LDL row
                    }
                    return false;
            }

            return false;
        }


    }
}
