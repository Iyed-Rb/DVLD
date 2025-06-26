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
    public class clsTestTypes
    {
        public int TestTypeID { get; set; }
        public string TestTypeTitle { get; set; }
        public string TestTypeDescription { get; set; }
        public decimal TestTypeFees { get; set; }

        clsTestTypes()
        {

            TestTypeID = 0;
            TestTypeTitle = "";
            TestTypeDescription = "";
            TestTypeFees = 0.0m;
        }

        clsTestTypes(int testTypesID, string testTypesTitle, string testTypesDescription, decimal testTypesFees)
        {
            this.TestTypeID = testTypesID;
            this.TestTypeTitle = testTypesTitle;
            this.TestTypeDescription = testTypesDescription;
            this.TestTypeFees = testTypesFees;
        }

        public static DataTable GetAllTestTypesList()
        {
            return clsTestTypesData.GetAllTestTypesList();
        }

        public static clsTestTypes FindTestTypeByID(int TestTypeID)
        {
            string TestTypeTitle = "", TestTypeDescription = "";
            decimal TestTypeFees = -1;
            if (clsTestTypesData.GetTestTypeInfoByID(TestTypeID, ref TestTypeTitle, ref TestTypeDescription,
                ref TestTypeFees))
            {
                return new clsTestTypes(TestTypeID, TestTypeTitle, TestTypeDescription, TestTypeFees);
            }
            else
                return null;
        }

        public static bool IsTestTypeTitleExists(string TestTypeTitle)
        {
            return clsTestTypesData.IsTitleExist(TestTypeTitle);
        }

        public static bool IsTestTypesDescriptionExists(string TestTypeDescription)
        {
            return clsTestTypesData.IsDescriptionExist(TestTypeDescription);
        }

        public bool _UpdateTestType()
        {
            return clsTestTypesData.UpdateTestType(this.TestTypeID, this.TestTypeTitle, this.TestTypeDescription, this.TestTypeFees);

        }
    }
}
