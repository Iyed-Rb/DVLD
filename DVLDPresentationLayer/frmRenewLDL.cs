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
    public partial class frmRenewLDL : Form
    {
        public frmRenewLDL()
        {
            InitializeComponent();
        }

        private int _OldLicense;
        private int _NewLicenseID = -1;
        private void btCLose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void frmRenewLDL_Load(object sender, EventArgs e)
        {
            llShowLicenseHistory.Enabled = false;
            llShowNewLicenseInfo.Enabled = false;
            btRenew.Enabled = false;

            ctrlLicenseWithFilter1.OnLicenseSelected += ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected;

            lbIssueDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lbExpirationDate.Text = DateTime.Now.AddYears(1).ToString("dd/MMM/yyyy");
            lbApplicationDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");

            clsApplicationTypes ApplicationTypes = clsApplicationTypes.FindApplicationTypeByID(2);
            lbApplicationFees.Text = ApplicationTypes.ApplicationTypeFees.ToString("C2");

            lbCreatedByUserID.Text = clsGlobalSettings.CurrentUser.UserID.ToString();
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            int SelectedLicenseID = obj;
           
            lbOldLicenseID.Text = SelectedLicenseID.ToString();

            llShowLicenseHistory.Enabled = (SelectedLicenseID != -1);

            //MessageBox.Show("SelectedLicenseID == " + SelectedLicenseID, "info", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (SelectedLicenseID == -1)
            {
                llShowNewLicenseInfo.Enabled = false;
                //MessageBox.Show("SelectedLicenseID == -1 , return", "info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _OldLicense = SelectedLicenseID;

            clsLicenseClass licenseClass = clsLicenseClass.FindLicenseClassByID(ctrlLicenseWithFilter1.SelectedLicenseInfo.LicenseClassID);
            lbLicenseFees.Text = licenseClass.ClassFees.ToString("C2");

            lbTotalFees.Text = (clsApplicationTypes.FindApplicationTypeByID(2).ApplicationTypeFees + licenseClass.ClassFees).ToString("C2");

           
            if (ctrlLicenseWithFilter1.SelectedLicenseInfo.ExpirationDate > DateTime.Now)
            {
                MessageBox.Show("Selected License is not Expired\nIt will Expire on " + ctrlLicenseWithFilter1.SelectedLicenseInfo.ExpirationDate , "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                llShowNewLicenseInfo.Enabled = false;
                btRenew.Enabled = false;
                return;
            }

            if (ctrlLicenseWithFilter1.SelectedLicenseInfo.IsActive == false)
            {
                MessageBox.Show("This is the Old License and it is Disactivated\nThis is not the Current License ", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                llShowNewLicenseInfo.Enabled = false;
                btRenew.Enabled = false;
                return;
            }

            btRenew.Enabled = true;
            llShowNewLicenseInfo.Enabled = false;

        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clsPerson person = clsPerson.FindPersonByID(ctrlLicenseWithFilter1.SelectedLicenseInfo.Driver.PersonID);

            frmLicenseHistory frmLicense = new frmLicenseHistory(person.NationalNo);
            frmLicense.ShowDialog();
        }

        private void btRenew_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Renew the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            clsLicense NewLicense = new clsLicense();
            
            NewLicense.Application.ApplicantPersonID = ctrlLicenseWithFilter1.SelectedLicenseInfo.Driver.PersonID; 
            NewLicense.Application.ApplicationDate = DateTime.Now;
            NewLicense.Application.ApplicationTypeID = 2; //Renew License Application Type
            NewLicense.Application.ApplicationStatus = 3; //Application Status: Issued
            NewLicense.Application.LastStatusDate = DateTime.Now;
            NewLicense.Application.PaidFees = clsApplicationTypes.FindApplicationTypeByID(2).ApplicationTypeFees;
            NewLicense.Application.CreatedByUserID = clsGlobalSettings.CurrentUser.UserID;

            NewLicense.DriverID = ctrlLicenseWithFilter1.SelectedLicenseInfo.DriverID;
            NewLicense.LicenseClassID = ctrlLicenseWithFilter1.SelectedLicenseInfo.LicenseClassID;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.ExpirationDate = DateTime.Now.AddYears(clsLicenseClass.FindLicenseClassByID(ctrlLicenseWithFilter1.SelectedLicenseInfo.LicenseClassID).DefaultValidityLength);
            NewLicense.Notes = textBox1.Text;
            NewLicense.PaidFees = clsLicenseClass.FindLicenseClassByID(ctrlLicenseWithFilter1.SelectedLicenseInfo.LicenseClassID).ClassFees;
            NewLicense.IsActive = DateTime.Now != NewLicense.ExpirationDate;
            NewLicense.IssueReason = 2; //Renew License
            NewLicense.CreatedByUserID = clsGlobalSettings.CurrentUser.UserID;  


            if (!NewLicense.Save())
            {
                MessageBox.Show("Failed to Renew License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clsLicense OldLicense = clsLicense.FindLicenseByID(_OldLicense);
            OldLicense.IsActive = false; // Deactivate the old license
            OldLicense.Save();

            lnRenewLicenseApplicationID.Text = NewLicense.ApplicationID.ToString();
            _NewLicenseID = NewLicense.LicenseID;
            lbRenewedLicenseID.Text = NewLicense.LicenseID.ToString();
            MessageBox.Show("New License Issued Successfully with ID=" + NewLicense.LicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            llShowNewLicenseInfo.Enabled = true;
            btRenew.Enabled = false;
            ctrlLicenseWithFilter1.FilterEnabled = false;

        }

        private void llShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseInfo frmLicenseInfo = new frmLicenseInfo(_NewLicenseID);
            frmLicenseInfo.ShowDialog();
        }
    }
}
