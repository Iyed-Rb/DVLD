using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsDriver
    {
        //public enum enMode { AddNew = 0, Update = 1 };
        //public enMode Mode = enMode.AddNew;
        public int DriverID{ get; set; }
        public int PersonID { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }

        public clsDriver()
        {
            DriverID = -1;
            PersonID = -1;
            CreatedByUserID = -1;
            CreatedDate = DateTime.Now;

        }

        // Private constructor for internal use when loading an existing driver from DB
        private clsDriver(int driverID, int personID, int createdByUserID, DateTime createdDate)
        {
            this.DriverID = driverID;
            this.PersonID = personID;
            this.CreatedByUserID = createdByUserID;
            this.CreatedDate = createdDate;
         
        }

        public static DataTable GetAllDriversList()
        {
            return clsDriversData.GetAllDrivers();
        }

        public static clsDriver FindDriverByID(int DriverID)
        {
            int PersonID = -1, createdByUserID = -1;
            DateTime createdDate = DateTime.MinValue;


            if (clsDriversData.GetDriverInfoByID(DriverID, ref PersonID,ref createdByUserID,ref createdDate))
            {
                return new clsDriver(DriverID, PersonID, createdByUserID, createdDate);
            }
            else
                return null;
        }

        public static clsDriver FindDriverByPersonID(int PersonID)
        {
            int DriverID = -1, createdByUserID = -1;
            DateTime createdDate = DateTime.MinValue;


            if (clsDriversData.GetDriverInfoByPersonID(ref DriverID, PersonID, ref createdByUserID, ref createdDate))
            {
                return new clsDriver(DriverID, PersonID, createdByUserID, createdDate);
            }
            else
                return null;
        }


        public static bool IsDriverExistByPersonID(int PersonID)
        {
            return clsDriversData.IsDriverExistByPersonID(PersonID);
        }

        public bool _AddNewDriver()
        {
            this.DriverID = clsDriversData.AddNewDriver(this.PersonID, this.CreatedByUserID, this.CreatedDate);
            return (this.DriverID != -1);
        }

    }
}
