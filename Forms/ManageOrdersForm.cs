using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace GreenLifeStore.Forms
{
    public partial class ManageOrdersForm : BaseForm
    {

        private AdminDashboardForm adminDashboardForm;

        private string connectionString = "server=localhost;database=greenlife;uid=root;pwd=1234;";

        public ManageOrdersForm(AdminDashboardForm adminDashboardForm)
        {
            InitializeComponent();
            this.adminDashboardForm = adminDashboardForm;
        }

        private void LoadOrders()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = @"
                SELECT 
                    o.order_id,
                    c.name AS customer_name,
                    o.order_date,
                    o.total_amount,
                    o.order_status
                FROM orders o
                INNER JOIN customers c ON o.customer_id = c.customer_id
                ORDER BY o.order_date DESC";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();

                    connection.Open();
                    adapter.Fill(dt);

                    dgvOrders.DataSource = dt;

                    dgvOrders.ReadOnly = true;
                    dgvOrders.AllowUserToAddRows = false;
                    dgvOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dgvOrders.MultiSelect = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading orders: " + ex.Message);
            }
        }

        private void LoadOrderItems(int orderId)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = @"
                SELECT
                    p.product_name,
                    oi.quantity,
                    oi.unit_price,
                    oi.discount,
                    (oi.unit_price - (oi.unit_price * oi.discount / 100)) * oi.quantity AS line_total
                FROM order_items oi
                INNER JOIN products p ON oi.product_id = p.product_id
                WHERE oi.order_id = @orderId";

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@orderId", orderId);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    connection.Open();
                    adapter.Fill(dt);

                    dgvOrderItems.DataSource = dt;

                    dgvOrderItems.ReadOnly = true;
                    dgvOrderItems.AllowUserToAddRows = false;
                    dgvOrderItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading order details: " + ex.Message);
            }
        }

        private void dgvOrders_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvOrders.SelectedRows.Count == 0)
                return;

            int orderId = Convert.ToInt32(
                dgvOrders.SelectedRows[0].Cells["order_id"].Value);

            string status =
                dgvOrders.SelectedRows[0].Cells["order_status"].Value.ToString();

            lblSelectedOrder.Text = $"Order ID: {orderId}";
            cmbOrderStatus.SelectedItem = status;

            LoadOrderItems(orderId);
        }


        private void ManageOrdersForm_Load(object sender, EventArgs e)
        {
            cmbOrderStatus.Items.AddRange(new string[]
            {
        "PLACED",
        "PROCESSING",
        "SHIPPED",
        "DELIVERED",
        "CANCELLED"
            });

            LoadOrders();
            dgvOrders.SelectionChanged += dgvOrders_SelectionChanged;
        }

        private void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            if (dgvOrders.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an order.");
                return;
            }

            int orderId = Convert.ToInt32(
                dgvOrders.SelectedRows[0].Cells["order_id"].Value);

            string newStatus = cmbOrderStatus.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(newStatus))
            {
                MessageBox.Show("Please select a status.");
                return;
            }

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = @"
                UPDATE orders
                SET order_status = @status
                WHERE order_id = @orderId";

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@status", newStatus);
                    cmd.Parameters.AddWithValue("@orderId", orderId);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Order status updated successfully.");
                LoadOrders();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Status update failed: " + ex.Message);
            }
        }


        private void btnBack_Click(object sender, EventArgs e)
        {
            adminDashboardForm.Show();
            this.Hide();
        }

        private void ManageOrdersForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfirmAndExit(e);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
