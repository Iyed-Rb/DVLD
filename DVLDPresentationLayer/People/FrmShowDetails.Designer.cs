namespace DVLDPresentationLayer
{
    partial class FrmShowDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmShowDetails));
            this.label1 = new System.Windows.Forms.Label();
            this.btCLose = new System.Windows.Forms.Button();
            this.ctrlPersonInformation1 = new DVLDPresentationLayer.ctrlPersonInformation();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(275, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(205, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Person Details";
            // 
            // btCLose
            // 
            this.btCLose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btCLose.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCLose.Image = ((System.Drawing.Image)(resources.GetObject("btCLose.Image")));
            this.btCLose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btCLose.Location = new System.Drawing.Point(601, 374);
            this.btCLose.Name = "btCLose";
            this.btCLose.Size = new System.Drawing.Size(105, 32);
            this.btCLose.TabIndex = 51;
            this.btCLose.Text = "        Close";
            this.btCLose.UseVisualStyleBackColor = true;
            this.btCLose.Click += new System.EventHandler(this.btCLose_Click);
            // 
            // ctrlPersonInformation1
            // 
            this.ctrlPersonInformation1.Location = new System.Drawing.Point(2, 52);
            this.ctrlPersonInformation1.Name = "ctrlPersonInformation1";
            this.ctrlPersonInformation1.Size = new System.Drawing.Size(763, 316);
            this.ctrlPersonInformation1.TabIndex = 52;
            // 
            // FrmShowDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(777, 418);
            this.Controls.Add(this.ctrlPersonInformation1);
            this.Controls.Add(this.btCLose);
            this.Controls.Add(this.label1);
            this.Name = "FrmShowDetails";
            this.Text = "FrmShowDetails";
            this.Load += new System.EventHandler(this.FrmShowDetails_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btCLose;
        private ctrlPersonInformation ctrlPersonInformation1;
    }
}