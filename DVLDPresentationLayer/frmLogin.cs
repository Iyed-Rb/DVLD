using DVLDBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDPresentationLayer
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        string loginFilePath = Path.Combine(Application.StartupPath, "userlogin.txt");

        private void PerformClick()
        {
            if (!string.IsNullOrEmpty(textBox1.Text) || !string.IsNullOrEmpty(textBox2.Text))
            {
                if (clsUser.FindUserByUserNameAndPassword(textBox1.Text, textBox2.Text))
                {

                   if (clsUser.UserIsActive(textBox1.Text) == false)
                   {
                       MessageBox.Show("Your account is inactive", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                       return;
                   }
                   else
                   {

                        clsGlobalSettings.CurrentUser = clsUser.Find(textBox1.Text);

                        if (checkBox1.Checked == true)
                       {
                           File.WriteAllText(loginFilePath, $"{textBox1.Text}\n{textBox2.Text}");
                           this.DialogResult = DialogResult.OK;
                       }
                       else
                       {
                           File.WriteAllText(loginFilePath, string.Empty); // just clear file
                            textBox1.Text = string.Empty; // Clear username field
                            textBox2.Text = string.Empty; // Clear password field
                        } 


                        frmMain mainForm = new frmMain();
                        mainForm.Show();
                   }
                    
                }
                else
                {
                    MessageBox.Show("Invalid username or password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Please enter your username and password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        private void btLogin_Click(object sender, EventArgs e)
        {
          PerformClick();

        }

        public void LoadLoginData()
        {
            if (File.Exists(loginFilePath))
            {
                string[] lines = File.ReadAllLines(loginFilePath);
                if (lines.Length >= 2 && !string.IsNullOrWhiteSpace(lines[0]) && !string.IsNullOrWhiteSpace(lines[1]))
                {
                    textBox1.Text = lines[0];
                    textBox2.Text = lines[1];
                    checkBox1.Checked = true;
                }
            }
            else
            {
                textBox1.Text = string.Empty; // Clear username field
                textBox2.Text = string.Empty; // Clear password field
            }
        }


        private void frmLogin_Load(object sender, EventArgs e)
        {
            LoadLoginData(); // Load saved login data if available
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent beep sound
                textBox2.Focus();          // move focus to password field
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btLogin.PerformClick();  // simulate button click
            }
        }
    }
}
