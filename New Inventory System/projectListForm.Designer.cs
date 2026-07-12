namespace New_Inventory_System
{
    partial class projectListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(projectListForm));
            this.pbHeader = new System.Windows.Forms.PictureBox();
            this.pnlToolbar = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboFilter = new System.Windows.Forms.ComboBox();
            this.lblFilter = new System.Windows.Forms.Label();
            this.lvProjects = new System.Windows.Forms.ListView();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pbHeader)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbHeader
            // 
            this.pbHeader.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbHeader.BackgroundImage")));
            this.pbHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pbHeader.Location = new System.Drawing.Point(0, 0);
            this.pbHeader.Name = "pbHeader";
            this.pbHeader.Size = new System.Drawing.Size(1424, 80);
            this.pbHeader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbHeader.TabIndex = 0;
            this.pbHeader.TabStop = false;
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.btnDelete);
            this.pnlToolbar.Controls.Add(this.btnOpen);
            this.pnlToolbar.Controls.Add(this.btnNew);
            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.Location = new System.Drawing.Point(0, 80);
            this.pnlToolbar.Name = "pnlToolbar";
            this.pnlToolbar.Size = new System.Drawing.Size(1424, 40);
            this.pnlToolbar.TabIndex = 1;
            // 
            // btnDelete
            // 
            this.btnDelete.AutoSize = true;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Tahoma", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(229, 0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(85, 40);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.AutoSize = true;
            this.btnOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpen.Font = new System.Drawing.Font("Tahoma", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpen.Location = new System.Drawing.Point(151, 0);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 40);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnNew
            // 
            this.btnNew.AutoSize = true;
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Font = new System.Drawing.Font("Tahoma", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.Location = new System.Drawing.Point(0, 0);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(150, 40);
            this.btnNew.TabIndex = 0;
            this.btnNew.Text = "New Project";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // pnlFilter
            // 
            this.pnlFilter.Controls.Add(this.txtSearch);
            this.pnlFilter.Controls.Add(this.label1);
            this.pnlFilter.Controls.Add(this.cboFilter);
            this.pnlFilter.Controls.Add(this.lblFilter);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(0, 120);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(1424, 35);
            this.pnlFilter.TabIndex = 2;
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Tahoma", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(460, 2);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(300, 33);
            this.txtSearch.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(380, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Search:";
            // 
            // cboFilter
            // 
            this.cboFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFilter.Font = new System.Drawing.Font("Tahoma", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboFilter.FormattingEnabled = true;
            this.cboFilter.Items.AddRange(new object[] {
            "All",
            "Radio",
            "Computer",
            "Console",
            "Programming",
            "Electronics",
            "CNC",
            "Other"});
            this.cboFilter.Location = new System.Drawing.Point(150, 1);
            this.cboFilter.Name = "cboFilter";
            this.cboFilter.Size = new System.Drawing.Size(224, 33);
            this.cboFilter.TabIndex = 1;
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Font = new System.Drawing.Font("Tahoma", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilter.Location = new System.Drawing.Point(5, 8);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(147, 25);
            this.lblFilter.TabIndex = 0;
            this.lblFilter.Text = "Filter by Type:";
            // 
            // lvProjects
            // 
            this.lvProjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvProjects.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lvProjects.FullRowSelect = true;
            this.lvProjects.GridLines = true;
            this.lvProjects.HideSelection = false;
            this.lvProjects.Location = new System.Drawing.Point(0, 155);
            this.lvProjects.Name = "lvProjects";
            this.lvProjects.Size = new System.Drawing.Size(1424, 774);
            this.lvProjects.TabIndex = 3;
            this.lvProjects.UseCompatibleStateImageBehavior = false;
            this.lvProjects.View = System.Windows.Forms.View.Details;
            this.lvProjects.DoubleClick += new System.EventHandler(this.lvProjects_DoubleClick);
            // 
            // statusStrip
            // 
            this.statusStrip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip.Location = new System.Drawing.Point(0, 890);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1424, 39);
            this.statusStrip.TabIndex = 4;
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(119, 29);
            this.lblStatus.Text = "0 Projects";
            // 
            // projectListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1424, 929);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.lvProjects);
            this.Controls.Add(this.pnlFilter);
            this.Controls.Add(this.pnlToolbar);
            this.Controls.Add(this.pbHeader);
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "projectListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Projects";
            ((System.ComponentModel.ISupportInitialize)(this.pbHeader)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbHeader;
        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboFilter;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ListView lvProjects;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
    }
}