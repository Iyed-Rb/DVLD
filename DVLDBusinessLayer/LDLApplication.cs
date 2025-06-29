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

        public int ApplicationID => Application.ApplicationID;
        public int LicenseClassID => LicenseClass.LicenseClassID;


        public clsLDLApplication()
        {
            this.LDLApplicationID = -1;
            this.Application = new clsApplication();
            this.LicenseClass = new clsLicenseClass();
        }

        private clsLDLApplication(int ldlApplicationID, clsApplication Application, clsLicenseClass licenseClass)
        {
            this.LDLApplicationID = ldlApplicationID;
            this.Application = Application;
            this.LicenseClass = licenseClass;
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


                if (application != null && licenseClass != null)
                {
                    return new clsLDLApplication(LDLApplicationID, application, licenseClass);
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
            return clsLDLApplicationData.UpdateLDLApplication(this.LDLApplicationID, Application.ApplicationID, LicenseClass.LicenseClassID);
        }

        public static bool DeleteLDLApplication(int LDLApplicationID)
        { 

            clsLDLApplication LDL = FindLDLApplicationByID(LDLApplicationID);

            if (LDL == null)
                return false;

            // 2. Delete the Application first
            if (!clsApplication.DeleteApplication(LDL.Application.ApplicationID))
                return false;

            // 3. Then delete the LDL application
            return clsLDLApplicationData.DeleteLDLApplication(LDLApplicationID);
        } 
        public static bool HasLDLApplicationBefore(string NationalNo, int LicenseClassID)
        {
            return clsLDLApplicationData.HasSameLDLApplication(NationalNo,LicenseClassID);
        }

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
