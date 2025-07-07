using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLDBusinessLayer
{
    public class clsLDLApplication
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int LDLApplicationID { get; set; }
        public clsApplication Application { get; set; }

        public clsLicenseClass LicenseClass { get; set; }

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
        public int LicenseClassID
        {
            get => LicenseClass.LicenseClassID;
            set
            {
                LicenseClass = clsLicenseClass.FindLicenseClassByID(value);
                if (LicenseClass == null)
                    LicenseClass = new clsLicenseClass();

                LicenseClass.LicenseClassID = value;
            }
        }

        public clsLDLApplication()
        {
            this.LDLApplicationID = -1;
            this.Application = new clsApplication();
            this.LicenseClass = new clsLicenseClass();
            Mode = enMode.AddNew;
        }

        private clsLDLApplication(int ldlApplicationID, clsApplication Application, clsLicenseClass licenseClass)
        {
            this.LDLApplicationID = ldlApplicationID;
            this.Application = Application;
            this.LicenseClass = licenseClass;
            Mode = enMode.Update;
        }


        public static int GetPassedTestsCount(int ldlApplicationID)
        {
            return clsLDLApplicationData.GetPassedTestsCount(ldlApplicationID);
        }
     
        public static DataTable GetAllLDLApplications()
        {
            return clsLDLApplicationData.GetAllLDLApplications();
        }

        public static clsLDLApplication FindLDLApplicationByID(int LDLApplicationID)
        {
            int ApplicationID = -1, LicenseClassID = -1;

            if (clsLDLApplicationData.GetLDLApplicationInfoByID(LDLApplicationID, ref ApplicationID, ref LicenseClassID))
            {
                clsApplication application = clsApplication.FindApplicationByID(ApplicationID);
                clsLicenseClass licenseClass = clsLicenseClass.FindLicenseClassByID(LicenseClassID);


                if (application == null && licenseClass == null)
                {
                    return null; // If application or license class is not found, return null
                }

                return new clsLDLApplication(LDLApplicationID, application, licenseClass);
            }
            else
                return null;
        }

        private  bool _AddNewLDLApplication()
        {
            this.LDLApplicationID = clsLDLApplicationData.AddNewLDLApplication(Application.ApplicationID, LicenseClass.LicenseClassID);
            return (this.LDLApplicationID != -1);
        }

        private bool _UpdateLDLApplication()
        {
            return clsLDLApplicationData.UpdateLDLApplication(this.LDLApplicationID, this.Application.ApplicationID, this.LicenseClass.LicenseClassID);
        }

        public static bool DeleteLDLApplication(int LDLApplicationID)
        {

            clsLDLApplication LDL = FindLDLApplicationByID(LDLApplicationID);

            if (LDL == null)
                return false;

            // 1. Delete the LDL application first (child)
            if (!clsLDLApplicationData.DeleteLDLApplication(LDLApplicationID))
                return false;

            // 2. Then delete the related application (parent)
            return clsApplication.DeleteApplication(LDL.Application.ApplicationID);


        } 
        public static bool HasLDLApplicationBefore(string NationalNo, int LicenseClassID)
        {
            return clsLDLApplicationData.HasSameLDLApplication(NationalNo,LicenseClassID);
        }

        //public bool Save()
        //{
        //    switch (Mode)
        //    {
        //        case enMode.AddNew:
        //            if (!Application.Save())
        //                throw new Exception("Failed to save Application.");

        //            if (!_AddNewLDLApplication())
        //                throw new Exception("Failed to add new LDL application.");

        //            Mode = enMode.Update;
        //            return true;

        //        case enMode.Update:
        //            if (!Application.Save())
        //                throw new Exception("Failed to update Application.");

        //            if (!_UpdateLDLApplication())
        //                throw new Exception("Failed to update LDL application.");

        //            return true;
        //    }

        //    return false;
        //}


        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (Application.Save()) // Save Application first
                    {

                        if (_AddNewLDLApplication())
                        {
                            Mode = enMode.Update;
                            return true;
                        }
                    }
                    return false;

                case enMode.Update:
                    if (Application.Save()) // Save Application (updated)
                    {
                        return _UpdateLDLApplication(); // Then update LDL row
                    }
                    return false;
            }

            return false;
        }

    }
}
