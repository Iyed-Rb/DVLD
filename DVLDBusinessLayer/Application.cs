using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsApplicationTypes
    {

        public int ApplicationTypeID { get; set; }
        public string ApplicationTypeTitle { get; set; }
        public decimal ApplicationTypeFees {get; set; }

        public clsApplicationTypes()
        {
            ApplicationTypeID = -1;
            ApplicationTypeTitle = "";
            ApplicationTypeFees = -1;
        }

        private clsApplicationTypes(int ApplicationTypeID, string ApplicationTypeTitle, decimal ApplicationTypeFees)
        {
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeTitle = ApplicationTypeTitle;
            this.ApplicationTypeFees = ApplicationTypeFees;
        }
        public static DataTable GetAllApplicationsTypes()
        {
            return clsApplicationsData.GetAllApplicationsTypes();
        }

        public static clsApplicationTypes FindApplicationTypeByID(int ApplicationTypeID)
        {
            string ApplicationTypeTitle = "";
            decimal ApplicationTypeFees = -1;
            if (clsApplicationsData.GetApplicationTypeInfoByID(ApplicationTypeID, ref ApplicationTypeTitle, ref ApplicationTypeFees) )     
            {
                return new clsApplicationTypes(ApplicationTypeID, ApplicationTypeTitle, ApplicationTypeFees);
            }
            else
                return null;
        }

        public bool _UpdateApplicationType()
        {
            return clsApplicationsData.UpdateApplicationType(this.ApplicationTypeID, this.ApplicationTypeTitle, this.ApplicationTypeFees);

        }

        public static bool IsApplicationTypeTitleExists(string ApplicationTypeTitle)
        {
            return clsApplicationsData.IsTitleExist(ApplicationTypeTitle);
        }


    }
}
