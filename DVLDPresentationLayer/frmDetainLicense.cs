using DVLDBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDPresentationLayer
{
    public partial class frmDetainLicense : Form
    {
        public frmDetainLicense()
        {
            InitializeComponent();
        }

        private int _LicenseID = -1;
        private void frmDetainLicense_Load(object sender, EventArgs e)
        {
            llShowNewLicenseInfo.Enabled = false;
            btDetain.Enabled = false;
            llShowLicenseHistory.Enabled = false;

            ctrlLicenseWithFilter1.OnLicenseSelected += ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected;

            lbDetainDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");

            clsApplicationTypes ApplicationTypes = clsApplicationTypes.FindApplicationTypeByID(4);
            

            lbCreatedBy.Text = clsGlobalSettings.CurrentUser.UserID.ToString();
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            int SelectedLicenseID = obj;

            lbLicenseID.Text = SelectedLicenseID.ToString();

            llShowLicenseHistory.Enabled = (SelectedLicenseID != -1);

            //MessageBox.Show("SelectedLicenseID == " + SelectedLicenseID, "info", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (SelectedLicenseID == -1)
            {
                llShowNewLicenseInfo.Enabled = false;
                //MessageBox.Show("SelectedLicenseID == -1 , return", "info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _LicenseID = SelectedLicenseID;

        
            if (clsDetainedLicense.IsLicenseDetained(_LicenseID))
            {
                MessageBox.Show("Selected License is already Detained\nChoose Another One", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                llShowNewLicenseInfo.Enabled = false;
                btDetain.Enabled = false;
                return;
            }

            if (ctrlLicenseWithFilter1.SelectedLicenseInfo.IsActive == false)
            {
                MessageBox.Show("This is the Old License and it is Disactivated\nThis is not the Current License ", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                llShowNewLicenseInfo.Enabled = false;
                btDetain.Enabled = false;
                return;
            }

            btDetain.Enabled = true;
            llShowNewLicenseInfo.Enabled = false;

        }

        private void btIssue_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
                return;

            if (MessageBox.Show("Are you sure you want to Detain the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            clsDetainedLicense detainedLicense = new clsDetainedLicense();
            detainedLicense.LicenseID = _LicenseID;
            detainedLicense.DetainDate = DateTime.Now;
            detainedLicense.FineFees = Convert.ToInt32(textBox1.Text);
            detainedLicense.CreatedByUserID = clsGlobalSettings.CurrentUser.UserID;

            if (!detainedLicense.Save())
            {
                MessageBox.Show("Error Detaining License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("License Detained Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //clsLicense License = clsLicense.FindLicenseByID(_License);
            //OldLicense.IsActive = false; // Deactivate the old license
            //OldLicense.Save();

            lbDetainID.Text = detainedLicense.DetainID.ToString();

            llShowNewLicenseInfo.Enabled = true;
            btDetain.Enabled = false;
            ctrlLicenseWithFilter1.FilterEnabled = false;

        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                e.Cancel = true; // Prevents focus from leaving the textbox
                errorProvider1.SetError(textBox1, "Fees are required.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(textBox1, ""); // Clear the error
            }
        }

        private void llShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseInfo frmLicenseInfo = new frmLicenseInfo(_LicenseID);
            frmLicenseInfo.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clsPerson person = clsPerson.FindPersonByID(ctrlLicenseWithFilter1.SelectedLicenseInfo.Driver.PersonID);

            frmLicenseHistory frmLicense = new frmLicenseHistory(person.NationalNo);
            frmLicense.ShowDialog();
        }
    }

}
