using DVLDBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
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

        int _CountRow;
        public enTest _Test;

        bool _IsLocked;
        public FrmScheduleTest(int LDLApplicationID, enTest test, bool isFirstTime, int CountRow)
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
            _LDLApplicationID = LDLApplicationID;
            _Test = test;
            _IsFirstTime = isFirstTime;
            _CountRow = CountRow;   
   
        }
        public FrmScheduleTest(int ApplicationID, enTest test, int AppointmentID, bool isFirstTime, bool IsLocked)
        {
            InitializeComponent();
           
            _LDLApplicationID = ApplicationID;
            _Test = test;
            _TestAppointmentID = AppointmentID;
            _IsFirstTime = isFirstTime;
            _Mode = enMode.Update;
            _IsLocked = IsLocked;

        }

        private DateTime GetLastAppointmentDate()
        {
            DataTable _dtAppointments = clsTestAppointment.GetAllAppointmentsByTestTypeID((int)_Test,
                _LDLApplication.Application.ApplicantPersonID, _LDLApplication.LicenseClassID);
            if (_dtAppointments.Rows.Count == 0)
                return DateTime.Today;

            DateTime latestDate = DateTime.MinValue;

            foreach (DataRow row in _dtAppointments.Rows)
            {
                DateTime currentDate = Convert.ToDateTime(row["Appointment Date"]);
                if (currentDate > latestDate)
                    latestDate = currentDate;
            }

            return latestDate;
        }

        private DateTime GetLastLockedAppointmentDateExcludingCurrent()
        {
            DataTable _dtAppointments = clsTestAppointment.GetAllAppointmentsByTestTypeID(
                (int)_Test,
                _LDLApplication.Application.ApplicantPersonID,
                _LDLApplication.LicenseClassID);

            if (_dtAppointments.Rows.Count == 0)
                return DateTime.Today.AddDays(-1);

            DateTime latestDate = DateTime.MinValue;

            foreach (DataRow row in _dtAppointments.Rows)
            {
                bool isLocked = Convert.ToBoolean(row["Is Locked"]);
                int appointmentID = Convert.ToInt32(row["Appointment ID"]);

                if (!isLocked || appointmentID == _TestAppointmentID)
                    continue;

                DateTime currentDate = Convert.ToDateTime(row["Appointment Date"]);
                if (currentDate > latestDate)
                    latestDate = currentDate;
            }

            return latestDate;
        }


        private void enableGroupBoxControls(int Number)
        {
            if (Number == 0 )
            {
                groupBox1.Enabled = false;
                label8.Enabled = false;
                pictureBox7.Enabled = false;
                lbRetakeAppFees.Enabled = false;
                label10.Enabled = false;
                pictureBox9.Enabled = false;
                pictureBox10.Enabled = false;
                label9.Enabled = false;
                lbTotalFees.Enabled=false;
                lbRetakeAppID.Enabled = false;
            }
            else if (Number == 1 )
            {
                groupBox1.Enabled = true;
                label8.Enabled = true;
                pictureBox7.Enabled = true;
                lbRetakeAppFees.Enabled = true;
                label10.Enabled = true;
                pictureBox9.Enabled = true;
                pictureBox10.Enabled = true;
                label9.Enabled = true;
                lbTotalFees.Enabled = true;
                lbRetakeAppID.Enabled = true;
            }
        }

        private void _LoadData()
        {
            if (_IsLocked)
            {

                dateTimePicker1.Enabled = false;
                label11.Visible = true;
                btSave.Enabled = false;
                return;
            }
            dateTimePicker1.Enabled = true;
            label11.Visible = false;
            btSave.Enabled = true;

            
            _LDLApplication = clsLDLApplication.FindLDLApplicationByID(_LDLApplicationID);

            if (_LDLApplication == null)
            {
                MessageBox.Show("LDL Application not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            lbDLAppID.Text = _LDLApplication.LDLApplicationID.ToString();
            lbClassName.Text = _LDLApplication.LicenseClass.ClassName;

            _Person = clsPerson.FindPersonByID(_LDLApplication.Application.ApplicantPersonID);
            lbPersonFullName.Text = _Person.FirstName + " " + _Person.SecondName + " " + _Person.ThirdName + " " + _Person.LastName;

            //int PassedTests = clsLDLApplication.GetPassedTestsCount(_LDLApplicationID);
            lbTrial.Text = _CountRow.ToString();

            clsTestTypes clsTestTypes = clsTestTypes.FindTestTypeByID((int)_Test);
            lbFees.Text = clsTestTypes.TestTypeFees.ToString();
            


            clsApplicationTypes ApplicationType = clsApplicationTypes.FindApplicationTypeByID(7);
            if (_Mode == enMode.AddNew)
            {
                _TestAppointment = new clsTestAppointment();
                dateTimePicker1.MinDate = DateTime.Today;
                dateTimePicker1.Value = DateTime.Today;

                if (_IsFirstTime)
                {
                    dateTimePicker1.MinDate = DateTime.Today;
                    dateTimePicker1.Value = DateTime.Today;
                    enableGroupBoxControls(0);
                    lbRetakeAppID.Text = "N/A";
                }
                else
                {
                    DateTime lastDate = GetLastAppointmentDate();
                    dateTimePicker1.MinDate = lastDate.AddDays(1);
                    dateTimePicker1.Value = dateTimePicker1.MinDate;

                    enableGroupBoxControls(1);


                    lbRetakeAppFees.Text = ApplicationType.ApplicationTypeFees.ToString();
                    lbRetakeAppID.Text = "N/A";

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
            DateTime lastLockedDate = GetLastLockedAppointmentDateExcludingCurrent();
            dateTimePicker1.MinDate = lastLockedDate.AddDays(1);
            if (_IsFirstTime)
            {
                dateTimePicker1.MinDate = DateTime.Today;
                enableGroupBoxControls(0);
                lbRetakeAppID.Text = "N/A";
            }
            else
            {
                //DateTime lastDate = GetLastAppointmentDate();
                //dateTimePicker1.MinDate = lastDate;
                enableGroupBoxControls(1);
                lbRetakeAppFees.Text = ApplicationType.ApplicationTypeFees.ToString();
     
                _Application = clsApplication.FindApplicationByID(_TestAppointment.RetakeTestApplicationID);
                lbRetakeAppID.Text = _Application.ApplicationID.ToString();

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
