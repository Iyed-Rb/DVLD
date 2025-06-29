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
    public partial class ctrlUserInfo : UserControl
    {
        public int _UserID = -1;
        clsUser _User;

        public ctrlUserInfo()
        {
            InitializeComponent();
        }

        public void LoadUserDataByID(int UserID)
        {
            _UserID = UserID;
            _User = clsUser.FindUserByID(_UserID);
            

      
            if (_User == null)
            {
                MessageBox.Show("User not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ctrlPersonInformation1.LoadDataByPersonID(_User.PersonID);

            lbUserID.Text = _UserID.ToString();
            lbUsername.Text = _User.UserName;
            lbIsActive.Text = _User.IsActive ? "Yes" : "No";

            this.AutoValidate = AutoValidate.EnableAllowFocusChange;
        }

        private void ctrlUserInfo_Load(object sender, EventArgs e)
        {

            //ctrlPersonInformation1.LoadDataByPersonID(_User.PersonID);

            //_User = clsUser.FindUserByID(_UserID);
            //if (_User == null)
            //{
            //    MessageBox.Show("User not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            //ctrlPersonInformation1.LoadDataByPersonID(_User.PersonID);

            //lbUserID.Text = _UserID.ToString();
            //lbUsername.Text = _User.UserName;
            //lbIsActive.Text = _User.IsActive ? "Yes" : "No";

            //this.AutoValidate = AutoValidate.EnableAllowFocusChange;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
