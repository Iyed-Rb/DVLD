using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public  class clsUser
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        clsUser()
        {
            UserID = -1;
            PersonID = -1;
            UserName = "";
            Password = "";
            IsActive = true;

        }

        clsUser (int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {

            this.UserID = UserID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;
        }


        public static DataTable GetAllUsers()
        {
            return clsUsersData.GetAllUsers();

        }

        public static bool FindUserByUserNameAndPassword(string Username, string Password)
        {
            return clsUsersData.FindUserByUsernameAndPassword(Username, Password);
        }   

        public static bool UserIsActive(string Username)
        {
            return clsUsersData.IsActive(Username);
        }

        public static clsUser Find(string Username)
        {

            int UserID = -1, PersonID = -1;
            string UserName = "", Password = ""; 
            bool IsActive = false;
            if (clsUsersData.GetUserInfoByUsername(ref UserID, ref PersonID, UserName,  ref Password, ref IsActive) )
            {
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            }
            else
                return null;
 

            
        }


    }
}
