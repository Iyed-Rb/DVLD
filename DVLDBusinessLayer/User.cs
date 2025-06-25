using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsUser
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public clsUser()
        {
            UserID = -1;
            PersonID = -1;
            UserName = "";
            Password = "";
            IsActive = true;
            Mode = enMode.AddNew;

        }

        private clsUser (int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {

            this.UserID = UserID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;

            Mode = enMode.Update;
        }


        public static DataTable GetAllUsers()
        {
            return clsUsersData.GetAllUsers();

        }

        public static bool FindUserByUserNameAndPassword(string Username, string Password)
        {
            return clsUsersData.FindUserByUsernameAndPassword(Username, Password);
        }   

        public static bool IsUserExistByUsername(string Username)
        {
            return clsUsersData.IsUserExistByUsername(Username);
        }

        public static bool UserIsActive(string Username)
        {
            return clsUsersData.IsActive(Username);
        }

        public static clsUser FindUserByUsername(string UserName)
        {

            int UserID = -1, PersonID = -1;
                 string Password = ""; 
            bool IsActive = false;
            if (clsUsersData.GetUserInfoByUsername(ref UserID, ref PersonID, UserName,  ref Password, ref IsActive) )
            {
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            }
            else
                return null;
        }

        public static clsUser FindUserByID(int UserID)
        {

            int PersonID = -1;
            string UserName = "", Password = "";
            bool IsActive = false;
            if (clsUsersData.GetUserInfoByID(UserID, ref PersonID,ref UserName, ref Password, ref IsActive))
            {
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            }
            else
                return null;
        }

        public static bool IsUserExistByPersonID(int PersonID)
        {
            return clsUsersData.IsUserExistByPersonID(PersonID);
        }

        private bool _AddNewUser()
        {
            //call DataAccess Layer 

            this.UserID = clsUsersData.AddNewUser(this.PersonID, this.UserName, this.Password, this.IsActive);

            return (this.UserID != -1);
        }

        private bool _UpdateUser()
        {
            return clsUsersData.UpdateUser(this.UserID, this.PersonID, this.UserName, this.Password, this.IsActive);
  
        }

        public static bool DeleteUser(int UserID)
        {
            //call DataAccess Layer 
            return clsUsersData.DeleteUser(UserID);
        }

        public bool Save()
        {


            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateUser();

            }

            return false;
        }

        //public bool Save()
        //{
        //    switch (Mode)
        //    {
        //        case enMode.AddNew:
        //            if (_AddNewUser())
        //            {
        //                Mode = enMode.Update;
        //                return true;
        //            }
        //            else
        //            {
        //                throw new Exception("Failed to add new user.");
        //            }

        //        case enMode.Update:
        //            if (_UpdateUser())
        //            {
        //                return true;
        //            }
        //            else
        //            {
        //                throw new Exception("Failed to update user.");
        //            }

        //        default:
        //            throw new InvalidOperationException("Invalid mode in Save(): " + Mode);
        //    }
        //}


    }
}
