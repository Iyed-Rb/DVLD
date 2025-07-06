namespace DVLDPresentationLayer
{
    partial class frmTestAppointment
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTestAppointment));
            this.lbTest = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvAllAppointments = new System.Windows.Forms.DataGridView();
            this.lbj = new System.Windows.Forms.Label();
            this.lbCountRow = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TakeTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btAddAppointment = new System.Windows.Forms.Button();
            this.btCLose = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ctrlApplicationInfo1 = new DVLDPresentationLayer.Controls.ctrlApplicationInfo();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllAppointments)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbTest
            // 
            this.lbTest.AutoSize = true;
            this.lbTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTest.ForeColor = System.Drawing.Color.Red;
            this.lbTest.Location = new System.Drawing.Point(204, 75);
            this.lbTest.Name = "lbTest";
            this.lbTest.Size = new System.Drawing.Size(280, 25);
            this.lbTest.TabIndex = 2;
            this.lbTest.Text = "Vision Test Appointments";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 385);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 18);
            this.label2.TabIndex = 74;
            this.label2.Text = "Appointments:";
            // 
            // dgvAllAppointments
            // 
            this.dgvAllAppointments.AllowUserToAddRows = false;
            this.dgvAllAppointments.AllowUserToDeleteRows = false;
            this.dgvAllAppointments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAllAppointments.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvAllAppointments.Location = new System.Drawing.Point(12, 406);
            this.dgvAllAppointments.Name = "dgvAllAppointments";
            this.dgvAllAppointments.Size = new System.Drawing.Size(652, 150);
            this.dgvAllAppointments.TabIndex = 76;
            // 
            // lbj
            // 
            this.lbj.AutoSize = true;
            this.lbj.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbj.Location = new System.Drawing.Point(9, 568);
            this.lbj.Name = "lbj";
            this.lbj.Size = new System.Drawing.Size(91, 18);
            this.lbj.TabIndex = 77;
            this.lbj.Text = "# Records:";
            // 
            // lbCountRow
            // 
            this.lbCountRow.AutoSize = true;
            this.lbCountRow.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCountRow.Location = new System.Drawing.Point(110, 568);
            this.lbCountRow.Name = "lbCountRow";
            this.lbCountRow.Size = new System.Drawing.Size(17, 18);
            this.lbCountRow.TabIndex = 78;
            this.lbCountRow.Text = "0";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.editToolStripMenuItem,
            this.TakeTestToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(137, 86);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(133, 6);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("editToolStripMenuItem.Image")));
            this.editToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(136, 38);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // TakeTestToolStripMenuItem
            // 
            this.TakeTestToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("TakeTestToolStripMenuItem.Image")));
            this.TakeTestToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TakeTestToolStripMenuItem.Name = "TakeTestToolStripMenuItem";
            this.TakeTestToolStripMenuItem.Size = new System.Drawing.Size(136, 38);
            this.TakeTestToolStripMenuItem.Text = "Take Test";
            this.TakeTestToolStripMenuItem.Click += new System.EventHandler(this.TakeTestToolStripMenuItem_Click_1);
            // 
            // btAddAppointment
            // 
            this.btAddAppointment.BackColor = System.Drawing.Color.Transparent;
            this.btAddAppointment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btAddAppointment.Image = ((System.Drawing.Image)(resources.GetObject("btAddAppointment.Image")));
            this.btAddAppointment.Location = new System.Drawing.Point(627, 370);
            this.btAddAppointment.Name = "btAddAppointment";
            this.btAddAppointment.Size = new System.Drawing.Size(37, 30);
            this.btAddAppointment.TabIndex = 80;
            this.btAddAppointment.UseVisualStyleBackColor = false;
            this.btAddAppointment.Click += new System.EventHandler(this.btAddAppointment_Click);
            // 
            // btCLose
            // 
            this.btCLose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btCLose.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCLose.Image = ((System.Drawing.Image)(resources.GetObject("btCLose.Image")));
            this.btCLose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btCLose.Location = new System.Drawing.Point(549, 561);
            this.btCLose.Name = "btCLose";
            this.btCLose.Size = new System.Drawing.Size(115, 32);
            this.btCLose.TabIndex = 79;
            this.btCLose.Text = "        Close";
            this.btCLose.UseVisualStyleBackColor = true;
            this.btCLose.Click += new System.EventHandler(this.btCLose_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(300, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(89, 69);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // ctrlApplicationInfo1
            // 
            this.ctrlApplicationInfo1.Location = new System.Drawing.Point(12, 103);
            this.ctrlApplicationInfo1.Name = "ctrlApplicationInfo1";
            this.ctrlApplicationInfo1.Size = new System.Drawing.Size(674, 261);
            this.ctrlApplicationInfo1.TabIndex = 75;
            // 
            // frmTestAppointment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(680, 599);
            this.Controls.Add(this.btAddAppointment);
            this.Controls.Add(this.btCLose);
            this.Controls.Add(this.lbCountRow);
            this.Controls.Add(this.lbj);
            this.Controls.Add(this.dgvAllAppointments);
            this.Controls.Add(this.ctrlApplicationInfo1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbTest);
            this.Controls.Add(this.pictureBox1);
            this.Name = "frmTestAppointment";
            this.Text = "frmTestAppointment";
            this.Load += new System.EventHandler(this.frmTestAppointment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllAppointments)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lbTest;
        private System.Windows.Forms.Label label2;
        private Controls.ctrlApplicationInfo ctrlApplicationInfo1;
        private System.Windows.Forms.DataGridView dgvAllAppointments;
        private System.Windows.Forms.Label lbj;
        private System.Windows.Forms.Label lbCountRow;
        private System.Windows.Forms.Button btCLose;
        private System.Windows.Forms.Button btAddAppointment;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TakeTestToolStripMenuItem;
    }
}