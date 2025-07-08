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

namespace DVLDPresentationLayer
{
    public partial class ctrlLicenseWithFilter : UserControl
    {
        // Define a custom event handler delegate with parameters
        public event Action<int> OnLicenseSelected;
        // Create a protected method to raise the event with a parameter
        protected virtual void PersonSelected(int LicenseID)
        {
            //Action<int> handler = OnLicenseSelected;
            //if (handler != null)
            //{
            //    handler(LicenseID); // Raise the event with the parameter
            //}

            OnLicenseSelected?.Invoke(LicenseID);
        }

        public ctrlLicenseWithFilter()
        {
            InitializeComponent();
        }

        private bool _FilterEnabled = true;

        public bool FilterEnabled
        {
            get
            {
                return _FilterEnabled;
            }
            set
            {
                _FilterEnabled = value;
                groupBox1.Enabled = _FilterEnabled;
            }
        }

        //public int SelectedLicenseID = -1;
        //int _LicenseID;

        private int _LicenseID = -1;

        public int LicenseID
        {
            get { return ctrlLicenseInfo1.LicenseID; }
        }
        public clsLicense SelectedLicenseInfo
        { get { return ctrlLicenseInfo1.SelectedLicenseInfo; } }

        private void LoadLicenseInfo(int LicenseID)
        {
            txtLicenseID.Text = LicenseID.ToString();
            ctrlLicenseInfo1.LoadInfo(LicenseID);
            _LicenseID = ctrlLicenseInfo1.LicenseID;
            if (OnLicenseSelected != null && FilterEnabled)
                // Raise the event with a parameter
                OnLicenseSelected(_LicenseID);
        }

        private void button1_Click(object sender, EventArgs e)
        {
        
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLicenseID.Focus();
                return;

            }
            _LicenseID = int.Parse(txtLicenseID.Text);
            LoadLicenseInfo(_LicenseID);
        }

        private void txtLicenseID_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtLicenseID.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtLicenseID, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(txtLicenseID, null);
            }
        }

        private void ctrlLicenseInfo1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlLicenseWithFilter_Load(object sender, EventArgs e)
        {

        }
    }
}
