using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsApplication
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int ApplicationID { get; set; }
        public int ApplicantPersonID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public int ApplicationTypeID { get; set; }
        public int ApplicationStatus { get; set; }

        public DateTime LastStatusDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserID { get; set; }

        public clsApplication()
        {
            ApplicationID = -1;
            ApplicantPersonID = -1;
            ApplicationDate = DateTime.MinValue;
            ApplicationTypeID = -1;
            ApplicationStatus = -1;
            LastStatusDate = DateTime.MinValue;
            PaidFees = -1;
            CreatedByUserID = -1;
            Mode = enMode.AddNew;
        }

        private clsApplication(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate,
            int ApplicationTypeID, int ApplicationStatus, DateTime LastStatusDate, decimal PaidFees, int CreatedByUserID)
        {
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;

            Mode = enMode.Update;
        }

        //public static DataTable  GetAllApplicationsList()
        //{
        //    return clsApplicationsData.GetAllApplications();
        //}

        public static clsApplication FindApplicationByID(int ApplicationID)
        {
            int ApplicantPersonID = -1;
            DateTime ApplicationDate = DateTime.MinValue;
            int ApplicationTypeID = -1;
            int ApplicationStatusID = -1;
            DateTime LastStatusDate = DateTime.MinValue;
            decimal PaidFees = 0;
            int CreatedByUserID = -1;

            if (clsApplicationsData.GetApplicationInfoByID(ApplicationID, ref ApplicantPersonID, ref ApplicationDate, ref ApplicationTypeID,
              ref ApplicationStatusID, ref LastStatusDate, ref PaidFees, ref CreatedByUserID))
            {
                return new clsApplication(ApplicationID, ApplicantPersonID, ApplicationDate, ApplicationTypeID, ApplicationStatusID,
                                 LastStatusDate,PaidFees, CreatedByUserID);
            }
            else
                return null;
        }

     

        public static clsApplication FindApplicationByPersonIDAnd(int ApplicationID, int LicenceClass)
        {
            int ApplicantPersonID = -1;
            DateTime ApplicationDate = DateTime.MinValue;
            int ApplicationTypeID = -1;
            int ApplicationStatusID = -1;
            DateTime LastStatusDate = DateTime.MinValue;
            decimal PaidFees = 0;
            int CreatedByUserID = -1;

            if (clsApplicationsData.GetApplicationInfoByID(ApplicationID, ref ApplicantPersonID, ref ApplicationDate, ref ApplicationTypeID,
              ref ApplicationStatusID, ref LastStatusDate, ref PaidFees, ref CreatedByUserID))
            {
                return new clsApplication(ApplicationID, ApplicantPersonID, ApplicationDate, ApplicationTypeID, ApplicationStatusID,
                                 LastStatusDate, PaidFees, CreatedByUserID);
            }
            else
                return null;
        }


        private bool _AddNewApplication()
        {
            //call DataAccess Layer 

            this.ApplicationID = clsApplicationsData.AddNewApplication(this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypeID,
              this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);

            return (this.ApplicationID != -1);
        }

        private bool _UpdateApplication()
        {
            return clsApplicationsData.UpdateApplication(this.ApplicationID, this.ApplicantPersonID,
                this.ApplicationTypeID, this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);
        }

        public static bool DeleteApplication(int ApplicationID)
        {
            //call DataAccess Layer 
            return clsApplicationsData.DeleteApplication(ApplicationID);
        }

        public static int HasSameApplicationBefore(int PersonID, int LicenseClassID)
        {
            return clsApplicationsData.GetExistingApplicationID(PersonID, LicenseClassID);
        }

        public static bool CancelApplication(int ApplicationID)
        {

            return clsApplicationsData.CancelApplication(ApplicationID);

        }

        public bool Save()
        {

            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplication())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateApplication();

            }

            return false;
        }

    }
}
