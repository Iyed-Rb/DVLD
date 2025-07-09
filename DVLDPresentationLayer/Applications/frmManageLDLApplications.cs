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
using static DVLDPresentationLayer.frmTestAppointment;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DVLDPresentationLayer
{
    public partial class frmManageLDLApplications : Form
    {
        public frmManageLDLApplications()
        {
            InitializeComponent();
        }
        DataTable _dtLDLApplications;

        

        //int _PassedTests;
        //int _LDLApplicationID;
        private void _RefreshApplicationList()
        {
            _dtLDLApplications = clsLDLApplication.GetAllLDLApplications();
            dgvAllLDLApplications.DataSource = _dtLDLApplications;

            int rowCount = dgvAllLDLApplications.Rows.Count;
            lbCountRow.Text = rowCount.ToString();

            if (dgvAllLDLApplications.Columns.Contains("L.D.LAppID"))
                dgvAllLDLApplications.Columns["L.D.LAppID"].Width = 90;

            if (dgvAllLDLApplications.Columns.Contains("Driving Class"))
                dgvAllLDLApplications.Columns["Driving Class"].Width = 190;

            if (dgvAllLDLApplications.Columns.Contains("NationalNo"))
                dgvAllLDLApplications.Columns["NationalNo"].Width = 90;

            if (dgvAllLDLApplications.Columns.Contains("Full Name"))
                dgvAllLDLApplications.Columns["Full Name"].Width = 230;

            if (dgvAllLDLApplications.Columns.Contains("ApplicationDate"))
                dgvAllLDLApplications.Columns["ApplicationDate"].Width = 110;

            if (dgvAllLDLApplications.Columns.Contains("Passed Tests"))
                dgvAllLDLApplications.Columns["Passed Tests"].Width = 100;

            if (dgvAllLDLApplications.Columns.Contains("Status"))
                dgvAllLDLApplications.Columns["Status"].Width = 90;




            if (dgvAllLDLApplications.Rows.Count > 0)
            {
                dgvAllLDLApplications.Rows[0].Selected = true;
                dgvAllLDLApplications.CurrentCell = dgvAllLDLApplications.Rows[0].Cells[0];
            }

        }

        //private void Show()
        //{
        //    if (dgvAllLDLApplications.CurrentRow == null)
        //        return;

        //    int PassedTests = (int)dgvAllLDLApplications.CurrentRow.Cells[5].Value;
        //    int LDLApplicationID = (int)dgvAllLDLApplications.CurrentRow.Cells[0].Value;
        //    //int passedTests = clsLDLApplication.GetPassedTestsCount(_LDLApplicationID);
        //    //MessageBox.Show("PassedTests = " + PassedTests);

        //    clsLDLApplication lDLApplication = clsLDLApplication.FindLDLApplicationByID(LDLApplicationID);
        //    clsApplication application = clsApplication.FindApplicationByID(lDLApplication.ApplicationID);

        //    if (application.ApplicationStatus == 2) // Canceled
        //    {
        //        MessageBox.Show("This Application Is Cancelled ","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        //        scheduleTestToolStripMenuItem.Enabled = false;
        //        scheduleVisionTestToolStripMenuItem.Enabled = false;
        //        scheduleWrittenTestToolStripMenuItem.Enabled = false;
        //        scheduleStreetTestToolStripMenuItem.Enabled = false;

        //        issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;

        //        deleteToolStripMenuItem.Enabled = false;
        //        cancelApplicationToolStripMenuItem.Enabled = false;
        //        editToolStripMenuItem.Enabled = false;

        //        showLicenseToolStripMenuItem.Enabled = false;

        //        return;
        //    }

        //        switch (PassedTests)
        //    {
        //        case 0:
        //            scheduleTestToolStripMenuItem.Enabled = true;
        //            scheduleVisionTestToolStripMenuItem.Enabled = true;
        //            scheduleWrittenTestToolStripMenuItem.Enabled = false;
        //            scheduleStreetTestToolStripMenuItem.Enabled = false;

        //            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;

        //            deleteToolStripMenuItem.Enabled = true;
        //            cancelApplicationToolStripMenuItem.Enabled = true;
        //            editToolStripMenuItem.Enabled = true;

        //            break;
        //        case 1:
        //            scheduleTestToolStripMenuItem.Enabled = true;
        //            scheduleVisionTestToolStripMenuItem.Enabled = false;
        //            scheduleWrittenTestToolStripMenuItem.Enabled = true;
        //            scheduleStreetTestToolStripMenuItem.Enabled = false;

        //            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;

        //            deleteToolStripMenuItem.Enabled = true;
        //            cancelApplicationToolStripMenuItem.Enabled = true;
        //            editToolStripMenuItem.Enabled = true;

        //            break;
        //        case 2:
        //            scheduleTestToolStripMenuItem.Enabled = true;
        //            scheduleVisionTestToolStripMenuItem.Enabled = false;
        //            scheduleWrittenTestToolStripMenuItem.Enabled = false;
        //            scheduleStreetTestToolStripMenuItem.Enabled = true;

        //            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;

        //            deleteToolStripMenuItem.Enabled = true;
        //            cancelApplicationToolStripMenuItem.Enabled = true;
        //            editToolStripMenuItem.Enabled = true;
        //            break;
        //        case 3:
        //            scheduleTestToolStripMenuItem.Enabled = false;
        //            scheduleVisionTestToolStripMenuItem.Enabled = false;
        //            scheduleWrittenTestToolStripMenuItem.Enabled = false;
        //            scheduleStreetTestToolStripMenuItem.Enabled = false;

        //            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = true;

        //            deleteToolStripMenuItem.Enabled = false;
        //            cancelApplicationToolStripMenuItem.Enabled = false;
        //            editToolStripMenuItem.Enabled = false;


        //            break;
        //        default:
        //            MessageBox.Show("Default case: Invalid PassedTests = " + PassedTests);
        //            scheduleVisionTestToolStripMenuItem.Enabled = false;
        //            scheduleWrittenTestToolStripMenuItem.Enabled = false;
        //            scheduleStreetTestToolStripMenuItem.Enabled = false;
        //            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
        //            deleteToolStripMenuItem.Enabled = false;

        //            break;
        //    }

        //    //clsPerson Person = clsPerson.FindPersonByNationalNO((string)dgvAllLDLApplications.CurrentRow.Cells[2].Value);


        //    clsLicense license = clsLicense.FindLicenseByApplicationID(lDLApplication.ApplicationID);
        //    if (license != null)
        //    {
        //        showLicenseToolStripMenuItem.Enabled = true;
        //        issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
        //    }
        //    else
        //    {
        //        showLicenseToolStripMenuItem.Enabled = false;
        //        //issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
        //    }
        //}

        private void DisableAllMenuItems()
        {
            // Disable everything related to the application
            scheduleTestToolStripMenuItem.Enabled = false;
            scheduleVisionTestToolStripMenuItem.Enabled = false;
            scheduleWrittenTestToolStripMenuItem.Enabled = false;
            scheduleStreetTestToolStripMenuItem.Enabled = false;

            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
            deleteToolStripMenuItem.Enabled = false;
            cancelApplicationToolStripMenuItem.Enabled = false;
            editToolStripMenuItem.Enabled = false;
            showLicenseToolStripMenuItem.Enabled = false;
        }

        private void EnableMenuItemsBasedOnPassedTests(int passedTests)
        {
            // First reset everything
            DisableAllMenuItems();

            switch (passedTests)
            {
                case 0:
                    scheduleTestToolStripMenuItem.Enabled = true;
                    scheduleVisionTestToolStripMenuItem.Enabled = true;
                    deleteToolStripMenuItem.Enabled = true;
                    cancelApplicationToolStripMenuItem.Enabled = true;
                    editToolStripMenuItem.Enabled = true;
                    break;

                case 1:
                    scheduleTestToolStripMenuItem.Enabled = true;
                    scheduleWrittenTestToolStripMenuItem.Enabled = true;
                    deleteToolStripMenuItem.Enabled = true;
                    cancelApplicationToolStripMenuItem.Enabled = true;
                    editToolStripMenuItem.Enabled = true;
                    break;

                case 2:
                    scheduleTestToolStripMenuItem.Enabled = true;
                    scheduleStreetTestToolStripMenuItem.Enabled = true;
                    deleteToolStripMenuItem.Enabled = true;
                    cancelApplicationToolStripMenuItem.Enabled = true;
                    editToolStripMenuItem.Enabled = true;
                    break;

                case 3:
                    issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = true;
                    break;

                default:
                    MessageBox.Show("Invalid PassedTests = " + passedTests, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }
        }

        private void Show()
        {
            if (dgvAllLDLApplications.CurrentRow == null)
                return;

            int passedTests = (int)dgvAllLDLApplications.CurrentRow.Cells[5].Value;
            int ldlApplicationID = (int)dgvAllLDLApplications.CurrentRow.Cells[0].Value;

            clsLDLApplication lDLApplication = clsLDLApplication.FindLDLApplicationByID(ldlApplicationID);
            clsApplication application = clsApplication.FindApplicationByID(lDLApplication.ApplicationID);

            if (application.ApplicationStatus == 2) // Cancelled
            {
                //MessageBox.Show("This Application Is Cancelled", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DisableAllMenuItems();
                return;
            }

            EnableMenuItemsBasedOnPassedTests(passedTests);

            clsLicense license = clsLicense.FindLicenseByApplicationID(lDLApplication.ApplicationID);
            showLicenseToolStripMenuItem.Enabled = (license != null);
            if (license != null)
                issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
        }


        private void frmManageLDLApplications_Load(object sender, EventArgs e)
        {

            _RefreshApplicationList();

            if (dgvAllLDLApplications.CurrentRow == null)
            {
                MessageBox.Show("No row is selected in the DataGridView.");
                return;
            }
            //MessageBox.Show(dgvAllLDLApplications.CurrentCell?.Value?.ToString() ?? "NULL");


            dgvAllLDLApplications.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            comboBox1.SelectedIndex = 0;
            textBox1.Visible = false;

            dgvAllLDLApplications.AllowUserToAddRows = false;
            dgvAllLDLApplications.ReadOnly = true;

            int LDLApplicationID = Convert.ToInt32(dgvAllLDLApplications.CurrentRow.Cells[0].Value);
            int PassedTest = clsLDLApplication.GetPassedTestsCount(LDLApplicationID);
            //MessageBox.Show("PassedTest count: " + PassedTest);

        }
        private void btLDLApplication_Click(object sender, EventArgs e)
        {
            //int LDLApplicationID = (int)dgvAllLDLApplications.CurrentRow.Cells[0].Value;
            //clsLDLApplication LDLApplication = clsLDLApplication.FindLDLApplicationByID(LDLApplicationID);

            //string NationalNo = (string)dgvAllLDLApplications.CurrentRow.Cells[1].Value;
            //clsPerson Person = clsPerson.FindPersonByNationalNO(NationalNo);


            AddEditLDLApplications frmAddEditLDLApplications = new AddEditLDLApplications();
            frmAddEditLDLApplications.ShowDialog();
            _RefreshApplicationList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LDLApplicationID = (int)dgvAllLDLApplications.CurrentRow.Cells[0].Value;
            clsLDLApplication LDLApplication = clsLDLApplication.FindLDLApplicationByID(LDLApplicationID);

            AddEditLDLApplications frmAddEditLDLApplications = new AddEditLDLApplications((int)dgvAllLDLApplications.CurrentRow.Cells[0].Value);
            frmAddEditLDLApplications.ShowDialog();
            _RefreshApplicationList();
        }

        private void btCLose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != 0)
            {
                textBox1.Visible = true;

            }
            else
            {
                textBox1.Visible = false;
                textBox1.Text = "";

                DataView dv = _dtLDLApplications.DefaultView;
                dv.RowFilter = ""; // Clear filter
                dgvAllLDLApplications.DataSource = dv;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (_dtLDLApplications == null || comboBox1.SelectedIndex == 0)
                return;

            string SelectedColumn = "";

            switch (comboBox1.SelectedIndex)
            {
                case 1: SelectedColumn = "L.D.LAppID"; break;
                case 2: SelectedColumn = "NationalNO"; break;
                case 3: SelectedColumn = "Full Name"; break;
                case 4: SelectedColumn = "Status"; break;
                default: return;
            }

            string keyword = textBox1.Text.Replace("'", "''"); // Escape single quotes

            try
            {
                DataView dv = _dtLDLApplications.DefaultView;

                if (SelectedColumn == "L.D.LAppID")
                {
                    if (int.TryParse(textBox1.Text, out int id))
                        dv.RowFilter = $"[{SelectedColumn}] = {id}";
                    else
                        dv.RowFilter = "1 = 0"; // Invalid input, show no results
                }
                else if (SelectedColumn == "Full Name" || SelectedColumn == "NationalNO" || SelectedColumn == "Status")
                {
                    dv.RowFilter = $"[{SelectedColumn}] LIKE '%{keyword}%'";
                    //dv.RowFilter = $"[{SelectedColumn}] = '{keyword}'";
                }

                dgvAllLDLApplications.DataSource = dv;
            }
            catch (Exception ex)
            {
                // You can log the error or show it
                // MessageBox.Show("Filter error: " + ex.Message);
            }
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LDLApplicationID = (int)dgvAllLDLApplications.CurrentRow.Cells[0].Value;
        

            frmLDLApplicationInfo frmLDLApplicationInfo = new frmLDLApplicationInfo(LDLApplicationID);
            frmLDLApplicationInfo.ShowDialog();

        }

        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsLDLApplication LDLApplication;
            LDLApplication = clsLDLApplication.FindLDLApplicationByID((int)dgvAllLDLApplications.CurrentRow.Cells[0].Value);

  

            if (LDLApplication == null)
            {
                MessageBox.Show("L.D.L Application not Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            
            DialogResult result = MessageBox.Show(
                "Are you sure you want to cancel this application?",
                "Confirm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            if (clsApplication.CancelApplication(LDLApplication.ApplicationID))
            {
                MessageBox.Show("L.D.L Application Cancelled Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _RefreshApplicationList();
            }
            else
            {
                MessageBox.Show("Error Cancelling L.D.L Application", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (dgvAllLDLApplications.CurrentRow == null)
            {
                MessageBox.Show("Please select a valid application to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int LDLApplicationID = (int)dgvAllLDLApplications.CurrentRow.Cells[0].Value;
            clsLDLApplication LDLApplication = clsLDLApplication.FindLDLApplicationByID(LDLApplicationID);

        

            if (LDLApplication == null)
            {
                MessageBox.Show("This application does not exist in the system.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Are you sure you want to delete Application ID [{LDLApplication.ApplicationID}]?",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                if (clsLDLApplication.DeleteLDLApplication(LDLApplicationID))
                {
                    MessageBox.Show("Application deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshApplicationList();
                }
                else
                {
                    MessageBox.Show("Deletion failed. This application may have linked data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int PassedTests = (int)dgvAllLDLApplications.CurrentRow.Cells[5].Value;
            int LDLApplicationID = (int)dgvAllLDLApplications.CurrentRow.Cells[0].Value;

           
            frmTestAppointment frmTestAppointment = new frmTestAppointment(LDLApplicationID, PassedTests, enTest.Vision);
            frmTestAppointment.ShowDialog();
            _RefreshApplicationList();
        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PassedTests = (int)dgvAllLDLApplications.CurrentRow.Cells[5].Value;
            int LDLApplicationID = (int)dgvAllLDLApplications.CurrentRow.Cells[0].Value;

            
            frmTestAppointment frmTestAppointment = new frmTestAppointment(LDLApplicationID, PassedTests, enTest.Written);
            frmTestAppointment.ShowDialog();
            _RefreshApplicationList();
        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PassedTests = (int)dgvAllLDLApplications.CurrentRow.Cells[5].Value;
            int LDLApplicationID = (int)dgvAllLDLApplications.CurrentRow.Cells[0].Value;
            

            frmTestAppointment frmTestAppointment = new frmTestAppointment(LDLApplicationID, PassedTests, enTest.Street);
            frmTestAppointment.ShowDialog();
            _RefreshApplicationList();
        }

        private void dgvAllLDLApplications_SelectionChanged(object sender, EventArgs e)
        {
            Show();
        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLicenseFirstTime frmLicenseFirstTime = new frmLicenseFirstTime((int)dgvAllLDLApplications.CurrentRow.Cells[0].Value);
            
            frmLicenseFirstTime.ShowDialog();
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsLDLApplication lDLApplication = clsLDLApplication.FindLDLApplicationByID((int)dgvAllLDLApplications.CurrentRow.Cells[0].Value);
            clsLicense license = clsLicense.FindLicenseByApplicationID(lDLApplication.ApplicationID);
            frmLicenseInfo frmLicenseInfo = new frmLicenseInfo(license.LicenseID);
 
            frmLicenseInfo.ShowDialog();
            _RefreshApplicationList();
        }

        private void showLicenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmLicenseHistory licenseHistory = new frmLicenseHistory((string)dgvAllLDLApplications.CurrentRow.Cells[2].Value);
            licenseHistory.ShowDialog();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }
    }
}
