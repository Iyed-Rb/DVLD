namespace DVLDPresentationLayer
{
    partial class DriverLicenses
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbLocal = new System.Windows.Forms.TabPage();
            this.lbCountRow = new System.Windows.Forms.Label();
            this.lbj = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvLocalLicenses = new System.Windows.Forms.DataGridView();
            this.tbInternational = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvAllInternationalLicenses = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.lbCountRow2 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tbLocal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalLicenses)).BeginInit();
            this.tbInternational.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllInternationalLicenses)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tbLocal);
            this.tabControl1.Controls.Add(this.tbInternational);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(6, 23);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1010, 207);
            this.tabControl1.TabIndex = 0;
            // 
            // tbLocal
            // 
            this.tbLocal.Controls.Add(this.lbCountRow);
            this.tbLocal.Controls.Add(this.lbj);
            this.tbLocal.Controls.Add(this.label1);
            this.tbLocal.Controls.Add(this.dgvLocalLicenses);
            this.tbLocal.Location = new System.Drawing.Point(4, 27);
            this.tbLocal.Name = "tbLocal";
            this.tbLocal.Padding = new System.Windows.Forms.Padding(3);
            this.tbLocal.Size = new System.Drawing.Size(1002, 176);
            this.tbLocal.TabIndex = 0;
            this.tbLocal.Text = "Local";
            this.tbLocal.UseVisualStyleBackColor = true;
            // 
            // lbCountRow
            // 
            this.lbCountRow.AutoSize = true;
            this.lbCountRow.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCountRow.Location = new System.Drawing.Point(103, 148);
            this.lbCountRow.Name = "lbCountRow";
            this.lbCountRow.Size = new System.Drawing.Size(17, 18);
            this.lbCountRow.TabIndex = 12;
            this.lbCountRow.Text = "0";
            // 
            // lbj
            // 
            this.lbj.AutoSize = true;
            this.lbj.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbj.Location = new System.Drawing.Point(6, 148);
            this.lbj.Name = "lbj";
            this.lbj.Size = new System.Drawing.Size(91, 18);
            this.lbj.TabIndex = 8;
            this.lbj.Text = "# Records:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(190, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Local Licenses History: ";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // dgvLocalLicenses
            // 
            this.dgvLocalLicenses.AllowUserToAddRows = false;
            this.dgvLocalLicenses.AllowUserToDeleteRows = false;
            this.dgvLocalLicenses.AllowUserToResizeColumns = false;
            this.dgvLocalLicenses.AllowUserToResizeRows = false;
            this.dgvLocalLicenses.BackgroundColor = System.Drawing.Color.White;
            this.dgvLocalLicenses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLocalLicenses.Location = new System.Drawing.Point(12, 33);
            this.dgvLocalLicenses.Name = "dgvLocalLicenses";
            this.dgvLocalLicenses.Size = new System.Drawing.Size(963, 112);
            this.dgvLocalLicenses.TabIndex = 0;
            this.dgvLocalLicenses.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLocalLicenses_CellContentClick);
            // 
            // tbInternational
            // 
            this.tbInternational.Controls.Add(this.lbCountRow2);
            this.tbInternational.Controls.Add(this.label3);
            this.tbInternational.Controls.Add(this.dgvAllInternationalLicenses);
            this.tbInternational.Controls.Add(this.label2);
            this.tbInternational.Location = new System.Drawing.Point(4, 27);
            this.tbInternational.Name = "tbInternational";
            this.tbInternational.Padding = new System.Windows.Forms.Padding(3);
            this.tbInternational.Size = new System.Drawing.Size(1002, 176);
            this.tbInternational.TabIndex = 1;
            this.tbInternational.Text = "International";
            this.tbInternational.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tabControl1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1022, 240);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Driver Licenses";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(241, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "International Licenses History: ";
            // 
            // dgvAllInternationalLicenses
            // 
            this.dgvAllInternationalLicenses.AllowUserToAddRows = false;
            this.dgvAllInternationalLicenses.AllowUserToDeleteRows = false;
            this.dgvAllInternationalLicenses.AllowUserToResizeColumns = false;
            this.dgvAllInternationalLicenses.AllowUserToResizeRows = false;
            this.dgvAllInternationalLicenses.BackgroundColor = System.Drawing.Color.White;
            this.dgvAllInternationalLicenses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAllInternationalLicenses.Location = new System.Drawing.Point(12, 33);
            this.dgvAllInternationalLicenses.Name = "dgvAllInternationalLicenses";
            this.dgvAllInternationalLicenses.Size = new System.Drawing.Size(963, 112);
            this.dgvAllInternationalLicenses.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 148);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 18);
            this.label3.TabIndex = 9;
            this.label3.Text = "# Records:";
            // 
            // lbCountRow2
            // 
            this.lbCountRow2.AutoSize = true;
            this.lbCountRow2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCountRow2.Location = new System.Drawing.Point(103, 148);
            this.lbCountRow2.Name = "lbCountRow2";
            this.lbCountRow2.Size = new System.Drawing.Size(17, 18);
            this.lbCountRow2.TabIndex = 13;
            this.lbCountRow2.Text = "0";
            // 
            // DriverLicenses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "DriverLicenses";
            this.Size = new System.Drawing.Size(1038, 252);
            this.Load += new System.EventHandler(this.DriverLicenses_Load);
            this.tabControl1.ResumeLayout(false);
            this.tbLocal.ResumeLayout(false);
            this.tbLocal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalLicenses)).EndInit();
            this.tbInternational.ResumeLayout(false);
            this.tbInternational.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllInternationalLicenses)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbLocal;
        private System.Windows.Forms.TabPage tbInternational;
        private System.Windows.Forms.DataGridView dgvLocalLicenses;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbj;
        private System.Windows.Forms.Label lbCountRow;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvAllInternationalLicenses;
        private System.Windows.Forms.Label lbCountRow2;
    }
}
