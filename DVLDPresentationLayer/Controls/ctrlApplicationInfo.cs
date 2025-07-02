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

namespace DVLDPresentationLayer.Controls
{
    public partial class ctrlApplicationInfo : UserControl
    {
        public ctrlApplicationInfo()
        {
            InitializeComponent();
        }
        private void ctrlInfo_Load(object sender, EventArgs e)
        {

        }

        clsLDLApplication _LDLApplication;
        clsPerson _Person;
        public void FillCtrlInformation(int LDLApplicationID)
        {
            _LDLApplication = clsLDLApplication.FindLDLApplicationByID(LDLApplicationID);
            if (_LDLApplication == null)
            {
                lbDLAppID.Text = "[???]";
                lbClassName.Text = "[???]";
                lbPassedTests.Text = "[???]";
                lbApplicationID.Text = "[???]";
                lbStatus.Text = "[???]";
                lbFees.Text = "[???]";
                lbType.Text = "[???]";
                lbPersonFullName.Text = "[???]"; 
                lb15.Text = "[???]";
                lbLastStatus.Text = "[???]";
                lb17.Text = "[???]";

                MessageBox.Show("The selected Local Driving License Application was not found.",
                    "Application Not Found",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            clsApplicationTypes applicationTypes = clsApplicationTypes.FindApplicationTypeByID(_LDLApplication.Application.ApplicationTypeID);
            lbDLAppID.Text = _LDLApplication.LDLApplicationID.ToString();
            lbClassName.Text = _LDLApplication.LicenseClass.ClassName;
            lbPassedTests.Text = clsLDLApplication.GetPassedTestsCount(_LDLApplication.LDLApplicationID).ToString();
            lbApplicationID.Text = _LDLApplication.Application.ApplicationID.ToString();
            int status = _LDLApplication.Application.ApplicationStatus;
            lbStatus.Text = status == 1 ? "New" :
                            status == 2 ? "Cancelled" :
                            status == 3 ? "Completed" :
                            "Unknown";

            lbFees.Text = applicationTypes.ApplicationTypeFees.ToString();
            lbType.Text = applicationTypes.ApplicationTypeTitle.ToString();
            _Person = clsPerson.FindPersonByID(_LDLApplication.Application.ApplicantPersonID);
            lbPersonFullName.Text = _Person.FirstName + " " +  _Person.SecondName + " " + _Person.ThirdName + " " + _Person.LastName;
            lbDate.Text = _LDLApplication.Application.ApplicationDate.ToShortDateString();
            lbLastStatus.Text = _LDLApplication.Application.LastStatusDate.ToShortDateString();
            clsUser User = clsUser.FindUserByID(_LDLApplication.Application.CreatedByUserID);
            lbCreatedByUserID.Text = User.UserName;



        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void linkLabelPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmShowDetails frm = new FrmShowDetails(_Person.PersonID);
        }
    }
}
