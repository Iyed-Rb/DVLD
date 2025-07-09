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
    public partial class frmListDetainedLicenses : Form
    {
        public frmListDetainedLicenses()
        {
            InitializeComponent();
        }

        DataTable dtDetainedLicenses;
        private void _RefreshList()
        {

            dtDetainedLicenses = clsDetainedLicense.GetAllDetainedLicenses();
            dgvDetainedLicenses.DataSource = dtDetainedLicenses;
            int rowCount = dgvDetainedLicenses.Rows.Count;
            lbCountRow.Text = rowCount.ToString();

            if (dgvDetainedLicenses.Rows.Count > 0)
            {
                dgvDetainedLicenses.Columns[0].HeaderText = "D.ID";
                dgvDetainedLicenses.Columns[0].Width = 60;

                dgvDetainedLicenses.Columns[1].HeaderText = "L.ID";
                dgvDetainedLicenses.Columns[1].Width = 60;

                dgvDetainedLicenses.Columns[2].HeaderText = "D.Date";
                dgvDetainedLicenses.Columns[2].Width = 120;

                dgvDetainedLicenses.Columns[3].HeaderText = "Is Released";
                dgvDetainedLicenses.Columns[3].Width = 80;

                dgvDetainedLicenses.Columns[4].HeaderText = "Fine Fees";
                dgvDetainedLicenses.Columns[4].Width = 100;

                dgvDetainedLicenses.Columns[5].HeaderText = "Release Date";
                dgvDetainedLicenses.Columns[5].Width = 120;

                dgvDetainedLicenses.Columns[6].HeaderText = "N.No.";
                dgvDetainedLicenses.Columns[6].Width = 60;

                dgvDetainedLicenses.Columns[7].HeaderText = "Full Name";
                dgvDetainedLicenses.Columns[7].Width = 270;

                dgvDetainedLicenses.Columns[8].HeaderText = "Rlease App.ID";
                dgvDetainedLicenses.Columns[8].Width = 110;
            }

        }

        private void frmListDetainedLicenses_Load(object sender, EventArgs e)
        {
            _RefreshList();
            comboBox2.Visible = false;
            textBox1.Visible = false;

            dgvDetainedLicenses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            comboBox1.SelectedIndex = 0;
            textBox1.Visible = false;

            dgvDetainedLicenses.AllowUserToAddRows = false;
            dgvDetainedLicenses.ReadOnly = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != 0)
            {
                if (comboBox1.SelectedIndex == 2)
                {
                    comboBox2.Visible = true;
                    comboBox2.SelectedIndex = 0;
                    textBox1.Visible = false;
                }
                else
                {
                    textBox1.Visible = true;
                    comboBox2.Visible = false;
                }
            }
            else
            {
                textBox1.Visible = false;
                textBox1.Text = "";

                DataView dv = dtDetainedLicenses.DefaultView;
                dv.RowFilter = ""; // Clear filter
                dgvDetainedLicenses.DataSource = dv;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (dtDetainedLicenses == null || comboBox1.SelectedIndex == 0)
                return;

            string SelectedColumn = "";

            switch (comboBox1.SelectedIndex)
            {
                case 1: SelectedColumn = "DetainID"; break;
                case 2: SelectedColumn = "IsReleased"; break;
                case 3: SelectedColumn = "NationalNo"; break;
                case 4: SelectedColumn = "FullName"; break;
                case 5: SelectedColumn = "ReleaseApplicationID"; break;

                default: return;
            }

            string keyword = textBox1.Text.Replace("'", "''"); // Escape single quotes

            try
            {
                DataView dv = dtDetainedLicenses.DefaultView;

                if (SelectedColumn == "DetainID" || SelectedColumn == "ReleaseApplicationID")
                {
                    if (int.TryParse(textBox1.Text, out int id))
                        dv.RowFilter = $"[{SelectedColumn}] = {id}";

                    else
                        dv.RowFilter = "1 = 0"; // Invalid input, show no results
                }
                else if (SelectedColumn == "FullName" || SelectedColumn == "NationalNo")
                {
                    dv.RowFilter = $"[{SelectedColumn}] LIKE '%{keyword}%'";
                    //dv.RowFilter = $"[{SelectedColumn}] = '{keyword}'";
                }

                dgvDetainedLicenses.DataSource = dv;
            }
            catch (Exception ex)
            {
                // You can log the error or show it
                // MessageBox.Show("Filter error: " + ex.Message);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataView dv = dtDetainedLicenses.DefaultView;

            switch (comboBox2.SelectedIndex)
            {
                case 0: // All
                    dv.RowFilter = ""; // No filter
                    break;
                case 1: // Released Only
                    dv.RowFilter = "[IsReleased] = true";
                    break;
                case 2: // No Released Only
                    dv.RowFilter = "[IsReleased] = false";
                    break;
            }

            dgvDetainedLicenses.DataSource = dv;
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLicenseHistory licenseHistory = new frmLicenseHistory((string)dgvDetainedLicenses.CurrentRow.Cells[6].Value);
            licenseHistory.ShowDialog();
            frmListDetainedLicenses_Load(null, null);
            //frmListDetainedLicenses_Load(this, EventArgs.Empty);

        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmReleaseDetainedLicense frmReleaseDetainedLicense = new FrmReleaseDetainedLicense((int)dgvDetainedLicenses.CurrentRow.Cells[1].Value);
            frmReleaseDetainedLicense.ShowDialog();
            frmListDetainedLicenses_Load(null, null);
            //frmListDetainedLicenses_Load(this, EventArgs.Empty);

        }

        private void ShowLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLicenseInfo frmLicenseInfo = new frmLicenseInfo((int)dgvDetainedLicenses.CurrentRow.Cells[1].Value);
            frmLicenseInfo.ShowDialog();
            frmListDetainedLicenses_Load(null, null);
            //frmListDetainedLicenses_Load(this, EventArgs.Empty);
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsPerson person = clsPerson.FindPersonByNationalNO((string)dgvDetainedLicenses.CurrentRow.Cells[6].Value);
            FrmShowDetails frmShowDetails = new FrmShowDetails(person.PersonID);
            frmListDetainedLicenses_Load(null, null);
            //frmListDetainedLicenses_Load(this, EventArgs.Empty);

        }

        private void btRelease_Click(object sender, EventArgs e)
        {
            FrmReleaseDetainedLicense frmReleaseDetainedLicense = new FrmReleaseDetainedLicense();
            frmReleaseDetainedLicense.ShowDialog();
            frmListDetainedLicenses_Load(this, EventArgs.Empty);
        }

        private void btDetain_Click(object sender, EventArgs e)
        {
            frmDetainLicense frmDetainLicense = new frmDetainLicense();
            frmDetainLicense.ShowDialog();
            frmListDetainedLicenses_Load(this, EventArgs.Empty);
        }

        private void dgvDetainedLicenses_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvDetainedLicenses_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDetainedLicenses.CurrentRow == null)
                return;

            if ((bool)dgvDetainedLicenses.CurrentRow.Cells[3].Value == true)
            {
                releaseDetainedLicenseToolStripMenuItem.Enabled = false;
            }
        }
    }
}
