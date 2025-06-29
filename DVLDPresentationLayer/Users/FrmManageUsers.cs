using DVLDBusinessLayer;
using DVLDPresentationLayer.Users;
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
    public partial class FrmManageUsers : Form
    {
        public FrmManageUsers()
        {
            InitializeComponent();
        }


        DataTable _dtUsers;

        private void _RefreshUsersList()
        {
            _dtUsers = clsUser.GetAllUsers();
            dgvAllUsers.DataSource = _dtUsers;
            int rowCount = dgvAllUsers.Rows.Count;
            lbCountRow.Text = rowCount.ToString();

            dgvAllUsers.Columns["UserID"].Width = 85;
            dgvAllUsers.Columns["PersonID"].Width = 85;
            dgvAllUsers.Columns["FullName"].Width = 220;
            dgvAllUsers.Columns["Username"].Width = 100;
            dgvAllUsers.Columns["IsActive"].Width = 85;
        }
        private void FrmManageUsers_Load(object sender, EventArgs e)
        {
            comboBox2.Visible = false;
            _RefreshUsersList();
            textBox1.Visible = false;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.SelectedIndex = 0;

       
            dgvAllUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvAllUsers.AllowUserToAddRows = false;
            dgvAllUsers.ReadOnly = true;
        }

        private void btCLose_Click(object sender, EventArgs e)
        {
            this.Close();
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

                DataView dv = _dtUsers.DefaultView;
                dv.RowFilter = ""; // Clear filter
                dgvAllUsers.DataSource = dv;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (_dtUsers == null || comboBox1.SelectedIndex == 0)
                return;

            string SelectedColumn = "";

            switch (comboBox1.SelectedIndex)
            {
                case 1: SelectedColumn = "UserID"; break;
                case 2: SelectedColumn = "Username"; break;
                case 3: SelectedColumn = "PersonID"; break;
                case 4: SelectedColumn = "FullName"; break;
                case 5: SelectedColumn = "IsActive"; break;

                default: return;
            }

            string keyword = textBox1.Text.Replace("'", "''"); // Escape single quotes

            try
            {
                DataView dv = _dtUsers.DefaultView;

                if (SelectedColumn == "PersonID" || SelectedColumn == "UserID")
                {
                    if (int.TryParse(textBox1.Text, out int id))
                        dv.RowFilter = $"[{SelectedColumn}] = {id}";
                    else
                        dv.RowFilter = "1 = 0"; // Invalid input, show no results
                }
                else if (SelectedColumn == "FullName" || SelectedColumn == "Username")
                {
                    dv.RowFilter = $"[{SelectedColumn}] LIKE '%{keyword}%'";
                    //dv.RowFilter = $"[{SelectedColumn}] = '{keyword}'";
                }

                dgvAllUsers.DataSource = dv;
            }
            catch (Exception ex)
            {
                // You can log the error or show it
                // MessageBox.Show("Filter error: " + ex.Message);
            }
        }

        private void pbAddUser_Click(object sender, EventArgs e)
        {
            frmAddEditUser frmAddEditUser = new frmAddEditUser();
            frmAddEditUser.ShowDialog();

            _RefreshUsersList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditUser frmEdit = new frmAddEditUser((int)dgvAllUsers.CurrentRow.Cells[0].Value);
            frmEdit.ShowDialog();

            _RefreshUsersList();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditUser frmAddEditUser = new frmAddEditUser();
            frmAddEditUser.ShowDialog();

            _RefreshUsersList();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvAllUsers.CurrentRow == null)
            {
                MessageBox.Show("Please select a Valid User to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int UserID = (int)dgvAllUsers.CurrentRow.Cells[0].Value;
            clsUser User = clsUser.FindUserByID(UserID);

            if (User == null)
            {
                MessageBox.Show("This User does not exist in the system.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete User[" + UserID + "]", "Confirm Deletion",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {


                // Delete person from database
                if (clsUser.DeleteUser(UserID))
                {

                    MessageBox.Show("User deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshUsersList();
                }
                else
                {
                    MessageBox.Show("User was not Deleleted because is has data linked it to.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frmChangePassword1 = new frmChangePassword((int)dgvAllUsers.CurrentRow.Cells[0].Value);
            frmChangePassword1.ShowDialog();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataView dv = _dtUsers.DefaultView;

            switch (comboBox2.SelectedIndex)
            {
                case 0: // All
                    dv.RowFilter = ""; // No filter
                    break;
                case 1: // Active Only
                    dv.RowFilter = "[IsActive] = true";
                    break;
                case 2: // Inactive Only
                    dv.RowFilter = "[IsActive] = false";
                    break;
            }

            dgvAllUsers.DataSource = dv;
        }


        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo frmUserInfo = new frmUserInfo((int)dgvAllUsers.CurrentRow.Cells[0].Value);
            frmUserInfo.ShowDialog();
        }
    }

}
