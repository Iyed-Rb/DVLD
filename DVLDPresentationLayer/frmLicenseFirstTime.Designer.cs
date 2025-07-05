namespace DVLDPresentationLayer
{
    partial class frmLicenseFirstTime
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLicenseFirstTime));
            this.ctrlApplicationInfo1 = new DVLDPresentationLayer.Controls.ctrlApplicationInfo();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox9 = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btCLose = new System.Windows.Forms.Button();
            this.btIssue = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
            this.SuspendLayout();
            // 
            // ctrlApplicationInfo1
            // 
            this.ctrlApplicationInfo1.Location = new System.Drawing.Point(12, 23);
            this.ctrlApplicationInfo1.Name = "ctrlApplicationInfo1";
            this.ctrlApplicationInfo1.Size = new System.Drawing.Size(656, 272);
            this.ctrlApplicationInfo1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(24, 298);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Notes:";
            // 
            // pictureBox9
            // 
            this.pictureBox9.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox9.Image")));
            this.pictureBox9.Location = new System.Drawing.Point(88, 301);
            this.pictureBox9.Name = "pictureBox9";
            this.pictureBox9.Size = new System.Drawing.Size(22, 22);
            this.pictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox9.TabIndex = 95;
            this.pictureBox9.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(116, 301);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(546, 104);
            this.textBox1.TabIndex = 96;
            // 
            // btCLose
            // 
            this.btCLose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btCLose.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCLose.Image = ((System.Drawing.Image)(resources.GetObject("btCLose.Image")));
            this.btCLose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btCLose.Location = new System.Drawing.Point(436, 411);
            this.btCLose.Name = "btCLose";
            this.btCLose.Size = new System.Drawing.Size(105, 32);
            this.btCLose.TabIndex = 97;
            this.btCLose.Text = "        Close";
            this.btCLose.UseVisualStyleBackColor = true;
            this.btCLose.Click += new System.EventHandler(this.btCLose_Click);
            // 
            // btIssue
            // 
            this.btIssue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btIssue.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btIssue.Image = ((System.Drawing.Image)(resources.GetObject("btIssue.Image")));
            this.btIssue.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btIssue.Location = new System.Drawing.Point(557, 411);
            this.btIssue.Name = "btIssue";
            this.btIssue.Size = new System.Drawing.Size(105, 32);
            this.btIssue.TabIndex = 98;
            this.btIssue.Text = "        Issue";
            this.btIssue.UseVisualStyleBackColor = true;
            this.btIssue.Click += new System.EventHandler(this.btIssue_Click);
            // 
            // frmLicenseFirstTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(687, 456);
            this.Controls.Add(this.btIssue);
            this.Controls.Add(this.btCLose);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.pictureBox9);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ctrlApplicationInfo1);
            this.Name = "frmLicenseFirstTime";
            this.Text = "Issue Driver License For the First Time";
            this.Load += new System.EventHandler(this.frmLicenseFirstTime_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.ctrlApplicationInfo ctrlApplicationInfo1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox9;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btCLose;
        private System.Windows.Forms.Button btIssue;
    }
}