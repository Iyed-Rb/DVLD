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
using System.Globalization;


namespace DVLDPresentationLayer.Applications
{
    public partial class frmEditApplicationType : Form
    {
        private int _ApplicationTypeID = -1;
        clsApplicationTypes _ApplicationType;
        public frmEditApplicationType(int ApplicationTypeID)
        {
            InitializeComponent();
            _ApplicationTypeID = ApplicationTypeID;
        }

        private void frmEditApplicationType_Load(object sender, EventArgs e)
        {
            this.AutoValidate = AutoValidate.EnableAllowFocusChange;
            _ApplicationType = clsApplicationTypes.FindApplicationTypeByID(_ApplicationTypeID);
            if (_ApplicationType == null)
            {
                MessageBox.Show("Application Type not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            lbApplicationTypeID.Text = _ApplicationType.ApplicationTypeID.ToString();
            txtTitle.Text = _ApplicationType.ApplicationTypeTitle;
            txtFees.Text = _ApplicationType.ApplicationTypeFees.ToString();
        }

        private void btCLose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btSave_Click(object sender, EventArgs e)
        {

            _ApplicationType.ApplicationTypeTitle = txtTitle.Text.Trim();
            _ApplicationType.ApplicationTypeFees = Convert.ToDecimal(
             txtFees.Text.Trim().Replace(',', '.'),
             System.Globalization.CultureInfo.InvariantCulture);
            if (_ApplicationType._UpdateApplicationType())
            {
                MessageBox.Show("Application Type updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
 
            }
            else
            {
                MessageBox.Show("Failed to update Application Type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTitle, "Title cannot be empty.");
            }
            else if (
                clsApplicationTypes.IsApplicationTypeTitleExists(txtTitle.Text.Trim()) &&
                txtTitle.Text.Trim().ToLower() != _ApplicationType.ApplicationTypeTitle.ToLower()
            )
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTitle, "This title already exists in another application.");
            }
            else
            {
                errorProvider1.SetError(txtTitle, null);
            }
        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            string input = txtFees.Text.Trim().Replace(',', '.');

            if (string.IsNullOrWhiteSpace(input))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Fees cannot be empty.");
            }
            else if (!decimal.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out _))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Invalid fee format. Use 15.25 for example");
            }
            else
            {
                errorProvider1.SetError(txtFees, null);
            }
        }

        private void txtTitle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep sound
                txtFees.Focus();     // move focus to password field
            }
        }

        private void txtFees_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep sound
                btSave.Focus();     // move focus to password field
            }
        }
    }
}
