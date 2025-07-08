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
    public partial class frmLicenseFirstTime : Form
    {
        int _LDLApplicationID;
        clsLDLApplication _LDLApplication;
        public frmLicenseFirstTime(int LDLApplication)
        {
            InitializeComponent();
            _LDLApplicationID = LDLApplication;
        }

        private void frmLicenseFirstTime_Load(object sender, EventArgs e)
        {
            _LDLApplication = clsLDLApplication.FindLDLApplicationByID(_LDLApplicationID);
            if (_LDLApplication == null)
            {
                MessageBox.Show("LDLApplication Not Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            ctrlApplicationInfo1.FillCtrlInformation(_LDLApplicationID);
        }

        private void btCLose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btIssue_Click(object sender, EventArgs e)
        {
            clsLicense License = new clsLicense();
            License.ApplicationID = _LDLApplication.ApplicationID;

            clsApplication Application = clsApplication.FindApplicationByID(_LDLApplication.ApplicationID);
            if (Application == null)
            {
                MessageBox.Show("Application Not Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            if (!clsDriver.IsDriverExistByPersonID(_LDLApplication.Application.ApplicantPersonID))
            {
                //MessageBox.Show("Driver doesnt exixst before", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                clsDriver Driver = new clsDriver();
                Driver.PersonID = _LDLApplication.Application.ApplicantPersonID;
                Driver.CreatedDate = DateTime.Now;
                Driver.CreatedByUserID = clsGlobalSettings.CurrentUser.UserID;
                License.Driver = Driver;
            }
            else
            {
                //MessageBox.Show("Driver exixst before", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsDriver Driver = clsDriver.FindDriverByPersonID(_LDLApplication.Application.ApplicantPersonID);

                License.Driver = Driver;

            }

            License.LicenseClassID = _LDLApplication.LicenseClassID;
            License.IssueDate = DateTime.Now;
            License.ExpirationDate = DateTime.Now.AddYears(_LDLApplication.LicenseClass.DefaultValidityLength);
            License.Notes = textBox1.Text;
            License.PaidFees = _LDLApplication.LicenseClass.ClassFees;
            License.IsActive = true;
            License.IssueReason = 1;
            License.CreatedByUserID = clsGlobalSettings.CurrentUser.UserID;

            MessageBox.Show("License.Driver.PersonID = " + License.Driver?.PersonID);

            if (License.Save())
            {
                Application.ApplicationStatus = 3; 
                if (Application.Save()) 
                    {
                    MessageBox.Show("Data Updated Succefully", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to update application status.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                DialogResult D = MessageBox.Show("License Issued Succefully with License ID = " + License.LicenseID, "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();

            }
            else
            {
                MessageBox.Show("Data Not Saved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();

            }

        }
    }
}
