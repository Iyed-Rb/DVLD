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
    public partial class frmLicenseHistory : Form
    {
        string _NationalNo;
        public frmLicenseHistory(string NationalNo)
        {
            InitializeComponent();
            _NationalNo = NationalNo;
        }

        private void frmLicenseHistory_Load(object sender, EventArgs e)
        {
            clsPerson person = clsPerson.FindPersonByNationalNO(_NationalNo);

            driverLicenses1.FillData(person.PersonID);
            personCardWithFilter1.groupBox1.Enabled = false;
            personCardWithFilter1.LoadPersonDataByID(person.PersonID);
        }
    }
}
