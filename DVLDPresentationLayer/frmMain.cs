using DVLDBusinessLayer;
using DVLDPresentationLayer.Applications;
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
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManagePeople formManagePeople  = new frmManagePeople();
            formManagePeople.ShowDialog();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
           

        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmManageUsers FormManageUsers = new FrmManageUsers();
            FormManageUsers.ShowDialog();

        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo frmUserInfo = new frmUserInfo(clsGlobalSettings.CurrentUser.UserID);
            frmUserInfo.ShowDialog();
        }

        private void changeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frmChangePassword = new frmChangePassword(clsGlobalSettings.CurrentUser.UserID);
            frmChangePassword.ShowDialog();
        }

        private void manageApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void managesTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageTestTypes frmManageTestTypes = new frmManageTestTypes();
            frmManageTestTypes.ShowDialog();
  
        }

        private void localDrivingLicenceApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageLDLApplications frm = new frmManageLDLApplications();
            frm.ShowDialog();
        }
    }
}
