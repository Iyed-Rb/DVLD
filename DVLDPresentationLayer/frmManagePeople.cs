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
    public partial class frmManagePeople : Form
    {
        public frmManagePeople()
        {
            InitializeComponent();
        }


        DataTable _dtPeople;

        private void _RefreshPeopleList()
        {
            _dtPeople = clsPerson.GetAllPeople();
            dgvAllPeople.DataSource = _dtPeople;
            int rowCount = dgvAllPeople.Rows.Count;
            lbCountRow.Text = rowCount.ToString();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvAllPeople.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }


        private void frmManagePeople_Load(object sender, EventArgs e)
        {
            _RefreshPeopleList();
            textBox1.Visible = false;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.SelectedIndex = 0;

            dgvAllPeople.ClearSelection();
            dgvAllPeople.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        }

      
            //KeyPressEventArgs
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (_dtPeople == null || comboBox1.SelectedIndex == 0)
                return;

            string SelectedColumn = "";

            switch (comboBox1.SelectedIndex)
            {
                case 1: SelectedColumn = "PersonID"; break;
                case 2: SelectedColumn = "NationalNo"; break;
                case 3: SelectedColumn = "FirstName"; break;
                case 4: SelectedColumn = "SecondName"; break;
                case 5: SelectedColumn = "ThirdName"; break;
                case 6: SelectedColumn = "LastName"; break;
                case 7: SelectedColumn = "Gendor"; break;
                case 8: SelectedColumn = "CountryName"; break;
                case 9: SelectedColumn = "Phone"; break;
                case 10: SelectedColumn = "Email"; break;
                default: return;
            }

            string keyword = textBox1.Text.Replace("'", "''"); // Escape single quotes

            try
            {
                DataView dv = _dtPeople.DefaultView;

                if (SelectedColumn == "PersonID")
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


                dgvAllPeople.DataSource = dv;
            }
            catch (Exception ex)
            {
                // You can log the error or show it
                // MessageBox.Show("Filter error: " + ex.Message);
            }
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

                
                DataView dv = _dtPeople.DefaultView;
                dv.RowFilter = ""; // Clear filter
                dgvAllPeople.DataSource = dv;
            }
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAddEditPerson formAddEdit = new frmAddEditPerson(-1);
            formAddEdit.ShowDialog();

            _RefreshPeopleList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frmEdit = new frmAddEditPerson((int)dgvAllPeople.CurrentRow.Cells[0].Value);
            frmEdit.ShowDialog();

            _RefreshPeopleList();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditPerson formAdd = new frmAddEditPerson(-1);
            formAdd.ShowDialog();

            _RefreshPeopleList();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (comboBox1.SelectedIndex == 1) // PersonID
            {
                // Only allow digits and control keys (like Backspace)
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true; // Block input
                }
            }
        }
    }
}

