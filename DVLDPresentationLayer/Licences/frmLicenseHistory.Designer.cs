namespace DVLDPresentationLayer
{
    partial class frmLicenseHistory
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLicenseHistory));
            this.label1 = new System.Windows.Forms.Label();
            this.driverLicenses1 = new DVLDPresentationLayer.DriverLicenses();
            this.personCardWithFilter1 = new DVLDPresentationLayer.PersonCardWithFilter();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(333, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(216, 31);
            this.label1.TabIndex = 2;
            this.label1.Text = "License History";
            // 
            // driverLicenses1
            // 
            this.driverLicenses1.Location = new System.Drawing.Point(12, 380);
            this.driverLicenses1.Name = "driverLicenses1";
            this.driverLicenses1.Size = new System.Drawing.Size(1029, 261);
            this.driverLicenses1.TabIndex = 4;
            // 
            // personCardWithFilter1
            // 
            this.personCardWithFilter1.Location = new System.Drawing.Point(269, 41);
            this.personCardWithFilter1.Name = "personCardWithFilter1";
            this.personCardWithFilter1.SelectedPersonID = 0;
            this.personCardWithFilter1.Size = new System.Drawing.Size(772, 333);
            this.personCardWithFilter1.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(39, 124);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(213, 250);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // frmLicenseHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1047, 634);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.driverLicenses1);
            this.Controls.Add(this.personCardWithFilter1);
            this.Controls.Add(this.label1);
            this.Name = "frmLicenseHistory";
            this.Text = "frmLicenseHistory";
            this.Load += new System.EventHandler(this.frmLicenseHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private PersonCardWithFilter personCardWithFilter1;
        private DriverLicenses driverLicenses1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}