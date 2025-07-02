using DVLDBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
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

        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;


        public enTest _Test;

        public FrmScheduleTest(int ApplicationID, enTest test)
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
            _LDLApplicationID = ApplicationID;
            _Test = test;
            _Mode = enMode.AddNew;
        }
        public FrmScheduleTest(int ApplicationID, enTest test, int AppointmentID)
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
            _LDLApplicationID = ApplicationID;
            _Test = test;
            _TestAppointmentID = AppointmentID;
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
            clsPerson person = clsPerson.FindPersonByID(_LDLApplication.Application.ApplicantPersonID);
            lbPersonFullName.Text = person.FirstName + " " + person.SecondName + " " + person.ThirdName + " " + person.LastName;
            int PassedTests = clsLDLApplication.GetPassedTestsCount(_LDLApplicationID);
            lbCountTests.Text = PassedTests.ToString();
            clsTestTypes clsTestTypes = clsTestTypes.FindTestTypeByID((int)_Test);
            lbFees.Text = clsTestTypes.TestTypeFees.ToString();


            if (_Mode == enMode.AddNew)
            {
                _TestAppointment = new clsTestAppointment();
                dateTimePicker1.MinDate = DateTime.Today;
                dateTimePicker1.Value = DateTime.Today;
                return;

            }

            _TestAppointment = clsTestAppointment.FindTestAppointmentByID(_TestAppointmentID);
            dateTimePicker1.Value = _TestAppointment.AppointmentDate;

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
            
        }
    }
}
