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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DVLDPresentationLayer
{
    public partial class ctrlPersonInformation : UserControl
    {
        public int _PersonID = -1;
        string _NationalNo;
        clsPerson _Person;

        public ctrlPersonInformation()
        {
            InitializeComponent();
      
        }

        private void ctrlPersonInformation_Load(object sender, EventArgs e)
        {

        }

        public void LoadDataByNationalNo(string NationalNo)
        {
            _NationalNo = NationalNo;

            _Person = clsPerson.FindPersonByNationalNO(_NationalNo);

            if (_Person == null)
            {

                _PersonID = -1;
                lbPersonID.Text = "[????]";
                lbName.Text = "[????]";
                lbNationalNo.Text = "[????]";
                lbGender.Text = "[????]";
                lbEmail.Text = "[????]";
                lbAddress.Text = "[????]";
                lbDateOfBirth.Text = "[????]";
                lbPhone.Text = "[????]";
                lbCountry.Text = "[????]";
                pbPersonImage.Image = Properties.Resources.Male_512;
                MessageBox.Show("No Person with National No = " + _NationalNo, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            _PersonID = _Person.PersonID;
            lbPersonID.Text = _Person.PersonID.ToString();

            lbName.Text = _Person.FirstName + " " + _Person.SecondName + " " + _Person.ThirdName + " " + _Person.LastName;
            lbNationalNo.Text = _Person.NationalNo;
            if (_Person.Gendor == 0)
            {
                lbGender.Text = "Male";
                pbGender.Image = Properties.Resources.Man_32;
            }
            else if (_Person.Gendor == 1)
            {
                lbGender.Text = "Female";
                pbGender.Image = Properties.Resources.Woman_32;
            }

            lbEmail.Text = _Person.Email;
            lbAddress.Text = _Person.Address;
            lbDateOfBirth.Text = _Person.DateOfBirth.ToShortDateString();
            lbPhone.Text = _Person.Phone;
            clsCountry _Country = clsCountry.Find(_Person.NationalityCountryID);
            if (_Country != null)
            {
                lbCountry.Text = _Country.CountryName;
            }
            else
            {
                lbCountry.Text = "Unknown";
            }


            if (!string.IsNullOrEmpty(_Person.ImagePath))
            {
                string imageFullPath = GetImageFullPath(_Person.ImagePath);

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
                    if (_Person.Gendor == 0)
                    {
                        pbPersonImage.Image = Properties.Resources.Male_512;
                    }
                    else if (_Person.Gendor == 1)
                    {
                        pbPersonImage.Image = Properties.Resources.Female_512;
                    }

                }

            }
            else
            {
                //pbPersonImage.Image = null; // or default
                if (_Person.Gendor == 0)
                {
                    pbPersonImage.Image = Properties.Resources.Male_512;
                }
                else if (_Person.Gendor == 1)
                {
                    pbPersonImage.Image = Properties.Resources.Female_512;
                }

            }
        }


        public void LoadDataByPersonID(int PersonID)
        {
            _PersonID = PersonID;

            _Person = clsPerson.FindPersonByID(_PersonID);

            if (_Person == null)
            {
                _PersonID = -1; // Reset _PersonID if not found
                lbPersonID.Text = "[????]";
                lbName.Text = "[????]";
                lbNationalNo.Text = "[????]";
                lbGender.Text = "[????]";
                lbEmail.Text = "[????]";
                lbAddress.Text = "[????]";
                lbDateOfBirth.Text = "[????]";
                lbPhone.Text = "[????]";
                lbCountry.Text = "[????]";
                pbPersonImage.Image = Properties.Resources.Male_512;
                MessageBox.Show("No Person with ID = " + _PersonID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 
                
            }


            lbPersonID.Text = _PersonID.ToString();

            lbName.Text = _Person.FirstName + " " + _Person.SecondName + " " + _Person.ThirdName + " " + _Person.LastName;
            lbNationalNo.Text = _Person.NationalNo;
            if (_Person.Gendor == 0)
            {
                lbGender.Text = "Male";
                pbGender.Image = Properties.Resources.Man_32;
            }
            else if (_Person.Gendor == 1)
            {
                lbGender.Text = "Female";
                pbGender.Image = Properties.Resources.Woman_32;
            }

            lbEmail.Text = _Person.Email;
            lbAddress.Text = _Person.Address;
            lbDateOfBirth.Text = _Person.DateOfBirth.ToShortDateString();
            lbPhone.Text = _Person.Phone;
            clsCountry _Country = clsCountry.Find(_Person.NationalityCountryID);
            if (_Country != null)
            {
                lbCountry.Text = _Country.CountryName;
            }
            else
            {
                lbCountry.Text = "Unknown";
            }


            if (!string.IsNullOrEmpty(_Person.ImagePath))
            {
                string imageFullPath = GetImageFullPath(_Person.ImagePath);

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
                    if (_Person.Gendor == 0)
                    {
                        pbPersonImage.Image = Properties.Resources.Male_512;
                    }
                    else if (_Person.Gendor == 1)
                    {
                        pbPersonImage.Image = Properties.Resources.Female_512;
                    }

                }

            }
            else
            {
                //pbPersonImage.Image = null; // or default
                if (_Person.Gendor == 0)
                {
                    pbPersonImage.Image = Properties.Resources.Male_512;
                }
                else if (_Person.Gendor == 1)
                {
                    pbPersonImage.Image = Properties.Resources.Female_512;
                }

            }


        }

        private string GetImageFullPath(string imageFileName)
        {
            // Go up from /bin/Debug to the root of the solution
            string solutionRoot = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\.."));

            // Go to DVLDPresentationLayer\DVLDPeopleImages
            string imagesFolder = Path.Combine(solutionRoot, "DVLDPresentationLayer", "DVLDPeopleImages");

            return Path.Combine(imagesFolder, imageFileName);
        }

        private void LinkLabel_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddEditPerson frmEdit = new frmAddEditPerson(_PersonID);
            frmEdit.ShowDialog();

            LoadDataByPersonID(_PersonID);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}





