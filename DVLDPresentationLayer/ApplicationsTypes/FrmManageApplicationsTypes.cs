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

namespace DVLDPresentationLayer.Applications
{
    public partial class FrmManageApplicationsTypes : Form
    {
        public FrmManageApplicationsTypes()
        {
            InitializeComponent();
        }


        private void _RefreshApplicationsTypesList()
        {
            dgvAllApplicationsTypes.DataSource = clsApplicationTypes.GetAllApplicationsTypes();
            int rowCount = dgvAllApplicationsTypes.Rows.Count;
            lbCountRow.Text = rowCount.ToString();

            if (dgvAllApplicationsTypes.Columns.Contains("ApplicationTypeID"))
                dgvAllApplicationsTypes.Columns["ApplicationTypeID"].Width = 85;

            if (dgvAllApplicationsTypes.Columns.Contains("ApplicationTypeTitle"))
                dgvAllApplicationsTypes.Columns["ApplicationTypeTitle"].Width = 220;

            if (dgvAllApplicationsTypes.Columns.Contains("ApplicationFees"))
                dgvAllApplicationsTypes.Columns["ApplicationFees"].Width = 100;


        }
        private void FrmManageApplicationsTypes_Load(object sender, EventArgs e)
        {
            _RefreshApplicationsTypesList();
            dgvAllApplicationsTypes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvAllApplicationsTypes.AllowUserToAddRows = false;
            dgvAllApplicationsTypes.ReadOnly = true;

        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditApplicationType frmEditApplicationType = new frmEditApplicationType((int)dgvAllApplicationsTypes.CurrentRow.Cells[0].Value);
            frmEditApplicationType.ShowDialog();

            _RefreshApplicationsTypesList();

        }
    }
}
