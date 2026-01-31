namespace GreenLifeStore.Forms
{
    partial class SalesReportForm
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
            this.btnBack = new System.Windows.Forms.Button();
            this.dgvSalesReport = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvStockReport = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvCustomerOrderHistoryReport = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.btnDownloadSalesReport = new System.Windows.Forms.Button();
            this.btnDonwloadStockReport = new System.Windows.Forms.Button();
            this.btnCustomerOrderHistoryReport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomerOrderHistoryReport)).BeginInit();
            this.SuspendLayout();
            // 
            // btnBack
            // 
            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBack.Location = new System.Drawing.Point(12, 646);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 2;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // dgvSalesReport
            // 
            this.dgvSalesReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSalesReport.Location = new System.Drawing.Point(12, 25);
            this.dgvSalesReport.Name = "dgvSalesReport";
            this.dgvSalesReport.Size = new System.Drawing.Size(400, 586);
            this.dgvSalesReport.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Sales Report";
            // 
            // dgvStockReport
            // 
            this.dgvStockReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStockReport.Location = new System.Drawing.Point(432, 25);
            this.dgvStockReport.Name = "dgvStockReport";
            this.dgvStockReport.Size = new System.Drawing.Size(400, 586);
            this.dgvStockReport.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(429, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Stock Report";
            // 
            // dgvCustomerOrderHistoryReport
            // 
            this.dgvCustomerOrderHistoryReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCustomerOrderHistoryReport.Location = new System.Drawing.Point(852, 25);
            this.dgvCustomerOrderHistoryReport.Name = "dgvCustomerOrderHistoryReport";
            this.dgvCustomerOrderHistoryReport.Size = new System.Drawing.Size(400, 586);
            this.dgvCustomerOrderHistoryReport.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(849, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Customer Order History Report";
            // 
            // btnDownloadSalesReport
            // 
            this.btnDownloadSalesReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDownloadSalesReport.Location = new System.Drawing.Point(12, 617);
            this.btnDownloadSalesReport.Name = "btnDownloadSalesReport";
            this.btnDownloadSalesReport.Size = new System.Drawing.Size(400, 23);
            this.btnDownloadSalesReport.TabIndex = 10;
            this.btnDownloadSalesReport.Text = "Download Sales Report";
            this.btnDownloadSalesReport.UseVisualStyleBackColor = true;
            this.btnDownloadSalesReport.Click += new System.EventHandler(this.btnDownloadSalesReport_Click);
            // 
            // btnDonwloadStockReport
            // 
            this.btnDonwloadStockReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDonwloadStockReport.Location = new System.Drawing.Point(432, 617);
            this.btnDonwloadStockReport.Name = "btnDonwloadStockReport";
            this.btnDonwloadStockReport.Size = new System.Drawing.Size(400, 23);
            this.btnDonwloadStockReport.TabIndex = 11;
            this.btnDonwloadStockReport.Text = "Download Stock Report";
            this.btnDonwloadStockReport.UseVisualStyleBackColor = true;
            this.btnDonwloadStockReport.Click += new System.EventHandler(this.btnDonwloadStockReport_Click);
            // 
            // btnCustomerOrderHistoryReport
            // 
            this.btnCustomerOrderHistoryReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCustomerOrderHistoryReport.Location = new System.Drawing.Point(852, 617);
            this.btnCustomerOrderHistoryReport.Name = "btnCustomerOrderHistoryReport";
            this.btnCustomerOrderHistoryReport.Size = new System.Drawing.Size(400, 23);
            this.btnCustomerOrderHistoryReport.TabIndex = 12;
            this.btnCustomerOrderHistoryReport.Text = "Download Customer Order History Report";
            this.btnCustomerOrderHistoryReport.UseVisualStyleBackColor = true;
            this.btnCustomerOrderHistoryReport.Click += new System.EventHandler(this.btnCustomerOrderHistoryReport_Click);
            // 
            // SalesReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.btnCustomerOrderHistoryReport);
            this.Controls.Add(this.btnDonwloadStockReport);
            this.Controls.Add(this.btnDownloadSalesReport);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dgvCustomerOrderHistoryReport);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvStockReport);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvSalesReport);
            this.Controls.Add(this.btnBack);
            this.Name = "SalesReportForm";
            this.Text = "SalesReportForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SalesReportForm_FormClosing);
            this.Load += new System.EventHandler(this.SalesReportForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomerOrderHistoryReport)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.DataGridView dgvSalesReport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvStockReport;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvCustomerOrderHistoryReport;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnDownloadSalesReport;
        private System.Windows.Forms.Button btnDonwloadStockReport;
        private System.Windows.Forms.Button btnCustomerOrderHistoryReport;
    }
}