using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace GreenLifeStore.Forms
{
    public partial class CustomerOrdersForm : BaseForm
    {

        private string connectionString = "server=localhost;database=greenlife;uid=root;pwd=1234;";

        private int customerId;

        private CustomerDashboardForm customerDashboardForm;
        public CustomerOrdersForm(CustomerDashboardForm customerDashboardForm, int customerId)
        {
            InitializeComponent();
            this.customerDashboardForm = customerDashboardForm;
            this.customerId = customerId;
        }

        private void CustomerOrdersForm_Load(object sender, EventArgs e)
        {
            LoadCustomerOrders();
            dgvOrders.SelectionChanged += dgvOrders_SelectionChanged;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            customerDashboardForm.Show();
            this.Hide();
        }

        private void CustomerOrdersForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfirmAndExit(e);
        }

        private void LoadCustomerOrders()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = @"
            SELECT 
                order_id,
                order_date,
                total_amount,
                order_status
            FROM orders
            WHERE customer_id = @customerId
            ORDER BY order_date DESC";

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@customerId", customerId);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
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
                MessageBox.Show(
                    "Error loading orders: " + ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
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
                MessageBox.Show(
                    "Error loading order details: " + ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void dgvOrders_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvOrders.SelectedRows.Count == 0)
                return;

            int orderId = Convert.ToInt32(
                dgvOrders.SelectedRows[0].Cells["order_id"].Value);

            LoadOrderItems(orderId);
        }
    }
}
