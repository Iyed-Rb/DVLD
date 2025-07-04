﻿using DVLDBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDPresentationLayer
{
    public partial class ctrlLicenseInfo : UserControl
    {
        int _LDLApplicationID;
        clsLDLApplication _LDLApplication;
        public ctrlLicenseInfo()
        {
            InitializeComponent();
        }

        public void FillData(int LDLApplication)
        {

            _LDLApplicationID = LDLApplication;

            _LDLApplication = clsLDLApplication.FindLDLApplicationByID(_LDLApplicationID);
            if (_LDLApplication == null)
            {
                MessageBox.Show("LDL Application not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            clsPerson person = clsPerson.FindPersonByID(_LDLApplication.Application.ApplicantPersonID);
            lbClassName.Text = _LDLApplication.LicenseClass.ClassName;
            lbFullName.Text = person.FullName;
            clsLicense license = clsLicense.FindLicenseByApplicationID(_LDLApplication.ApplicationID);
            lbLicenseID.Text = license.LicenseID.ToString();
            lbNationalNo.Text = person.NationalNo;
            if (person.Gendor == 0)
            {
                pbGendor.Image = Properties.Resources.Man_32;
                lbGendor.Text = "Male";
            }
            else if (person.Gendor == 1)
            {
                pbGendor.Image = Properties.Resources.Male_512;
                lbGendor.Text = "Male";
            }

            lbIssueDate.Text = license.IssueDate.ToString("dd/MMM/yyyy");
            switch (license.IssueReason)
            {
                case 1: lbIssueReason.Text = "First Time";
                  break;
                case 2: lbIssueReason.Text = "Renewal";
                  break;
                case 3: lbIssueReason.Text = "Replacement";
                  break;
            }
            if (license.Notes == null || license.Notes == "") 
                {
                lbNotes.Text = "No Notes";
            }
            else
            {
                lbNotes.Text = license.Notes;
            }
           
           
            lbIsActive.Text = license.IsActive ? "Yes" : "No";
            lbDateOfBirth.Text = person.DateOfBirth.ToString("dd/MMM/yyyy");
            lbDriverID.Text = license.Driver.DriverID.ToString();
            lbExpirationDate.Text = license.ExpirationDate.ToString("dd/MMM/yyyy");
            lbIsDetained.Text = "No";


            if (!string.IsNullOrEmpty(person.ImagePath))
            {
                string imageFullPath = GetImageFullPath(person.ImagePath);

                if (File.Exists(imageFullPath))
                {
                    using (var stream = new FileStream(imageFullPath, FileMode.Open, FileAccess.Read))
                    {
                        pbPersonImage.Image = Image.FromStream(stream);
                    }
                }
                else
                {
                    //pbPersonImage.Image = null; // or default
                    if (person.Gendor == 0)
                    {
                        pbPersonImage.Image = Properties.Resources.Male_512;
                    }
                    else if (person.Gendor == 1)
                    {
                        pbPersonImage.Image = Properties.Resources.Female_512;
                    }

                }

            }
            else
            {
                //pbPersonImage.Image = null; // or default
                if (person.Gendor == 0)
                {
                    pbPersonImage.Image = Properties.Resources.Male_512;
                }
                else if (person.Gendor == 1)
                {
                    pbPersonImage.Image = Properties.Resources.Female_512;
                }

            }
        }
        private string GetImageFullPath(string imageFileName)
        {
            // Go up from /bin/Debug to the root of the solution
            string solutionRoot = Path.GetFullPath(Path.Combine(System.Windows.Forms.Application.StartupPath, @"..\..\.."));

            // Go to DVLDPresentationLayer\DVLDPeopleImages
            string imagesFolder = Path.Combine(solutionRoot, "DVLDPresentationLayer", "DVLDPeopleImages");

            return Path.Combine(imagesFolder, imageFileName);
        }
    }
       
}
