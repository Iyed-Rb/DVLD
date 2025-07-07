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
    public partial class DriverLicenses : UserControl
    {
        public DriverLicenses()
        {
            InitializeComponent();
        }

        private void DriverLicenses_Load(object sender, EventArgs e)
        {

        }

        public void FillData(int PersonID)
        {

           
            
            
                dgvLocalLicenses.DataSource = clsLicense.GetAllLicensesList(PersonID);
                int rowCount = dgvLocalLicenses.Rows.Count;
                lbCountRow.Text = rowCount.ToString();

                if (dgvLocalLicenses.Columns.Contains("Lic.ID"))
                    dgvLocalLicenses.Columns["Lic.ID"].Width = 100;

                if (dgvLocalLicenses.Columns.Contains("App.ID"))
                    dgvLocalLicenses.Columns["App.ID"].Width = 100;

                if (dgvLocalLicenses.Columns.Contains("Class Name"))
                    dgvLocalLicenses.Columns["Class Name"].Width = 280;

                if (dgvLocalLicenses.Columns.Contains("Issue Date"))
                    dgvLocalLicenses.Columns["Issue Date"].Width = 170;

                if (dgvLocalLicenses.Columns.Contains("Expiration Date"))
                    dgvLocalLicenses.Columns["Expiration Date"].Width = 170;

                if (dgvLocalLicenses.Columns.Contains("Is Active"))
                    dgvLocalLicenses.Columns["Is Active"].Width = 100;

            
                dgvAllInternationalLicenses.DataSource = clsInternationalLicense.GetInternationalLicensesByPersonID(PersonID);
                int rowCount2 = dgvAllInternationalLicenses.Rows.Count;
                lbCountRow.Text = rowCount2.ToString();

                if (dgvAllInternationalLicenses.Columns.Contains("Int.License ID"))
                    dgvAllInternationalLicenses.Columns["Int.License ID"].Width = 140;

                if (dgvAllInternationalLicenses.Columns.Contains("Application ID"))
                    dgvAllInternationalLicenses.Columns["Application ID"].Width = 140;

                if (dgvAllInternationalLicenses.Columns.Contains("L.License ID"))
                    dgvAllInternationalLicenses.Columns["L.License ID"].Width = 200;

            if (dgvAllInternationalLicenses.Columns.Contains("Issue Date"))
                dgvAllInternationalLicenses.Columns["Issue Date"].Width = 170;

            if (dgvAllInternationalLicenses.Columns.Contains("Expiration Date"))
                dgvAllInternationalLicenses.Columns["Expiration Date"].Width = 170;

                if (dgvAllInternationalLicenses.Columns.Contains("Is Active"))
                    dgvAllInternationalLicenses.Columns["Is Active"].Width = 100;
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dgvLocalLicenses_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
             
        }

        private void dgvAllInternationalLicenses_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
