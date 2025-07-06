using DVLDBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace DVLDPresentationLayer
{
    public partial class frmTakeTest : Form
    {
        int _LDLApplicationID;
        clsLDLApplication _LDLApplication;

        int _PersonID;
        clsPerson _Person;

        int _TestAppointmentID;
        clsTestAppointment _TestAppointment;

        clsTest _Test;
        int _CountRow;
        public frmTakeTest(int LDLApplicationID, int TestAppointmentID, enTest test, int countRow)
        {
            InitializeComponent();
            _LDLApplicationID = LDLApplicationID;
            _TestAppointmentID = TestAppointmentID;
            eTest = test;
            _CountRow = countRow;
        }

        public enTest eTest;

        private void _LoadData()
       {

            rbPass.Checked = true;
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

            //int PassedTests = clsLDLApplication.GetPassedTestsCount(_LDLApplicationID);
            lbTrial.Text = _CountRow.ToString();

            lbDate.Text = DateTime.Now.ToString();

            clsTestTypes clsTestTypes = clsTestTypes.FindTestTypeByID((int)eTest);
            if (clsTestTypes == null)
            {
                MessageBox.Show("Error: clsTestTypes is null. eTest = " + ((int)eTest).ToString());
                return;
            }
            lbFees.Text = clsTestTypes.TestTypeFees.ToString();

            lbTestID.Text = "Not Taken";

        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void btCLose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            _Test = new clsTest();

            //_Test.TestAppointment.TestAppointmentID = _TestAppointmentID;
            _Test.TestAppointment = clsTestAppointment.FindTestAppointmentByID(_TestAppointmentID);
            if(_Test.TestAppointment == null)
            {
                MessageBox.Show("TestAppointment not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            _TestAppointment = _Test.TestAppointment;
            _Test.TotalResult = rbPass.Checked;
            _Test.Notes = textBox1.Text;
            _Test.CreatedByUserID = clsGlobalSettings.CurrentUser.UserID;

            DialogResult result = MessageBox.Show(
                @"Are you sure you want to save? After that, you cannot change the Pass/Fail results.",
                "Confirm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                btSave.Enabled = false;

                if (!_Test.Save())
                {
                    MessageBox.Show("Error: Test data is not saved successfully.");
                    return;
                }


                _TestAppointment.IsLocked = true;

                if (!_TestAppointment.Save())
                {
                    MessageBox.Show("Error: Appointment could not be locked.");
                    return;
                }

                clsApplication application = clsApplication.FindApplicationByID(_TestAppointment.RetakeTestApplicationID);
                if (application != null)
                {
                    application.ApplicationStatus = 3;
                    if (!application.Save())
                    {
                        MessageBox.Show("Error: Retake application could not be updated.");
                        return;
                    }
                }

                lbTestID.Text = _Test.TestID.ToString();
                MessageBox.Show("Test recorded and appointment locked successfully.");
                this.Close();
            }
        }



    }
}

