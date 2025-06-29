using DVLDBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDPresentationLayer
{
    public partial class AddEditLDLApplications : Form
    {

        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;

        clsPerson _Person;
        int _PersonID;

        int _LDLApplicationID;
        clsLDLApplication _LDLApplication;

        clsApplicationTypes _ApplicationType;

        public AddEditLDLApplications()
        {
            InitializeComponent();

            _Mode = enMode.AddNew;
        }

        public AddEditLDLApplications(int LDLApplicationID)
        {
            InitializeComponent();
            _LDLApplicationID = LDLApplicationID;

            if (_LDLApplicationID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;
        }

        private void AddEditLDLApplications_Load(object sender, EventArgs e)
        {
            _LoadData();
        }


        private void _FillLicenseClassesInComoboBox()
        {
      
            DataTable dtLicenseClasses = clsLicenseClass.GetAllLicenseClasses();

            foreach (DataRow row in dtLicenseClasses.Rows)
            {

                cbLicenseClass.Items.Add(row["ClassName"]);

            }

        }

        private void _LoadData()
        {
            _ApplicationType = clsApplicationTypes.FindApplicationTypeByID(1);//first type is AddNewLDLApplication
            if (_ApplicationType == null)
            {
                MessageBox.Show("No ApplicationType Found with ID = " + 1, "ApplicationType Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            lbApplicationFees.Text = _ApplicationType.ApplicationTypeFees.ToString();
            lbCreatedByUser.Text = clsGlobalSettings.CurrentUser.UserName;
            _FillLicenseClassesInComoboBox();
            if (_Mode == enMode.AddNew)
            {
                tbApplicationInfo.Enabled = false;
                personCardWithFilter1.groupBox1.Enabled = true;
                lbMode.Text = "New Local Driving License Application";
                _LDLApplication = new clsLDLApplication();

                lbLDLApplicationID.Text = "[????] ";
                lbApplicationDate.Text = lbApplicationDate.Text = DateTime.Now.ToShortDateString();
                cbLicenseClass.SelectedIndex = 1;
             
                return;
            }
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

            _LDLApplication = clsLDLApplication.FindLDLApplicationByID(_LDLApplicationID);
      

            if (_LDLApplication == null)
            {
                MessageBox.Show("No LDLApplication with ID = " + _LDLApplicationID, "LDLApplication Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }
           

            //personCardWithFilter1.LoadPersonDataByID(_Application.ApplicantPersonID);
            personCardWithFilter1.SelectedPersonID = _LDLApplication.Application.ApplicantPersonID;
            _PersonID = _LDLApplication.Application.ApplicantPersonID;
            _Person = clsPerson.FindPersonByID(_PersonID);
            

            tbApplicationInfo.Enabled = true;
            tbPersonalInfo.Enabled = true;
            btSave.Enabled = true;

      
            personCardWithFilter1.groupBox1.Enabled = false;


            lbMode.Text = "Update Local Driving License Application";

            lbLDLApplicationID.Text = _LDLApplication.ApplicationID.ToString();
            lbApplicationDate.Text = _LDLApplication.Application.ApplicationDate.ToShortDateString();
            cbLicenseClass.SelectedIndex = cbLicenseClass.FindString(_LDLApplication.LicenseClass.ClassName);

        }

        private void btCLose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void personCardWithFilter1_Load(object sender, EventArgs e)
        {

        }

        private void btNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                btSave.Enabled = true;
                tbApplicationInfo.Enabled = true;
                tabControl1.SelectedTab = tabControl1.TabPages["tbApplicationInfo"];
                return;
            }

            _PersonID = personCardWithFilter1.SelectedPersonID;
            _Person = clsPerson.FindPersonByID(_PersonID);

            if (_PersonID == -1 || _Person == null)
            {
                MessageBox.Show("Please select a valid person first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            btSave.Enabled = true;
            tbApplicationInfo.Enabled = true;
            tabControl1.SelectedTab = tabControl1.TabPages["tbApplicationInfo"];
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            int existingAppID = clsApplication.HasSameApplicationBefore(_PersonID, cbLicenseClass.SelectedIndex + 1);
            if (existingAppID != -1) // i put the validation here
            {

                MessageBox.Show(@"Choose Another License Class, the selected Person Already have an active application for the 
                     selected class with ID = " + existingAppID ,
                    
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);// here i need to display the ApplicationID
            }
        }
    }
}
