using GreenLifeStore.Models;
using GreenLifeStore.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace GreenLifeStore.Forms
{
    public partial class CustomerOrdersForm : BaseForm
    {

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
                using (MySqlConnection connection = new MySqlConnection(DatabaseConfig.ConnectionString))
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
                using (MySqlConnection connection = new MySqlConnection(DatabaseConfig.ConnectionString))
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
            if (dgvOrders.SelectedRows.Count == 0) return;

            string status =
                dgvOrders.SelectedRows[0].Cells["order_status"].Value.ToString();

            btnCancelOrder.Enabled =
                status == OrderStatus.Pending.ToString() || status == OrderStatus.Confirmed.ToString();

            int orderId = Convert.ToInt32(
                dgvOrders.SelectedRows[0].Cells["order_id"].Value);

            LoadOrderItems(orderId);
        }

        private void dgvOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnCancelOrder_Click(object sender, EventArgs e)
        {
            int orderId = Convert.ToInt32(
                dgvOrders.SelectedRows[0].Cells["order_id"].Value);

            DialogResult result = MessageBox.Show(
                "Are you sure you want to cancel this order?",
                "Confirm Cancellation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                CancelOrder(orderId);
                LoadCustomerOrders();
            }
        }

        private void CancelOrder(int orderId)
        {
            using (MySqlConnection conn = new MySqlConnection(DatabaseConfig.ConnectionString))
            {
                conn.Open();
                MySqlTransaction tx = conn.BeginTransaction();

                try
                {
                    // 1. Get order items
                    string itemQuery = @"
                        SELECT product_id, quantity
                        FROM order_items
                        WHERE order_id = @orderId";

                    MySqlCommand itemCmd = new MySqlCommand(itemQuery, conn, tx);
                    itemCmd.Parameters.AddWithValue("@orderId", orderId);

                    MySqlDataReader reader = itemCmd.ExecuteReader();

                    var items = new List<(int ProductId, int Quantity)>();

                    while (reader.Read())
                    {
                        items.Add((
                            reader.GetInt32("product_id"),
                            reader.GetInt32("quantity")
                        ));
                    }
                    reader.Close();

                    // 2. Restore stock
                    foreach (var item in items)
                    {
                        string restoreQuery = @"
                            UPDATE products
                            SET stock_quantity = stock_quantity + @qty,
                                modified_date = NOW()
                            WHERE product_id = @productId";

                        MySqlCommand restoreCmd =
                            new MySqlCommand(restoreQuery, conn, tx);

                        restoreCmd.Parameters.AddWithValue("@qty", item.Quantity);
                        restoreCmd.Parameters.AddWithValue("@productId", item.ProductId);
                        restoreCmd.ExecuteNonQuery();
                    }

                    LoadCustomerOrders();

                    // 3. Update order status
                    string cancelOrderQuery = @"
                        UPDATE orders
                        SET order_status = 'Cancelled'
                        WHERE order_id = @orderId
                        AND order_status IN ('Pending','Confirmed')";

                    MySqlCommand cancelCmd =
                        new MySqlCommand(cancelOrderQuery, conn, tx);

                    cancelCmd.Parameters.AddWithValue("@orderId", orderId);
                    cancelCmd.ExecuteNonQuery();

                    tx.Commit();
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }
    }
}
