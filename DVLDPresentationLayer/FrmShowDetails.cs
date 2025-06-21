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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace DVLDPresentationLayer
{
    public partial class FrmShowDetails : Form
    {
        int _PersonID;
        clsPerson _Person;
        public FrmShowDetails(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
        }

        private void LoadData()
        {

            _Person = clsPerson.Find(_PersonID);

            if (_Person == null)
            {
                MessageBox.Show("This form will be closed because No Person with ID = " + _PersonID);
                this.Close();

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

        private void LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddEditPerson frmEdit = new frmAddEditPerson(_PersonID);
            frmEdit.ShowDialog();

            LoadData();

        
        }

        private void btCLose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmShowDetails_Load(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
