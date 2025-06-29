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
    public partial class frmManageLDLApplications : Form
    {
        public frmManageLDLApplications()
        {
            InitializeComponent();
        }
        DataTable _dtLDLApplications;

        private void _RefreshApplicationList()
        {
            _dtLDLApplications = clsLDLApplication.GetAllLDLApplications();
            dgvAllLDLApplications.DataSource = _dtLDLApplications;
            int rowCount = dgvAllLDLApplications.Rows.Count;
            lbCountRow.Text = rowCount.ToString();

            dgvAllLDLApplications.Columns["L.D.LAppID"].Width = 90;
            dgvAllLDLApplications.Columns["Driving Class"].Width = 190;
            dgvAllLDLApplications.Columns["NationalNo"].Width = 90;
            dgvAllLDLApplications.Columns["Full Name"].Width = 230;
            dgvAllLDLApplications.Columns["ApplicationDate"].Width = 110;
            dgvAllLDLApplications.Columns["Passed Tests"].Width = 100;
            dgvAllLDLApplications.Columns["Status"].Width = 90;
            


        }
        private void frmNewLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            _RefreshApplicationList();
            dgvAllLDLApplications.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvAllLDLApplications.AllowUserToAddRows = false;
            dgvAllLDLApplications.ReadOnly = true;

        }

        private void btLDLApplication_Click(object sender, EventArgs e)
        {
            AddEditLDLApplications frmAddEditLDLApplications = new AddEditLDLApplications();
            frmAddEditLDLApplications.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEditLDLApplications frmAddEditLDLApplications = new AddEditLDLApplications((int)dgvAllLDLApplications.CurrentRow.Cells[0].Value);
            frmAddEditLDLApplications.ShowDialog();
        }
    }
}
