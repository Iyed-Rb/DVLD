using DVLDBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DVLDPresentationLayer.frmTestAppointment;
using static System.Net.Mime.MediaTypeNames;

namespace DVLDPresentationLayer
{
    public partial class FrmScheduleTest : Form
    {
        int _LDLApplicationID;
        clsLDLApplication _LDLApplication;

        int _TestAppointmentID;
        clsTestAppointment _TestAppointment;

        private bool _IsFirstTime;

        clsApplication _Application;

        clsPerson _Person;
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;


        public enTest _Test;

        public FrmScheduleTest(int ApplicationID, enTest test, bool isFirstTime)
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
            _LDLApplicationID = ApplicationID;
            _Test = test;
            _IsFirstTime = isFirstTime;
            _Mode = enMode.AddNew;
        }
        public FrmScheduleTest(int ApplicationID, enTest test, int AppointmentID, bool isFirstTime)
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
            _LDLApplicationID = ApplicationID;
            _Test = test;
            _TestAppointmentID = AppointmentID;
            _IsFirstTime = isFirstTime;
            _Mode = enMode.Update;

        }

        private void _LoadData()
        {

            //dateTimePicker1.MinDate = DateTime.Today;
            //dateTimePicker1.Value = DateTime.Today;
            _LDLApplication = clsLDLApplication.FindLDLApplicationByID(_LDLApplicationID);

            if (_LDLApplication == null)
            {
                MessageBox.Show("LDL Application not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

            lbDLAppID.Text = _LDLApplication.LDLApplicationID.ToString();
            lbClassName.Text = _LDLApplication.LicenseClass.ClassName;

            _Person = clsPerson.FindPersonByID(_LDLApplication.Application.ApplicantPersonID);
            lbPersonFullName.Text = _Person.FirstName + " " + _Person.SecondName + " " + _Person.ThirdName + " " + _Person.LastName;

            int PassedTests = clsLDLApplication.GetPassedTestsCount(_LDLApplicationID);
            lbCountTests.Text = PassedTests.ToString();

            clsTestTypes clsTestTypes = clsTestTypes.FindTestTypeByID((int)_Test);
            lbFees.Text = clsTestTypes.TestTypeFees.ToString();
            MessageBox.Show("lbFees.Text = " + lbFees.Text);


            clsApplicationTypes ApplicationType = clsApplicationTypes.FindApplicationTypeByID(7);
            if (_Mode == enMode.AddNew)
            {
                _TestAppointment = new clsTestAppointment();
                dateTimePicker1.MinDate = DateTime.Today;
                dateTimePicker1.Value = DateTime.Today;

                if (_IsFirstTime)
                {
                    groupBox1.Enabled = false;
                }
                else
                {
                    groupBox1.Enabled = true;

                    lbRetakeAppFees.Text = ApplicationType.ApplicationTypeFees.ToString();
                    MessageBox.Show("lbRetakeAppFees.Text = " + lbRetakeAppFees.Text);
                    _Application = new clsApplication();
                    _Application.ApplicationDate = DateTime.Now;
                    decimal fees = Convert.ToDecimal(lbFees.Text);
                    decimal retakeFees = Convert.ToDecimal(lbRetakeAppFees.Text);
                    lbTotalFees.Text = (fees + retakeFees).ToString("0.00");


                }
                
                return;

            }

            _TestAppointment = clsTestAppointment.FindTestAppointmentByID(_TestAppointmentID);
            dateTimePicker1.Value = _TestAppointment.AppointmentDate;
            if (_IsFirstTime)
            {
                groupBox1.Enabled = false;
            }
            else
            {
                groupBox1.Enabled = true;
                lbRetakeAppFees.Text = ApplicationType.ApplicationTypeFees.ToString();
                MessageBox.Show("lbRetakeAppFees.Text = " + lbRetakeAppFees.Text);
                _Application = clsApplication.FindApplicationByID(_TestAppointment.RetakeTestApplicationID);

                decimal fees = Convert.ToDecimal(lbFees.Text);
                decimal retakeFees = Convert.ToDecimal(lbRetakeAppFees.Text);
                lbTotalFees.Text = (fees + retakeFees).ToString("0.00");
        

            }
        }

        private void FrmScheduleTest_Load(object sender, EventArgs e)
        {
           _LoadData();


        }

        private void btCLose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            _TestAppointment.TestTypeID = (int)_Test;
            _TestAppointment.LDLApplication.LDLApplicationID = _LDLApplicationID;
           
            _TestAppointment.AppointmentDate = dateTimePicker1.Value;
            clsTestTypes testTypes = clsTestTypes.FindTestTypeByID((int)_Test);
            _TestAppointment.PaidFees = testTypes.TestTypeFees;
            _TestAppointment.IsLocked = false;
            _TestAppointment.CreatedByUserID = clsGlobalSettings.CurrentUser.UserID;

            bool applicationSaved = true;
            if (_IsFirstTime)
            {
                _TestAppointment.RetakeTestApplicationID = -1;
            }
            else
            {
                _Application.ApplicantPersonID = _Person.PersonID;
               
                _Application.ApplicationTypeID = 7;
                _Application.ApplicationStatus = 1;
                _Application.LastStatusDate = DateTime.Now;

                clsApplicationTypes ApplicationTypes = clsApplicationTypes.FindApplicationTypeByID(7);
                _Application.PaidFees = ApplicationTypes.ApplicationTypeFees;
                _Application.CreatedByUserID = clsGlobalSettings.CurrentUser.UserID;

                applicationSaved = _Application.Save();

                if (!applicationSaved)
                {
                    MessageBox.Show("Retake application could not be saved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                _TestAppointment.RetakeTestApplicationID = _Application.ApplicationID;
                lbRetakeAppID.Text = _Application.ApplicationID.ToString();
            }


            if (_TestAppointment.Save())
                MessageBox.Show("Data Saved Successfully.");
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.");

            


        }
    }
}
