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
    public partial class frmNewInternationalLicenseApplication : Form
    {
        public frmNewInternationalLicenseApplication()
        {
            InitializeComponent();
        }

        private int _InternationalLicenseID = -1;
        private void frmNewInternationalLicenseApplication_Load(object sender, EventArgs e)
        {
            llShowLicenseHistory.Enabled = false;
            llShowLicenseInfo.Enabled = false;
            btIssue.Enabled = false;

            ctrlLicenseWithFilter1.OnLicenseSelected += ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected;

            lbIssueDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lbExpirationDate.Text = DateTime.Now.AddYears(1).ToString("dd/MMM/yyyy");
            lbApplicationDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");

            clsApplicationTypes ApplicationTypes = clsApplicationTypes.FindApplicationTypeByID(6);
            lbFees.Text = ApplicationTypes.ApplicationTypeFees.ToString("C2");

            lbCreatedBy.Text = clsGlobalSettings.CurrentUser.UserID.ToString();
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            int SelectedLicenseID = obj;

            lbLocalLicenseID.Text = SelectedLicenseID.ToString();

            llShowLicenseHistory.Enabled = (SelectedLicenseID != -1);

            //MessageBox.Show("SelectedLicenseID == " + SelectedLicenseID, "info", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (SelectedLicenseID == -1)
            {
                llShowLicenseInfo.Enabled = false;
                //MessageBox.Show("SelectedLicenseID == -1 , return", "info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //check the license class, person could not issue international license without having
            //normal license of class 3.

            if (ctrlLicenseWithFilter1.SelectedLicenseInfo.LicenseClassID != 3)
            {
                MessageBox.Show("Selected License should be Class 3, select another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (ctrlLicenseWithFilter1.SelectedLicenseInfo.IsActive == false)
            {
                MessageBox.Show("Your Local License is not Active", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (ctrlLicenseWithFilter1.SelectedLicenseInfo.ExpirationDate < DateTime.Now)
            {
                MessageBox.Show("This License Is Expired", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (clsDetainedLicense.IsLicenseDetained(SelectedLicenseID))
            {
                MessageBox.Show("Selected License is Detained\nYou must Release it First", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                llShowLicenseInfo.Enabled = false;
                btIssue.Enabled = false;
                return;
            }

            //check if person already have an active international license
            int ActiveInternaionalLicenseID = clsInternationalLicense.GetActiveInternationalLicenseIDByDriverID(ctrlLicenseWithFilter1.SelectedLicenseInfo.DriverID);

            if (ActiveInternaionalLicenseID != -1)
            {
                //clsInternationalLicense internationalLicense = clsInternationalLicense.FindInternationalLicenseByID(ActiveInternaionalLicenseID);
                //clsApplication application = clsApplication.FindApplicationByID(internationalLicense.Application.ApplicationID);


                MessageBox.Show("Person already have an active international license\nwith ID = " + ActiveInternaionalLicenseID.ToString(), "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                llShowLicenseInfo.Enabled = true;
                _InternationalLicenseID = ActiveInternaionalLicenseID;
                btIssue.Enabled = false;
                return;
            }

            btIssue.Enabled = true;
            llShowLicenseInfo.Enabled = false;

        }

        private void ctrlLicenseWithFilter1_Load(object sender, EventArgs e)
        {

        }

        private void btCLose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btIssue_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to issue the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }


            clsInternationalLicense InternationalLicense = new clsInternationalLicense();

            InternationalLicense.Application.ApplicationID = ctrlLicenseWithFilter1.SelectedLicenseInfo.ApplicationID;
            InternationalLicense.Application.ApplicantPersonID = ctrlLicenseWithFilter1.SelectedLicenseInfo.Driver.PersonID;
            InternationalLicense.Application.ApplicationDate = DateTime.Now;
            InternationalLicense.Application.ApplicationStatus = 3;
            InternationalLicense.Application.ApplicationTypeID = 6; //International License Application Type
            InternationalLicense.Application.LastStatusDate = DateTime.Now;
            InternationalLicense.Application.PaidFees = clsApplicationTypes.FindApplicationTypeByID(6).ApplicationTypeFees;
            InternationalLicense.Application.CreatedByUserID = clsGlobalSettings.CurrentUser.UserID;

            InternationalLicense.DriverID = ctrlLicenseWithFilter1.SelectedLicenseInfo.DriverID;
            InternationalLicense.LocalLicenseID = ctrlLicenseWithFilter1.SelectedLicenseInfo.LicenseID;
            InternationalLicense.IssueDate = DateTime.Now;
            InternationalLicense.ExpirationDate = DateTime.Now.AddYears(1);
            InternationalLicense.IsActive = true;
            InternationalLicense.CreatedByUserID = clsGlobalSettings.CurrentUser.UserID;

            if (!InternationalLicense.Save())
            {
                MessageBox.Show("Faild to Issue International License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            lbApplicationID.Text = InternationalLicense.Application.ApplicationID.ToString();
            _InternationalLicenseID = InternationalLicense.InternationalLicenseID;
            lbInternationalLicenseID.Text = InternationalLicense.InternationalLicenseID.ToString();
            MessageBox.Show("International License Issued Successfully with ID=" + InternationalLicense.InternationalLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

          
            llShowLicenseInfo.Enabled = true;
            btIssue.Enabled = false;
            ctrlLicenseWithFilter1.FilterEnabled = false;
           
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clsPerson person = clsPerson.FindPersonByID(ctrlLicenseWithFilter1.SelectedLicenseInfo.Driver.PersonID);

            frmLicenseHistory frmLicense = new frmLicenseHistory(person.NationalNo);
            frmLicense.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmInternationalLicenseInfo frmInternationalLicenseInfo = new frmInternationalLicenseInfo(_InternationalLicenseID);
            frmInternationalLicenseInfo.ShowDialog();
        }
    }
}
