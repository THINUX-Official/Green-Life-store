namespace GreenLifeStore.Forms
{
    partial class AdminDashboardForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblStockCount = new System.Windows.Forms.Label();
            this.lblActiveOrderCount = new System.Windows.Forms.Label();
            this.lblSalesCount = new System.Windows.Forms.Label();
            this.btnManageProducts = new System.Windows.Forms.Button();
            this.btnManageCustomers = new System.Windows.Forms.Button();
            this.btnManageOrders = new System.Windows.Forms.Button();
            this.btnReports = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(218, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Total Sales: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(218, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Product in Stock: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(218, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Active Orders: ";
            // 
            // lblStockCount
            // 
            this.lblStockCount.AutoSize = true;
            this.lblStockCount.BackColor = System.Drawing.SystemColors.Control;
            this.lblStockCount.Location = new System.Drawing.Point(316, 49);
            this.lblStockCount.Name = "lblStockCount";
            this.lblStockCount.Size = new System.Drawing.Size(66, 13);
            this.lblStockCount.TabIndex = 8;
            this.lblStockCount.Text = "Stock Count";
            // 
            // lblActiveOrderCount
            // 
            this.lblActiveOrderCount.AutoSize = true;
            this.lblActiveOrderCount.BackColor = System.Drawing.SystemColors.Control;
            this.lblActiveOrderCount.Location = new System.Drawing.Point(316, 78);
            this.lblActiveOrderCount.Name = "lblActiveOrderCount";
            this.lblActiveOrderCount.Size = new System.Drawing.Size(71, 13);
            this.lblActiveOrderCount.TabIndex = 9;
            this.lblActiveOrderCount.Text = "Active Orders";
            // 
            // lblSalesCount
            // 
            this.lblSalesCount.AutoSize = true;
            this.lblSalesCount.BackColor = System.Drawing.SystemColors.Control;
            this.lblSalesCount.Location = new System.Drawing.Point(316, 20);
            this.lblSalesCount.Name = "lblSalesCount";
            this.lblSalesCount.Size = new System.Drawing.Size(64, 13);
            this.lblSalesCount.TabIndex = 10;
            this.lblSalesCount.Text = "Sales Count";
            // 
            // btnManageProducts
            // 
            this.btnManageProducts.Location = new System.Drawing.Point(3, 3);
            this.btnManageProducts.Name = "btnManageProducts";
            this.btnManageProducts.Size = new System.Drawing.Size(194, 23);
            this.btnManageProducts.TabIndex = 0;
            this.btnManageProducts.Text = "Manage Products";
            this.btnManageProducts.UseVisualStyleBackColor = true;
            this.btnManageProducts.Click += new System.EventHandler(this.btnManageProducts_Click);
            // 
            // btnManageCustomers
            // 
            this.btnManageCustomers.Location = new System.Drawing.Point(3, 32);
            this.btnManageCustomers.Name = "btnManageCustomers";
            this.btnManageCustomers.Size = new System.Drawing.Size(194, 23);
            this.btnManageCustomers.TabIndex = 1;
            this.btnManageCustomers.Text = "Manage Customers";
            this.btnManageCustomers.UseVisualStyleBackColor = true;
            this.btnManageCustomers.Click += new System.EventHandler(this.btnManageCustomers_Click);
            // 
            // btnManageOrders
            // 
            this.btnManageOrders.Location = new System.Drawing.Point(3, 61);
            this.btnManageOrders.Name = "btnManageOrders";
            this.btnManageOrders.Size = new System.Drawing.Size(194, 23);
            this.btnManageOrders.TabIndex = 2;
            this.btnManageOrders.Text = "Manage Orders";
            this.btnManageOrders.UseVisualStyleBackColor = true;
            this.btnManageOrders.Click += new System.EventHandler(this.btnManageOrders_Click);
            // 
            // btnReports
            // 
            this.btnReports.Location = new System.Drawing.Point(3, 90);
            this.btnReports.Name = "btnReports";
            this.btnReports.Size = new System.Drawing.Size(194, 23);
            this.btnReports.TabIndex = 3;
            this.btnReports.Text = "Reports";
            this.btnReports.UseVisualStyleBackColor = true;
            this.btnReports.Click += new System.EventHandler(this.btnReports_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLogout.Location = new System.Drawing.Point(3, 400);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(194, 23);
            this.btnLogout.TabIndex = 4;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnLogout);
            this.panel1.Controls.Add(this.btnReports);
            this.panel1.Controls.Add(this.btnManageOrders);
            this.panel1.Controls.Add(this.btnManageCustomers);
            this.panel1.Controls.Add(this.btnManageProducts);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 426);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // AdminDashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblSalesCount);
            this.Controls.Add(this.lblActiveOrderCount);
            this.Controls.Add(this.lblStockCount);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Name = "AdminDashboardForm";
            this.Text = "Greenlife - Admin Dashboard";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AdminDashboardForm_FormClosing);
            this.Load += new System.EventHandler(this.AdminDashboardForm_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblStockCount;
        private System.Windows.Forms.Label lblActiveOrderCount;
        private System.Windows.Forms.Label lblSalesCount;
        private System.Windows.Forms.Button btnManageProducts;
        private System.Windows.Forms.Button btnManageCustomers;
        private System.Windows.Forms.Button btnManageOrders;
        private System.Windows.Forms.Button btnReports;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Panel panel1;
    }
}