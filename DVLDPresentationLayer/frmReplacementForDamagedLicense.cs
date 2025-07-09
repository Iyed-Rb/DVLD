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
    public partial class frmReplacementForDamagedLicense : Form
    {
        public frmReplacementForDamagedLicense()
        {
            InitializeComponent();
        }

        private int _OldLicense;
        private int _NewLicenseID = -1;

        private void frmReplacementForDamagedLicense_Load(object sender, EventArgs e)
        {
            llShowLicenseHistory.Enabled = false;
            llShowNewLicenseInfo.Enabled = false;
            btIssue.Enabled = false;

            ctrlLicenseWithFilter1.OnLicenseSelected += ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected;

            //lbIssueDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            //lbExpirationDate.Text = DateTime.Now.AddYears(1).ToString("dd/MMM/yyyy");
            lbApplicationDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");

            clsApplicationTypes ApplicationTypes = clsApplicationTypes.FindApplicationTypeByID(4);
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

            //clsLicenseClass licenseClass = clsLicenseClass.FindLicenseClassByID(ctrlLicenseWithFilter1.SelectedLicenseInfo.LicenseClassID);
            //lbLicenseFees.Text = licenseClass.ClassFees.ToString("C2");

            //lbTotalFees.Text = (clsApplicationTypes.FindApplicationTypeByID(2).ApplicationTypeFees + licenseClass.ClassFees).ToString("C2");


            if (ctrlLicenseWithFilter1.SelectedLicenseInfo.ExpirationDate < DateTime.Now)
            {
                MessageBox.Show("Selected License is Expired\nIt will Expire on " + ctrlLicenseWithFilter1.SelectedLicenseInfo.ExpirationDate, "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                llShowNewLicenseInfo.Enabled = false;
                btIssue.Enabled = false;
                return;
            }

            if (ctrlLicenseWithFilter1.SelectedLicenseInfo.IsActive == false)
            {
                MessageBox.Show("This is the Old License and it is Disactivated\nThis is not the Current License ", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                llShowNewLicenseInfo.Enabled = false;
                btIssue.Enabled = false;
                return;
            }
            if (clsDetainedLicense.IsLicenseDetained(_OldLicense))
            {
                MessageBox.Show("Selected License is Detained\nYou must Release it First", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                llShowNewLicenseInfo.Enabled = false;
                btIssue.Enabled = false;
                return;
            }



            btIssue.Enabled = true;
            llShowNewLicenseInfo.Enabled = false;

        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDamagedLicense.Checked == true)
            {
                clsApplicationTypes ApplicationTypes = clsApplicationTypes.FindApplicationTypeByID(4);
                lbApplicationFees.Text = ApplicationTypes.ApplicationTypeFees.ToString("C2");
            }
        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLostLicense.Checked == true)
            {
                clsApplicationTypes ApplicationTypes = clsApplicationTypes.FindApplicationTypeByID(3);
                lbApplicationFees.Text = ApplicationTypes.ApplicationTypeFees.ToString("C2");
            }
        }

        private void btIssue_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to Issue a Replacement for the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            clsLicense NewLicense = new clsLicense();

            NewLicense.Application.ApplicantPersonID = ctrlLicenseWithFilter1.SelectedLicenseInfo.Driver.PersonID;
            NewLicense.Application.ApplicationDate = DateTime.Now;
            if (rbLostLicense.Checked == true)
            {
                NewLicense.Application.ApplicationTypeID = 3; //Lost License Application Type
            }
            else if (rbDamagedLicense.Checked == true)
            {
                NewLicense.Application.ApplicationTypeID = 4; //Damaged License Application Type
            }
 
            NewLicense.Application.ApplicationStatus = 3; //Application Status: Issued
            NewLicense.Application.LastStatusDate = DateTime.Now;
            NewLicense.Application.PaidFees = clsApplicationTypes.FindApplicationTypeByID(NewLicense.Application.ApplicationTypeID).ApplicationTypeFees;
            NewLicense.Application.CreatedByUserID = clsGlobalSettings.CurrentUser.UserID;

            NewLicense.DriverID = ctrlLicenseWithFilter1.SelectedLicenseInfo.DriverID;
            NewLicense.LicenseClassID = ctrlLicenseWithFilter1.SelectedLicenseInfo.LicenseClassID;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.ExpirationDate = ctrlLicenseWithFilter1.SelectedLicenseInfo.ExpirationDate;
            //NewLicense.Notes = textBox1.Text;
            NewLicense.PaidFees = clsLicenseClass.FindLicenseClassByID(ctrlLicenseWithFilter1.SelectedLicenseInfo.LicenseClassID).ClassFees;
            NewLicense.IsActive = true;
      
            if (rbLostLicense.Checked == true)
            {
                NewLicense.IssueReason = 4; //Lost License
            }
            else if (rbDamagedLicense.Checked == true)
            {
                NewLicense.IssueReason = 3; //Damaged License
            }
            NewLicense.CreatedByUserID = clsGlobalSettings.CurrentUser.UserID;


            if (!NewLicense.Save())
            {
                MessageBox.Show("Failed to Issue License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clsLicense OldLicense = clsLicense.FindLicenseByID(_OldLicense);
            OldLicense.IsActive = false; // Deactivate the old license
            OldLicense.Save();

            lbReplaceApplicationID.Text = NewLicense.ApplicationID.ToString();
            _NewLicenseID = NewLicense.LicenseID;
            lbReplacedLicenseID.Text = NewLicense.LicenseID.ToString();
            MessageBox.Show("New License Issued Successfully with ID=" + NewLicense.LicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            llShowNewLicenseInfo.Enabled = true;
            btIssue.Enabled = false;
            ctrlLicenseWithFilter1.FilterEnabled = false;
        }

        private void llShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseInfo frmLicenseInfo = new frmLicenseInfo(_NewLicenseID);
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
