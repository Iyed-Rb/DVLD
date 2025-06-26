using DVLDBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDPresentationLayer
{
    public partial class frmEditTestTypes : Form
    {
        private int _TestTypeID = -1;
        clsTestTypes _TestType;

        public frmEditTestTypes(int TestTypeID)
        {
            InitializeComponent();
            _TestTypeID = TestTypeID;

        }

        private void frmEditTestTypes_Load(object sender, EventArgs e)
        {
            this.AutoValidate = AutoValidate.EnableAllowFocusChange;
            _TestType = clsTestTypes.FindTestTypeByID(_TestTypeID);
            if (_TestType == null)
            {
                MessageBox.Show("Test Type not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            lbTestTypeID.Text = _TestType.TestTypeID.ToString();
            txtTitle.Text = _TestType.TestTypeTitle;
            txtDescription.Text = _TestType.TestTypeDescription;
            txtFees.Text = _TestType.TestTypeFees.ToString();
        }

        private void btCLose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btSave_Click(object sender, EventArgs e)
        {

            _TestType.TestTypeTitle = txtTitle.Text.Trim();
            _TestType.TestTypeDescription = txtDescription.Text.Trim();
            _TestType.TestTypeFees = Convert.ToDecimal(txtFees.Text.Trim().Replace(',', '.'),
             System.Globalization.CultureInfo.InvariantCulture);
            if (_TestType._UpdateTestType())
            {
                MessageBox.Show("Test Type updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Failed to update Test Type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                clsTestTypes.IsTestTypeTitleExists(txtTitle.Text.Trim()) &&
                txtTitle.Text.Trim().ToLower() != _TestType.TestTypeTitle.ToLower()
            )
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTitle, "This title already exists in another Test.");
            }
            else
            {
                errorProvider1.SetError(txtTitle, null);
            }
        }

        private void txtDescription_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtDescription, "Description cannot be empty.");
            }
            else if (
                clsTestTypes.IsTestTypesDescriptionExists(txtDescription.Text.Trim()) &&
                txtDescription.Text.Trim().ToLower() != _TestType.TestTypeDescription.ToLower()
            )
            {
                e.Cancel = true;
                errorProvider1.SetError(txtDescription, "This description already exists in another test.");
            }
            else
            {
                errorProvider1.SetError(txtDescription, null);
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
                txtDescription.Focus();     // move focus to password field
            }
        }

        private void txtDescription_KeyDown(object sender, KeyEventArgs e)
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
