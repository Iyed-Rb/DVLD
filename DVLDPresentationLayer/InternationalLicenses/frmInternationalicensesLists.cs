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
    public partial class frmInternationalicensesLists : Form
    {
        public frmInternationalicensesLists()
        {
            InitializeComponent();
        }

        private void btCLose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        DataTable _dtAllInternationalLicences;
        private void _RefreshList()
        {
            _dtAllInternationalLicences = clsInternationalLicense.GetAllInternationalLicenses();
            dgvAllinternationalLicences.DataSource = _dtAllInternationalLicences;

            int rowCount = dgvAllinternationalLicences.Rows.Count;
            lbCountRow.Text = rowCount.ToString();

            if (dgvAllinternationalLicences.Columns.Contains("Int.License ID"))
                dgvAllinternationalLicences.Columns["Int.License ID"].Width = 100;

            if (dgvAllinternationalLicences.Columns.Contains("Application ID"))
                dgvAllinternationalLicences.Columns["Application ID"].Width = 100;

            if (dgvAllinternationalLicences.Columns.Contains("L.License ID"))
                dgvAllinternationalLicences.Columns["L.License ID"].Width = 95;

            if (dgvAllinternationalLicences.Columns.Contains("Driver ID"))
                dgvAllinternationalLicences.Columns["Driver ID"].Width = 90;

            if (dgvAllinternationalLicences.Columns.Contains("Issue Date"))
                dgvAllinternationalLicences.Columns["Issue Date"].Width = 130;

            if (dgvAllinternationalLicences.Columns.Contains("Expiration Date"))
                dgvAllinternationalLicences.Columns["Expiration Date"].Width = 130;

            if (dgvAllinternationalLicences.Columns.Contains("Is Active"))
                dgvAllinternationalLicences.Columns["Is Active"].Width = 90;

        }
        private void frmInternationalicensesLists_Load(object sender, EventArgs e)
        {
            _RefreshList();
            dgvAllinternationalLicences.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            comboBox1.SelectedIndex = 0;
            textBox1.Visible = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != 0)
            {
                if (comboBox1.SelectedIndex == 5)
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

                DataView dv = _dtAllInternationalLicences.DefaultView;
                dv.RowFilter = ""; // Clear filter
                dgvAllinternationalLicences.DataSource = dv;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (_dtAllInternationalLicences == null || comboBox1.SelectedIndex == 0)
                return;

            string SelectedColumn = "";

            switch (comboBox1.SelectedIndex)
            {
                case 1: SelectedColumn = "Int.License ID"; break;
                case 2: SelectedColumn = "Application ID"; break;
                case 3: SelectedColumn = "Driver ID"; break;
                case 4: SelectedColumn = "L.License ID"; break;
                case 5: SelectedColumn = "Is Active"; break;

                default: return;
            }

            string keyword = textBox1.Text.Replace("'", "''"); // Escape single quotes

            try
            {
                DataView dv = _dtAllInternationalLicences.DefaultView;

                if (SelectedColumn == "Int.License ID" || SelectedColumn == "Application ID" ||
                    SelectedColumn == "Driver ID" || SelectedColumn == "L.License ID")
                {
                    if (int.TryParse(textBox1.Text, out int id))
                        dv.RowFilter = $"[{SelectedColumn}] = {id}";

                    else
                        dv.RowFilter = "1 = 0"; // Invalid input, show no results
                }
                //else if (SelectedColumn == "Int.License ID" || SelectedColumn == "Application ID" ||
                //    SelectedColumn == "Driver ID" || SelectedColumn == "L.License ID")
                //{
                //    dv.RowFilter = $"[{SelectedColumn}] LIKE '%{keyword}%'";
                //    //dv.RowFilter = $"[{SelectedColumn}] = '{keyword}'";
                //}

                dgvAllinternationalLicences.DataSource = dv;
            }
            catch (Exception ex)
            {
                // You can log the error or show it
                // MessageBox.Show("Filter error: " + ex.Message);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataView dv = _dtAllInternationalLicences.DefaultView;

            switch (comboBox2.SelectedIndex)
            {
                case 0: // All
                    dv.RowFilter = ""; // No filter
                    break;
                case 1: // Active Only
                    dv.RowFilter = "[Is Active] = true";
                    break;
                case 2: // Inactive Only
                    dv.RowFilter = "[Is Active] = false";
                    break;
            }

            dgvAllinternationalLicences.DataSource = dv;
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsApplication application = clsApplication.FindApplicationByID((int)dgvAllinternationalLicences.CurrentRow.Cells[1].Value);
            FrmShowDetails frmShowDetails = new FrmShowDetails(application.ApplicantPersonID);
            frmShowDetails.ShowDialog();
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInternationalLicenseInfo frmInternationalLicenseInfo = new frmInternationalLicenseInfo((int)dgvAllinternationalLicences.CurrentRow.Cells[0].Value);
            frmInternationalLicenseInfo.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsApplication application = clsApplication.FindApplicationByID((int)dgvAllinternationalLicences.CurrentRow.Cells[1].Value);
            clsPerson person = clsPerson.FindPersonByID(application.ApplicantPersonID);

            frmLicenseHistory frmLicenseHistory = new frmLicenseHistory(person.NationalNo);
            frmLicenseHistory.ShowDialog();
        }
    }
}
