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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace DVLDPresentationLayer
{
    public partial class frmAddEditUser : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;


        int _UserID = -1;
        clsUser _User;


        public frmAddEditUser()
        {
            InitializeComponent();

            _Mode = enMode.AddNew;
        }
        public frmAddEditUser(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;

            if (_UserID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;
        }

        private void frmAddEditUser_Load(object sender, EventArgs e)
        {
       
            comboBox1.SelectedIndex = 0; // Default to PersonID search
            _LoadData();
            this.AutoValidate = AutoValidate.EnableAllowFocusChange;
        }

        private void btSearchPerson_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            ctrlPersonInformation1.LoadDataByPersonID( Convert.ToInt32(textBox1.Text) );

            else if (comboBox1.SelectedIndex == 1)
                ctrlPersonInformation1.LoadDataByNationalNo(textBox1.Text);

           
          
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (comboBox1.SelectedIndex == 0) // PersonID
            {
                // Only allow digits and control keys (like Backspace)
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true; // Block input
                }
            }
        }


      
        private void _LoadData()
        {

            if (_Mode == enMode.AddNew)
            {
                tbLoginInfo.Enabled = false;
                lbMode.Text = "Add New User";
                _User = new clsUser();
                return;
            }

            _User = clsUser.FindUserByID(_UserID);
       
            if (_User == null)
            {
                MessageBox.Show("No User with ID = " + _UserID, "User Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;

            }

            ctrlPersonInformation1.LoadDataByPersonID(_User.PersonID);
            tbLoginInfo.Enabled = true;
            tbPersonInfo.Enabled = true;
            btSave.Enabled = true;

            
            groupBox1.Enabled = false;


            lbMode.Text = "Update User";

            lbUserID.Text = _User.UserID.ToString(); 
            txtUserName.Text = _User.UserName;
            txtPassword.Text = _User.Password;
            txtConfirmPassword.Text = _User.Password;
            ChkBoxIsActive.Checked = _User.IsActive;

        }

        private void btNext_Click(object sender, EventArgs e)
        {
         

            if (_Mode == enMode.Update)
            {
                btSave.Enabled = true;
                tbLoginInfo.Enabled = true;
                tabControl1.SelectedTab = tabControl1.TabPages["tbLoginInfo"];

                return;
            }

            if (ctrlPersonInformation1._PersonID == -1)
            {
                MessageBox.Show("Please select a valid person first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (clsUser.IsUserExistByPersonID(ctrlPersonInformation1._PersonID))
            {
                MessageBox.Show("Selected Person already has a User. Choose another one.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            btSave.Enabled = true;
            tbLoginInfo.Enabled = true;
            tabControl1.SelectedTab = tabControl1.TabPages["tbLoginInfo"];

        }



        private void btSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
                return; // 🔁 Stop if any validation fails


            _User.UserName = txtUserName.Text.Trim();
            _User.Password = txtPassword.Text.Trim();
            _User.PersonID = ctrlPersonInformation1._PersonID;
            _User.IsActive = ChkBoxIsActive.Checked;

            if (_User.Save())
                MessageBox.Show("Data Saved Successfully.");
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.");

            _Mode = enMode.Update;
            lbMode.Text = "Update User";
            lbUserID.Text = _User.UserID.ToString();
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            //if (string.IsNullOrWhiteSpace(txtUserName.Text))
            //{
            //    e.Cancel = true;
            //    errorProvider1.SetError(txtUserName, "UserName can't be blank");
            //}
            //else
            //{
            //    errorProvider1.SetError(txtUserName, "");
            //}


            //if (clsUser.IsUserExistByUsername(txtUserName.Text.Trim()) && _Mode == enMode.AddNew)
            //{
            //    e.Cancel = true;
            //    errorProvider1.SetError(txtUserName, "UserName already exists. Please choose another one.");
            //}
            //else
            //{
            //    errorProvider1.SetError(txtUserName, "");
            //}

            if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "Username cannot be blank");
                return;
            }
            else
            {
                errorProvider1.SetError(txtUserName, null);
            };


            if (_Mode == enMode.AddNew)
            {

                if (clsUser.IsUserExistByUsername(txtUserName.Text.Trim()))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtUserName, "username is used by another user");
                }
                else
                {
                    errorProvider1.SetError(txtUserName, null);
                };
            }
            else
            {
                //incase update make sure not to use anothers user name
                if (_User.UserName != txtUserName.Text.Trim())
                {
                    if (clsUser.IsUserExistByUsername(txtUserName.Text.Trim()))
                    {
                        e.Cancel = true;
                        errorProvider1.SetError(txtUserName, "username is used by another user");
                        return;
                    }
                    else
                    {
                        errorProvider1.SetError(txtUserName, null);
                    };
                }
            }
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPassword, "Password cannot be blank");
            }
            else
            {
                errorProvider1.SetError(txtPassword, null);
            };
        }


        private void btCLose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btAddPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frmAddEditPerson = new frmAddEditPerson();

            frmAddEditPerson.DataBack += GetPersonID; // Subscribe to the event

            frmAddEditPerson.ShowDialog();
        }

        private void GetPersonID(object sender, int PersonID)
        {
            ctrlPersonInformation1._PersonID = PersonID;
            ctrlPersonInformation1.LoadDataByPersonID(ctrlPersonInformation1._PersonID);
        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep sound
                txtPassword.Focus();          // move focus to password field
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep sound
                txtConfirmPassword.Focus();          // move focus to password field
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Passwords do not match!");
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, null);
            };
        }
    }
}
