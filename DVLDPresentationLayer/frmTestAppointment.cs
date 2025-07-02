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
            ctrlApplicationInfo1.FillCtrlInformation(_LDLApplicationID);
            if (Test == enTest.Vision)
            {
                pictureBox1.Image = Properties.Resources.Vision_512;
                lbTest.Text = "Vision Test Appointments";
            }
            else if (Test == enTest.Written)
            {
                pictureBox1.Image = Properties.Resources.Written_Test_512;
                lbTest.Text = "Written Test Appointments";
            }
            else if (Test == enTest.Street)
            {
                pictureBox1.Image = Properties.Resources.driving_test_512;
                lbTest.Text = "Street Test Appointments";
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
            _dtAppointments = clsTestAppointment.GetAllAppointmentsByTestTypeID((int)enTest.Vision, PersonID);
            dgvAllAppointments.DataSource = _dtAppointments;
            int rowCount = dgvAllAppointments.Rows.Count;
            lbCountRow.Text = rowCount.ToString();
        }

        private void btCLose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btAddAppointment_Click(object sender, EventArgs e)
        {
    
                // Check if there's any unlocked appointment
                bool hasUnlockedAppointment = _dtAppointments.AsEnumerable().Any(row =>
                    row.Field<bool>("IsLocked") == false
                );

                if (hasUnlockedAppointment)
                {
                    MessageBox.Show("Person already has an active appointment for this test.\nYou cannot add a new appointment.",
                                    "Not Allowed",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    return;
                }

                // No active appointment found → allow scheduling
                FrmScheduleTest frmScheduleTest = new FrmScheduleTest(_LDLApplicationID, Test);
                frmScheduleTest.ShowDialog();
            

        }
    }
}
