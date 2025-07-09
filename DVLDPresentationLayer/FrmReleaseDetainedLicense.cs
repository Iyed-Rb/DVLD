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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DVLDPresentationLayer
{
    public partial class FrmReleaseDetainedLicense : Form
    {
        public FrmReleaseDetainedLicense()
        {
            InitializeComponent();
        }

        public FrmReleaseDetainedLicense(int LicenseID)
        {
            InitializeComponent();
            _LicenseID = LicenseID;

            ctrlLicenseWithFilter1.LoadLicenseInfo(_LicenseID);
            ctrlLicenseWithFilter1.FilterEnabled = false;
        }

        private int _LicenseID = -1;
        private void FrmReleaseDetainedLicense_Load(object sender, EventArgs e)
        {
            llShowNewLicenseInfo.Enabled = false;
            btRelease.Enabled = false;
            llShowLicenseHistory.Enabled = false;

            ctrlLicenseWithFilter1.OnLicenseSelected += ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected;

            lbDetainDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");

            clsApplicationTypes ApplicationTypes = clsApplicationTypes.FindApplicationTypeByID(5);

            lbCreatedbyUserID.Text = clsGlobalSettings.CurrentUser.UserID.ToString();

            
        }
        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            int SelectedLicenseID = obj;

            lbLicenseID.Text = SelectedLicenseID.ToString();

            llShowLicenseHistory.Enabled = (SelectedLicenseID != -1);

            if (SelectedLicenseID == -1)
            {
                llShowNewLicenseInfo.Enabled = false;
                
                return;
            }
            _LicenseID = SelectedLicenseID;
            clsDetainedLicense detainedLicense = clsDetainedLicense.FindByLicenseID(_LicenseID);

            if (!clsDetainedLicense.IsLicenseDetained(_LicenseID))
            {
                MessageBox.Show("Selected License is not Detained\nChoose Another One", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                llShowNewLicenseInfo.Enabled = false;
                btRelease.Enabled = false;
                return;
            }

    
            lbFineFees.Text = detainedLicense.FineFees.ToString();

            if (ctrlLicenseWithFilter1.SelectedLicenseInfo.IsActive == false)
            {
                MessageBox.Show("This is the Old License and it is Disactivated\nThis is not the Current License ", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                llShowNewLicenseInfo.Enabled = false;
                btRelease.Enabled = false;
                return;
            }

            btRelease.Enabled = true;
            llShowNewLicenseInfo.Enabled = false;

        }

        private void btRelease_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
                return;

            if (MessageBox.Show("Are you sure you want to Release the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            clsApplication Application = new clsApplication();

            Application.ApplicantPersonID = ctrlLicenseWithFilter1.SelectedLicenseInfo.Driver.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = 5; 
            Application.ApplicationStatus = 3; 
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = clsApplicationTypes.FindApplicationTypeByID(5).ApplicationTypeFees;
            Application.CreatedByUserID = clsGlobalSettings.CurrentUser.UserID;

            if (!Application.Save())
            {
                MessageBox.Show("Failed to Save Application for Release this License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clsDetainedLicense detainedLicense = clsDetainedLicense.FindByLicenseID(_LicenseID);
            
            if (!detainedLicense.ReleaseDetainedLicense(clsGlobalSettings.CurrentUser.UserID, Application.ApplicationID))
            {
                MessageBox.Show("Failed to Release the Detained License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            lbDetainID.Text = detainedLicense.DetainID.ToString();
            lbApplicationID.Text = Application.ApplicationID.ToString();
            MessageBox.Show("License Detained Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);   

            llShowNewLicenseInfo.Enabled = true;
            btRelease.Enabled = false;
            ctrlLicenseWithFilter1.FilterEnabled = false;

        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clsPerson person = clsPerson.FindPersonByID(ctrlLicenseWithFilter1.SelectedLicenseInfo.Driver.PersonID);

            frmLicenseHistory frmLicense = new frmLicenseHistory(person.NationalNo);
            frmLicense.ShowDialog();
        }

        private void llShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseInfo frmLicenseInfo = new frmLicenseInfo(_LicenseID);
            frmLicenseInfo.ShowDialog();
        }
    }
}
