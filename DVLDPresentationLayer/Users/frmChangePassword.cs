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

namespace DVLDPresentationLayer.Users
{
    public partial class frmChangePassword : Form
    {
        int _UserID;
        clsUser _User;
        public frmChangePassword(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;

        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            txtCurrentPassword.Text = "";
            txtBoxNewPassword.Text = "";
            txtBoxConfirmPassword.Text = "";

            _User = clsUser.FindUserByID(_UserID);
            ctrlUserInfo1.LoadUserDataByID(_UserID);
            if (_User == null)
            {
                MessageBox.Show("User not found.");
                this.Close();
                return;
            }


            this.AutoValidate = AutoValidate.EnableAllowFocusChange;

        }

        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCurrentPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "Password cannot be blank");
                return;
            }
            else if (txtCurrentPassword.Text.Trim() != _User.Password)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "Current password is incorrect");
                return;
            }
            else
            {
                errorProvider1.SetError(txtCurrentPassword, null);
            };

        }

        private void txtBoxNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtBoxNewPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtBoxNewPassword, "New password cannot be blank");
                return;
            }
            else
            {
                errorProvider1.SetError(txtBoxNewPassword, null);
            };
        }

        private void txtBoxConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtBoxNewPassword.Text != txtBoxConfirmPassword.Text)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtBoxConfirmPassword, "Passwords do not match!");
                return;
            }
            else errorProvider1.SetError(txtBoxConfirmPassword, null);
        }

        private void btCLose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
                return; // 🔁 Stop if any validation fails

            _User.Password = txtBoxNewPassword.Text.Trim();

            if (_User.Save())
                MessageBox.Show("Data Saved Successfully.");
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.");

        }

        private void txtCurrentPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep sound
                txtBoxNewPassword.Focus();          // move focus to password field
            }
        }

        private void txtBoxNewPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep sound
                txtBoxConfirmPassword.Focus();     // move focus to password field
            }
        }

        private void txtBoxConfirmPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep sound
                btSave.Focus();     // move focus to password field
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void lbIsActive_Click(object sender, EventArgs e)
        {

        }

        private void ctrlUserInfo1_Load(object sender, EventArgs e)
        {

        }
    }
}
    

