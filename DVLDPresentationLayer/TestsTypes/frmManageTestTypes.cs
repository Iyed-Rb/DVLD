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
    public partial class frmManageTestTypes : Form
    {
        public frmManageTestTypes()
        {
            InitializeComponent();
        }

        private void _RefreshApplicationsTypesList()
        {
            dgvAllTestTypes.DataSource = clsTestTypes.GetAllTestTypesList();
            int rowCount = dgvAllTestTypes.Rows.Count;
            lbCountRow.Text = rowCount.ToString();

        }
        private void frmManageTestTypes_Load(object sender, EventArgs e)
        {
            _RefreshApplicationsTypesList();
            dgvAllTestTypes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvAllTestTypes.AllowUserToAddRows = false;
            dgvAllTestTypes.ReadOnly = true;
        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditTestTypes frmEditTestTypes = new frmEditTestTypes((int)dgvAllTestTypes.CurrentRow.Cells[0].Value);
            frmEditTestTypes.ShowDialog();

            _RefreshApplicationsTypesList();
        }

        private void btCLose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
