using DVLDBusinessLayer;
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
    public partial class ctrlInternationalLicenseInfo : UserControl
    {
        public ctrlInternationalLicenseInfo()
        {
            InitializeComponent();
        }

        public void FillInformation(int InternationalLicenseID)
        {
            clsInternationalLicense internationalLicense = clsInternationalLicense.FindInternationalLicenseByID(InternationalLicenseID);    
            if (internationalLicense == null)
            {
                MessageBox.Show("International License not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            clsPerson person = clsPerson.FindPersonByID(internationalLicense.Application.ApplicantPersonID);
            lbFullName.Text = person.FullName;
            lbInternationalLicenseID.Text = internationalLicense.InternationalLicenseID.ToString();
            lbLocalLicenceID.Text = internationalLicense.LocalLicenseID.ToString();
            lbNationalNo.Text = person.NationalNo;
            lbIssueDate.Text = internationalLicense.IssueDate.ToString("dd/MMM/yyyy");
            lbExpirationDate.Text = internationalLicense.ExpirationDate.ToString("dd/MMM/yyyy");
            lbApplicationID.Text = internationalLicense.Application.ApplicationID.ToString();
            if (person.Gendor == 0)
            {
                pbGendor.Image = Properties.Resources.Man_32;
                lbGender.Text = "Male";
            }
            else if (person.Gendor == 1)
            {
                pbGendor.Image = Properties.Resources.Male_512;
                lbGender.Text = "Male";
            }
            lbIsActive.Text = internationalLicense.IsActive ? "Yes" : "No";
            lbDateOfBirth.Text = person.DateOfBirth.ToString("dd/MMM/yyyy");
            lbDriverID.Text = internationalLicense.DriverID.ToString();

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

        private void ctrlInternationalLicenseInfo_Load(object sender, EventArgs e)
        {



        }
    }
}
