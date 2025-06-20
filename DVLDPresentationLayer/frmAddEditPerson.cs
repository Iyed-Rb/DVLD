using DVLDBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDPresentationLayer
{
    public partial class frmAddEditPerson : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;

        int _PersonID;
        clsPerson _Person;

        public frmAddEditPerson(int PersonID)
        {
            InitializeComponent();

            _PersonID = PersonID;

            if (_PersonID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;
        }

        public frmAddEditPerson()
        {
            InitializeComponent();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
        
            dateTimePicker1.MaxDate = DateTime.Today.AddYears(-18);
            
    
           
        }


        private void _FillCountriesInComoboBox()
        {
            DataTable dtCountries = clsCountry.GetAllCountries();

            foreach (DataRow row in dtCountries.Rows)
            {

                cbCountry.Items.Add(row["CountryName"]);

            }

        }


        private void btCLose_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            //if (clsPerson.isPersonExistByNationalNo(txtNationalNo.Text.ToString() ) )
            //{
            //    e.Cancel = true; // prevent focus from leaving
            //    errorProvider1.SetError(txtNationalNo, "National Number is used for Another Person");
            //}
            //else
            //{
            //    errorProvider1.SetError(txtNationalNo, ""); // Clear the error
            //}

            if (_Mode == enMode.AddNew)
            {
                if (clsPerson.isPersonExistByNationalNo(txtNationalNo.Text))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtNationalNo, "National Number is used for another person.");
                }
                else
                {
                    errorProvider1.SetError(txtNationalNo, "");
                }
            }
            else if (_Mode == enMode.Update)
            {
                // Only validate if national number was changed
                if (txtNationalNo.Text != _Person.NationalNo &&
                    clsPerson.isPersonExistByNationalNo(txtNationalNo.Text))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtNationalNo, "National Number is used for another person.");
                }
                else
                {
                    errorProvider1.SetError(txtNationalNo, "");
                }
            }


        }

        private void _LoadData()
        {

            dateTimePicker1.MaxDate = DateTime.Today.AddYears(-18);
            dateTimePicker1.Value = dateTimePicker1.MaxDate; // Set default to max date

            _FillCountriesInComoboBox();
            cbCountry.SelectedIndex = 2;

            if (_Mode == enMode.AddNew)
            {
                lbMode.Text = "Add New Person";
                _Person = new clsPerson();
                return;
            }

            _Person = clsPerson.Find(_PersonID);

            if (_Person == null)
            {
                MessageBox.Show("This form will be closed because No Person with ID = " + _PersonID);
                this.Close();

                return;
            }

            lbMode.Text = "Update Person";
            lbPersonID.Text = _PersonID.ToString();
            
            txtNationalNo.Text = _Person.NationalNo;
            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.SecondName;
            txtThirdName.Text = _Person.ThirdName;
            txtLastName.Text = _Person.LastName;
            txtEmail.Text = _Person.Email;

            if (_Person.Gendor == 0)
                radioButton1.Checked = true;
            else if (_Person.Gendor == 1)
                radioButton2.Checked = true;

            txtPhone.Text = _Person.Phone;
            txtAddress.Text = _Person.Address;
            dateTimePicker1.Value = _Person.DateOfBirth;

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
                    pbPersonImage.Image = null; // or default
                }
            }


            llRemoveImage.Visible = (_Person.ImagePath != "");

            //this will select the country in the combobox.
            cbCountry.SelectedIndex = cbCountry.FindString(clsCountry.Find(_Person.NationalityCountryID).CountryName);

        }


        private string GetImageFullPath(string imageFileName)
        {
            // Go up from /bin/Debug to the root of the solution
            string solutionRoot = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\.."));

            // Go to DVLDPresentationLayer\DVLDPeopleImages
            string imagesFolder = Path.Combine(solutionRoot, "DVLDPresentationLayer", "DVLDPeopleImages");

            return Path.Combine(imagesFolder, imageFileName);
        }

        private void frmAddEditPerson_Load(object sender,  EventArgs e)
        {
            _LoadData();
        }

        private void Gender_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                pbPersonImage.Image = Properties.Resources.Male_512;
            else if (radioButton2.Checked)
                pbPersonImage.Image = Properties.Resources.Female_512;
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return true; // Allow empty (optional)

            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }
        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (!IsValidEmail(txtEmail.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "Invalid email format.");
            }
            else
            {
                errorProvider1.SetError(txtEmail, "");
            }
        }

        private void txtPhone_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPhone, "Phone number is required.");
            }
            else
            {
                errorProvider1.SetError(txtPhone, "");
            }
        }

        private void txtAddress_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtAddress, "Address is required.");
            }
            else
            {
                errorProvider1.SetError(txtAddress, "");
            }
        }

        private void txtFirstName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFirstName, "First name is required.");
            }
            else
            {
                errorProvider1.SetError(txtFirstName, "");
            }
        }

        private void txtLastName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtLastName, "Last name is required.");
            }
            else
            {
                errorProvider1.SetError(txtLastName, "");
            }
        }

        private void btSave_Click(object sender, EventArgs e)
        {


            if (!ValidateChildren())
                return; // 🔁 Stop if any validation fails


            int CountryID = clsCountry.Find(cbCountry.Text).ID;

            _Person.NationalNo = txtNationalNo.Text;
            _Person.FirstName = txtFirstName.Text;
            _Person.SecondName = txtSecondName.Text;
            _Person.ThirdName = txtThirdName.Text;
            _Person.LastName = txtLastName.Text;
            _Person.Email = txtEmail.Text;
            _Person.Phone = txtPhone.Text;
            _Person.Address = txtAddress.Text;
            _Person.DateOfBirth = dateTimePicker1.Value;
            _Person.NationalityCountryID = CountryID;
            if (radioButton1.Checked)
                _Person.Gendor = 0;
            else if (radioButton2.Checked)
                _Person.Gendor = 1;

            if (!string.IsNullOrEmpty(_SelectedImagePath))
            {
                // User selected a new image → delete the old one (if exists)
                if (!string.IsNullOrEmpty(_Person.ImagePath))
                {
                    string oldImageFullPath = GetImageFullPath(_Person.ImagePath);

                    // 👇 Safely release the image
                    if (pbPersonImage.Image != null)
                    {
                        pbPersonImage.Image.Dispose(); // Fully releases the file
                        pbPersonImage.Image = null;
                        Application.DoEvents(); // Allow UI to process release
                    }

                    if (File.Exists(oldImageFullPath))
                    {
                        File.Delete(oldImageFullPath);
                    }
                }

                // Save the new image and update the path
                _Person.ImagePath = SaveImageToProjectFolder(_SelectedImagePath);
                pbPersonImage.Image = Image.FromFile(_SelectedImagePath); // Load the new image into PictureBox
            }



            if (_Person.Save())
                MessageBox.Show("Data Saved Successfully.");
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.");

            _Mode = enMode.Update;
            lbMode.Text = "Update Person";
            lbPersonID.Text = _Person.PersonID.ToString();
            

        }
        private string SaveImageToProjectFolder(string sourceImagePath)
        {
            // Validate the source path
            if (string.IsNullOrWhiteSpace(sourceImagePath) || !File.Exists(sourceImagePath))
                return "";

            // Generate a unique file name (e.g., 123e4567-e89b-12d3-a456-426614174000.jpg)
            string imageFileName = Guid.NewGuid().ToString() + Path.GetExtension(sourceImagePath);

            // Get the path to the DVLDPresentationLayer\DVLDPeopleImages folder
            string solutionRoot = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\.."));
            string imagesFolderPath = Path.Combine(solutionRoot, "DVLDPresentationLayer", "DVLDPeopleImages");

            // Ensure the folder exists
            Directory.CreateDirectory(imagesFolderPath);

            // Full destination path
            string destinationPath = Path.Combine(imagesFolderPath, imageFileName);

            // Copy the image into the project folder (overwrite: true)
            File.Copy(sourceImagePath, destinationPath, true);

            // Return just the filename (not full path) to be saved in DB or structure
            return imageFileName;
        }

        private void LinkLabelSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                _SelectedImagePath = ofd.FileName;
                pbPersonImage.Image = Image.FromFile(_SelectedImagePath);
            }
        }

        string _SelectedImagePath = "";
        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _SelectedImagePath = "";
            if (radioButton1.Checked)
                pbPersonImage.Image = Properties.Resources.Male_512;
            else if (radioButton2.Checked)
                pbPersonImage.Image = Properties.Resources.Female_512;
        }



    }
}
