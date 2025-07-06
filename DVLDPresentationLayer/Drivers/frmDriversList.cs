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

namespace DVLDPresentationLayer
{
    public partial class frmDriversList : Form
    {
        public frmDriversList()
        {
            InitializeComponent();
        }

        DataTable _dtDrivers;

        private void _RefreshUsersList()
        {
            _dtDrivers = clsDriver.GetAllDriversList();
            dgvAllDrivers.DataSource = _dtDrivers;
            int rowCount = dgvAllDrivers.Rows.Count;
            lbCountRow.Text = rowCount.ToString();

            if (dgvAllDrivers.Columns.Contains("Driver ID"))
                dgvAllDrivers.Columns["Driver ID"].Width = 85;

            if (dgvAllDrivers.Columns.Contains("Person ID"))
                dgvAllDrivers.Columns["Person ID"].Width = 85;

            if (dgvAllDrivers.Columns.Contains("National No"))
                dgvAllDrivers.Columns["National No"].Width = 85;

            if (dgvAllDrivers.Columns.Contains("Full Name"))
                dgvAllDrivers.Columns["Full Name"].Width = 220;

            if (dgvAllDrivers.Columns.Contains("Date"))
                dgvAllDrivers.Columns["Date"].Width = 100;

            if (dgvAllDrivers.Columns.Contains("Active Licenses"))
                dgvAllDrivers.Columns["Active Licenses"].Width = 85;
        }
        private void frmDriversList_Load(object sender, EventArgs e)
        {
            _RefreshUsersList();
            textBox1.Visible = false;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.SelectedIndex = 0;


            dgvAllDrivers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvAllDrivers.AllowUserToAddRows = false;
            dgvAllDrivers.ReadOnly = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != 0)
            {
                textBox1.Visible = true;
            }
            else
            {
                textBox1.Visible = false;
                textBox1.Text = "";


                DataView dv = _dtDrivers.DefaultView;
                dv.RowFilter = ""; // Clear filter
                dgvAllDrivers.DataSource = dv;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (_dtDrivers == null || comboBox1.SelectedIndex == 0)
                return;

            string SelectedColumn = "";

            switch (comboBox1.SelectedIndex)
            {
                case 1: SelectedColumn = "DriverID"; break;
                case 2: SelectedColumn = "PersonID"; break;
                case 3: SelectedColumn = "NationalNo"; break;
                case 4: SelectedColumn = "FullName"; break;
                default: return;
            }

            string keyword = textBox1.Text.Replace("'", "''"); // Escape single quotes

            try
            {
                DataView dv = _dtDrivers.DefaultView;

                if (SelectedColumn == "PersonID" || SelectedColumn == "DriverID")
                {
                    if (int.TryParse(textBox1.Text, out int id))
                        dv.RowFilter = $"[{SelectedColumn}] = {id}";
                    else
                        dv.RowFilter = "1 = 0"; // Invalid input, show no results
                }
                else
                {
                    //dv.RowFilter = $"[{SelectedColumn}] LIKE '%{keyword}%'";
                    dv.RowFilter = $"[{SelectedColumn}] = '{keyword}'";
                }


                dgvAllDrivers.DataSource = dv;
            }
            catch (Exception ex)
            {
                // You can log the error or show it
                // MessageBox.Show("Filter error: " + ex.Message);
            }
        }
    }
}
