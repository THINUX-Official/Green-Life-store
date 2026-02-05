using GreenLifeStore.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace GreenLifeStore.Forms
{
    public partial class SalesReportForm : BaseForm
    {

        private AdminDashboardForm adminDashboardForm;

        public SalesReportForm(AdminDashboardForm adminDashboardForm)
        {
            InitializeComponent();
            this.adminDashboardForm = adminDashboardForm;
        }

        private void SalesReportForm_Load(object sender, EventArgs e)
        {
            LoadSalesReport();
            LoadStockReport();
            LoadCustomerOrderHistoryReport();
            ;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            adminDashboardForm.Show();
            this.Hide();
        }

        private void SalesReportForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfirmAndExit(e);
        }

        private void LoadSalesReport()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    string query = @"
                        SELECT 
                            DATE(order_date) AS sale_date,
                            COUNT(order_id) AS total_orders,
                            SUM(total_amount) AS total_sales
                        FROM orders
                        WHERE order_status = 'Delivered'
                        GROUP BY DATE(order_date)
                        ORDER BY sale_date DESC";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvSalesReport.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex, "Failed to load sales report.");
            }
        }

        private void LoadStockReport()
        {
            try
            {
                using (MySqlConnection connection =
                       new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    string query = @"
                        SELECT 
                            product_name,
                            category,
                            price,
                            stock_quantity,
                            CASE
                                WHEN stock_quantity = 0 THEN 'Out of Stock'
                                WHEN stock_quantity < 10 THEN 'Low Stock'
                                ELSE 'In Stock'
                            END AS stock_status
                        FROM products
                        WHERE active_status = 1
                        ORDER BY stock_quantity ASC";

                    MySqlDataAdapter adapter =
                        new MySqlDataAdapter(query, connection);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvStockReport.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex, "Failed to load stock report.");
            }
        }

        private void LoadCustomerOrderHistoryReport()
        {
            try
            {
                using (MySqlConnection connection =
                       new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    string query = @"
                        SELECT 
                            o.order_id,
                            c.name AS customer_name,
                            o.order_date,
                            o.total_amount,
                            o.order_status
                        FROM orders o
                        INNER JOIN customers c 
                            ON o.customer_id = c.customer_id
                        ORDER BY o.order_date DESC";

                    MySqlDataAdapter adapter =
                        new MySqlDataAdapter(query, connection);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvCustomerOrderHistoryReport.DataSource = dt;

                    dgvCustomerOrderHistoryReport.ReadOnly = true;
                    dgvCustomerOrderHistoryReport.AllowUserToAddRows = false;
                    dgvCustomerOrderHistoryReport.SelectionMode =
                        DataGridViewSelectionMode.FullRowSelect;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex, "Failed to load customer order history.");
            }
        }


        private void ExportToCSV(DataGridView dgv)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "CSV Files (*.csv)|*.csv"
            };

            if (sfd.ShowDialog() != DialogResult.OK) return;

            using (StreamWriter sw = new StreamWriter(sfd.FileName))
            {
                // Headers
                foreach (DataGridViewColumn col in dgv.Columns)
                    sw.Write(col.HeaderText + ",");

                sw.WriteLine();

                // Rows
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                        sw.Write(cell.Value + ",");

                    sw.WriteLine();
                }
            }

            MessageBox.Show("Report exported successfully.");
        }

        private void btnDownloadSalesReport_Click(object sender, EventArgs e)
        {
            if (dgvSalesReport.Rows.Count == 0)
            {
                MessageBox.Show(
                    "No sales data available to export.",
                    "Export Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            ExportToCSV(dgvSalesReport);
        }

        private void btnDonwloadStockReport_Click(object sender, EventArgs e)
        {
            if (dgvStockReport.Rows.Count == 0)
            {
                MessageBox.Show(
                    "No sales data available to export.",
                    "Export Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            ExportToCSV(dgvStockReport);
        }

        private void btnCustomerOrderHistoryReport_Click(object sender, EventArgs e)
        {
            if (dgvCustomerOrderHistoryReport.Rows.Count == 0)
            {
                MessageBox.Show(
                    "No sales data available to export.",
                    "Export Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            ExportToCSV(dgvCustomerOrderHistoryReport);
        }
    }
}
