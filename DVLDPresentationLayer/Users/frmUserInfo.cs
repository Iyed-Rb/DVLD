﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDPresentationLayer.Users
{
    public partial class frmUserInfo : Form
    {
        private int _UserID = -1;
        public frmUserInfo(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
        }

        private void ctrlUserInfo1_Load(object sender, EventArgs e)
        {
            ctrlUserInfo1.LoadUserDataByID(_UserID);
        }
    }
}
