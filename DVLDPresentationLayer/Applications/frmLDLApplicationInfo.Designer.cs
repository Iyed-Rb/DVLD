namespace DVLDPresentationLayer
{
    partial class frmLDLApplicationInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLDLApplicationInfo));
            this.ctrlApplicationInfo1 = new DVLDPresentationLayer.Controls.ctrlApplicationInfo();
            this.btCLose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ctrlApplicationInfo1
            // 
            this.ctrlApplicationInfo1.Location = new System.Drawing.Point(12, 21);
            this.ctrlApplicationInfo1.Name = "ctrlApplicationInfo1";
            this.ctrlApplicationInfo1.Size = new System.Drawing.Size(656, 272);
            this.ctrlApplicationInfo1.TabIndex = 0;
            // 
            // btCLose
            // 
            this.btCLose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btCLose.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCLose.Image = ((System.Drawing.Image)(resources.GetObject("btCLose.Image")));
            this.btCLose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btCLose.Location = new System.Drawing.Point(537, 299);
            this.btCLose.Name = "btCLose";
            this.btCLose.Size = new System.Drawing.Size(105, 32);
            this.btCLose.TabIndex = 35;
            this.btCLose.Text = "        Close";
            this.btCLose.UseVisualStyleBackColor = true;
            // 
            // frmLDLApplicationInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(693, 363);
            this.Controls.Add(this.btCLose);
            this.Controls.Add(this.ctrlApplicationInfo1);
            this.Name = "frmLDLApplicationInfo";
            this.Text = "Local Driving License Application Info";
            this.Load += new System.EventHandler(this.frmLDLApplicationInfo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ctrlApplicationInfo ctrlApplicationInfo1;
        private System.Windows.Forms.Button btCLose;
    }
}