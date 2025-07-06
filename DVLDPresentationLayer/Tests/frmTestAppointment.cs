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

namespace DVLDPresentationLayer
{
    public enum enTest { Vision = 1, Written = 2, Street = 3 };
   
    public partial class frmTestAppointment : Form
    {
        int _LDLApplicationID;
        clsLDLApplication _LDLApplication;
        int _PassedTests;

  
        public enTest Test;

        DataTable _dtAppointments;
        int _CountRow;
        public frmTestAppointment(int LDLApplicationID, int PassedTests, enTest test)
        {
            InitializeComponent();
            _LDLApplicationID = LDLApplicationID;
            _PassedTests = PassedTests;
            Test = test;

        }

        private void frmTestAppointment_Load(object sender, EventArgs e)
        {
            _RefreshAppointmentList();
            dgvAllAppointments.AllowUserToAddRows = false;
            dgvAllAppointments.ReadOnly = true;
            ctrlApplicationInfo1.FillCtrlInformation(_LDLApplicationID);
            if (Test == enTest.Vision)
            {
                pictureBox1.Image = Properties.Resources.Vision_512;
                lbTest.Text = "Vision Test Appointments";
                this.Text = "Vision Test Appointments";
            }
            else if (Test == enTest.Written)
            {
                pictureBox1.Image = Properties.Resources.Written_Test_512;
                lbTest.Text = "Written Test Appointments";
                this.Text = "Written Test Appointments";
            }
            else if (Test == enTest.Street)
            {
                pictureBox1.Image = Properties.Resources.driving_test_512;
                lbTest.Text = "Street Test Appointments";
                this.Text = "Street Test Appointments";
            }
         
        }

        private void _RefreshAppointmentList()
        {
            _LDLApplication = clsLDLApplication.FindLDLApplicationByID(_LDLApplicationID);
            if (_LDLApplication == null)
            {
                MessageBox.Show("LDL Application not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int PersonID = _LDLApplication.Application.ApplicantPersonID;
            _dtAppointments = clsTestAppointment.GetAllAppointmentsByTestTypeID((int)Test, PersonID, _LDLApplication.LicenseClassID);

           
            dgvAllAppointments.DataSource = _dtAppointments;
            if (dgvAllAppointments.Columns.Contains("Appointment Date"))
            {
                dgvAllAppointments.Columns["Appointment Date"].DefaultCellStyle.Format = "dd/MM/yyyy hh:mm tt";
            }
            _CountRow = dgvAllAppointments.Rows.Count;
            lbCountRow.Text = _CountRow.ToString();
            ctrlApplicationInfo1.FillCtrlInformation(_LDLApplicationID);

            if (dgvAllAppointments.Columns.Contains("Appointment ID"))
                dgvAllAppointments.Columns["Appointment ID"].Width = 130;

            if (dgvAllAppointments.Columns.Contains("Appointment Date"))
            {
                dgvAllAppointments.Columns["Appointment Date"].DefaultCellStyle.Format = "dd/MM/yyyy hh:mm tt";
                dgvAllAppointments.Columns["Appointment Date"].Width = 130;
            }

            if (dgvAllAppointments.Columns.Contains("Paid Fees"))
                dgvAllAppointments.Columns["Paid Fees"].Width = 110;

            if (dgvAllAppointments.Columns.Contains("Is Locked"))
                dgvAllAppointments.Columns["Is Locked"].Width = 80;
        }

        private void btCLose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btAddAppointment_Click(object sender, EventArgs e)
        {
            // Step 1: Check if there's any unlocked appointment
            bool hasUnlockedAppointment = _dtAppointments.AsEnumerable().Any(row =>
                row.Field<bool>("Is Locked") == false
            );

            if (hasUnlockedAppointment)
            {
                MessageBox.Show("Person already has an active appointment for this test.\nYou cannot add a new appointment.",
                                "Not Allowed",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            // Step 2: Check if this test has already been passed
            int passedTests = clsLDLApplication.GetPassedTestsCount(_LDLApplicationID);

            // We assume GetPassedTestsCount returns:
            // 1 → Vision passed
            // 2 → Vision + Written passed
            // 3 → All tests passed

            bool alreadyPassed = false;

            switch (Test)
            {
                case enTest.Vision:
                    alreadyPassed = passedTests >= 1;
                    break;
                case enTest.Written:
                    alreadyPassed = passedTests >= 2;
                    break;
                case enTest.Street:
                    alreadyPassed = passedTests == 3;
                    break;
            }

            if (alreadyPassed)
            {
                MessageBox.Show("This person already passed this test before. You can only retake failed tests.",
                                "Not Allowed",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            //clsTestAppointment testAppointment = clsTestAppointment.FindTestAppointmentByID((int)dgvAllAppointments.CurrentRow.Cells[0].Value);
            //bool IsLocked = testAppointment.IsLocked;   
            int testAttemptsCount = _dtAppointments.Rows.Count; // all appointments already filtered by this test
            bool isFirstTime = (testAttemptsCount == 0);
            // Step 3: All checks passed → allow scheduling
            FrmScheduleTest frmScheduleTest = new FrmScheduleTest(_LDLApplicationID, Test, isFirstTime, _CountRow);
            frmScheduleTest.ShowDialog();

            _RefreshAppointmentList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsTestAppointment testAppointment = clsTestAppointment.FindTestAppointmentByID( (int)dgvAllAppointments.CurrentRow.Cells[0].Value);
            bool IsLocked = testAppointment.IsLocked;
           
            int testAttemptsCount = _dtAppointments.Rows.Count; // all appointments already filtered by this test
            bool isFirstTime = (testAttemptsCount == 1);
            FrmScheduleTest frm = new FrmScheduleTest(_LDLApplicationID, Test, (int)dgvAllAppointments.CurrentRow.Cells[0].Value, isFirstTime, IsLocked);
            frm.ShowDialog();
            _RefreshAppointmentList();
        }

 

        private void TakeTestToolStripMenuItem_Click_1(object sender, EventArgs e)
        {


            clsTestAppointment testAppointment = clsTestAppointment.FindTestAppointmentByID((int)dgvAllAppointments.CurrentRow.Cells[0].Value);
            if (testAppointment.IsLocked == true)
            {
                MessageBox.Show("Test Already Taken", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int passedTests = clsLDLApplication.GetPassedTestsCount(_LDLApplicationID);
            if (passedTests == 0)
            {
                Test = enTest.Vision;
            }
            else if (passedTests == 1)
            {
                Test = enTest.Written;
            }
            else if ( passedTests == 2)
            {
                Test = enTest.Street;
            }
            frmTakeTest frmTakeTest = new frmTakeTest(_LDLApplicationID, (int)dgvAllAppointments.CurrentRow.Cells[0].Value, Test, _CountRow);
            frmTakeTest.ShowDialog();
            _RefreshAppointmentList();

        }
    }
}
