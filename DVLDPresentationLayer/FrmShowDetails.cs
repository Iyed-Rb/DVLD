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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace DVLDPresentationLayer
{
    public partial class FrmShowDetails : Form
    {
        int _PersonID;

        public FrmShowDetails(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
            
        }


        private void btCLose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmShowDetails_Load(object sender, EventArgs e)
        {
            ctrlPersonInformation1.LoadData(_PersonID);
        }

    }
}
