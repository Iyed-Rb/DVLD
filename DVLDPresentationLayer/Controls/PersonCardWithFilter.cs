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
    public partial class PersonCardWithFilter : UserControl
    {
        public PersonCardWithFilter()
        {
            InitializeComponent();
        }

        public int SelectedPersonID
        {
            get { return ctrlPersonInformation1._PersonID; }
            set
            {
                ctrlPersonInformation1._PersonID = value;
                ctrlPersonInformation1.LoadDataByPersonID(value);
            }
        }

        public void LoadPersonDataByID(int PersonID)
        {
            ctrlPersonInformation1._PersonID = PersonID;
            ctrlPersonInformation1.LoadDataByPersonID(ctrlPersonInformation1._PersonID);

        }

        public void LoadPersonDataByNationalNo(string NationalNo)
        {
            ctrlPersonInformation1._NationalNo = NationalNo;
            ctrlPersonInformation1.LoadDataByNationalNo(ctrlPersonInformation1._NationalNo);

        }


        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (comboBox1.SelectedIndex == 0) // PersonID
            {
                // Only allow digits and control keys (like Backspace)
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true; // Block input
                }
            }
        }


        private void btSearchPerson_Click(object sender, EventArgs e)
        {

            if (comboBox1.SelectedIndex == 0) // Search by PersonID
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Please enter a valid Person ID.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(textBox1.Text, out int personID))
                {
                    MessageBox.Show("Person ID must be a number.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //ctrlPersonInformation1.LoadDataByPersonID(personID);
                SelectedPersonID = personID;
            }
            else if (comboBox1.SelectedIndex == 1) // Search by National No
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Please enter a National Number.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ctrlPersonInformation1.LoadDataByNationalNo(textBox1.Text);
                SelectedPersonID = ctrlPersonInformation1._PersonID;
            }

        }
        
        private void ctrlPersonInformation1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0; // Default to PersonID search
        }

        private void btAddPerson_Click(object sender, EventArgs e)
        {
     
            frmAddEditPerson frmAddEditPerson = new frmAddEditPerson();

            frmAddEditPerson.DataBack += GetPersonID; // Subscribe to the event

            frmAddEditPerson.ShowDialog();
        }

        private void GetPersonID(object sender, int PersonID)
        {
            ctrlPersonInformation1._PersonID = PersonID;
            ctrlPersonInformation1.LoadDataByPersonID(ctrlPersonInformation1._PersonID);
        }
    }
}
