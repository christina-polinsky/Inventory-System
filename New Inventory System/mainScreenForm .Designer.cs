using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace New_Inventory_System
{
    partial class mainScreenForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainScreenForm));
            this.pbInventorySystem = new System.Windows.Forms.PictureBox();
            this.pbPreview = new System.Windows.Forms.PictureBox();
            this.btnEquipment = new System.Windows.Forms.Button();
            this.btnComponents = new System.Windows.Forms.Button();
            this.btnShopping = new System.Windows.Forms.Button();
            this.btnProjects = new System.Windows.Forms.Button();
            this.ttpInventory = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pbInventorySystem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // pbInventorySystem
            // 
            this.pbInventorySystem.Image = ((System.Drawing.Image)(resources.GetObject("pbInventorySystem.Image")));
            this.pbInventorySystem.Location = new System.Drawing.Point(0, 0);
            this.pbInventorySystem.Name = "pbInventorySystem";
            this.pbInventorySystem.Size = new System.Drawing.Size(1645, 225);
            this.pbInventorySystem.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbInventorySystem.TabIndex = 0;
            this.pbInventorySystem.TabStop = false;
            // 
            // pbPreview
            // 
            this.pbPreview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbPreview.Image = ((System.Drawing.Image)(resources.GetObject("pbPreview.Image")));
            this.pbPreview.Location = new System.Drawing.Point(824, 224);
            this.pbPreview.Name = "pbPreview";
            this.pbPreview.Size = new System.Drawing.Size(821, 646);
            this.pbPreview.TabIndex = 1;
            this.pbPreview.TabStop = false;
            this.pbPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.pbPreview_Paint);
            // 
            // btnEquipment
            // 
            this.btnEquipment.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnEquipment.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnEquipment.Location = new System.Drawing.Point(0, 338);
            this.btnEquipment.Name = "btnEquipment";
            this.btnEquipment.Size = new System.Drawing.Size(825, 115);
            this.btnEquipment.TabIndex = 7;
            this.btnEquipment.Text = "Equipment and Tools";
            this.btnEquipment.UseVisualStyleBackColor = false;
            this.btnEquipment.MouseEnter += new System.EventHandler(this.btnEquipment_MouseEnter);
            this.btnEquipment.MouseLeave += new System.EventHandler(this.btnEquipment_MouseLeave);
            this.btnEquipment.MouseHover += new System.EventHandler(this.btnEquipment_MouseEnter);
            // 
            // btnComponents
            // 
            this.btnComponents.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnComponents.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnComponents.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.btnComponents.Location = new System.Drawing.Point(0, 227);
            this.btnComponents.Name = "btnComponents";
            this.btnComponents.Size = new System.Drawing.Size(825, 120);
            this.btnComponents.TabIndex = 6;
            this.btnComponents.Text = "Components and Parts";
            this.btnComponents.UseVisualStyleBackColor = false;
       //     this.btnComponents.Click += new System.EventHandler(this.btnComponents_Click);
            this.btnComponents.MouseEnter += new System.EventHandler(this.btnComponents_MouseEnter);
            this.btnComponents.MouseLeave += new System.EventHandler(this.btnComponents_MouseLeave);
            this.btnComponents.MouseHover += new System.EventHandler(this.btnComponents_MouseEnter);
            // 
            // btnShopping
            // 
            this.btnShopping.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnShopping.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnShopping.Location = new System.Drawing.Point(0, 560);
            this.btnShopping.Name = "btnShopping";
            this.btnShopping.Size = new System.Drawing.Size(825, 115);
            this.btnShopping.TabIndex = 9;
            this.btnShopping.Text = "Shopping List";
            this.btnShopping.UseVisualStyleBackColor = false;
            this.btnShopping.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnShopping_Click);
            this.btnShopping.MouseEnter += new System.EventHandler(this.btnShopping_MouseEnter);
            this.btnShopping.MouseLeave += new System.EventHandler(this.btnShopping_MouseLeave);
            this.btnShopping.MouseHover += new System.EventHandler(this.btnShopping_MouseEnter);
            // 
            // btnProjects
            // 
            this.btnProjects.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnProjects.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnProjects.Location = new System.Drawing.Point(0, 449);
            this.btnProjects.Name = "btnProjects";
            this.btnProjects.Size = new System.Drawing.Size(825, 115);
            this.btnProjects.TabIndex = 8;
            this.btnProjects.Text = "Projects";
            this.btnProjects.UseVisualStyleBackColor = false;
            this.btnProjects.Click += new System.EventHandler(this.btnProjects_Click);
            this.btnProjects.MouseEnter += new System.EventHandler(this.btnProjects_MouseEnter);
            this.btnProjects.MouseLeave += new System.EventHandler(this.btnProjects_MouseLeave);
            this.btnProjects.MouseHover += new System.EventHandler(this.btnProjects_MouseEnter);
            // 
            // mainScreenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(1645, 869);
            this.Controls.Add(this.btnShopping);
            this.Controls.Add(this.btnProjects);
            this.Controls.Add(this.btnEquipment);
            this.Controls.Add(this.btnComponents);
            this.Controls.Add(this.pbPreview);
            this.Controls.Add(this.pbInventorySystem);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1000, 700);
            this.Name = "mainScreenForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pbInventorySystem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbInventorySystem;
        private System.Windows.Forms.PictureBox pbPreview;
        private System.Windows.Forms.Button btnEquipment;
        private System.Windows.Forms.Button btnComponents;
        private System.Windows.Forms.Button btnShopping;
        private System.Windows.Forms.Button btnProjects;
        private ToolTip ttpInventory;
    }

}

