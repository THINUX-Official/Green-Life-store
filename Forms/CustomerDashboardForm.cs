using GreenLifeStore.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace GreenLifeStore.Forms
{
    public partial class CustomerDashboardForm : BaseForm
    {

        private List<CartItem> cart = new List<CartItem>();

        private string connectionString = "server=localhost;database=greenlife;uid=root;pwd=1234;";

        private LoginForm loginForm;
        private int customerId;
        public CustomerDashboardForm(LoginForm loginForm, int customerId)
        {
            InitializeComponent();
            this.loginForm = loginForm;
            this.customerId = customerId;
        }

        private void CustomerDashboardForm_Load(object sender, EventArgs e)
        {
            LoadProducts();

            // Prevent auto-generation to avoid duplicate columns
            dgvProductList.AutoGenerateColumns = false;

            // Add quantity control columns ONLY ONCE
            if (!dgvProductList.Columns.Contains("btnMinus"))
            {
                DataGridViewButtonColumn minusBtn = new DataGridViewButtonColumn();
                minusBtn.Name = "btnMinus";
                minusBtn.HeaderText = "";
                minusBtn.Text = "-";
                minusBtn.UseColumnTextForButtonValue = true;
                minusBtn.Width = 30;

                DataGridViewTextBoxColumn qtyCol = new DataGridViewTextBoxColumn();
                qtyCol.Name = "quantity";
                qtyCol.HeaderText = "Qty";
                qtyCol.ValueType = typeof(int);
                qtyCol.Width = 50;

                DataGridViewButtonColumn plusBtn = new DataGridViewButtonColumn();
                plusBtn.Name = "btnPlus";
                plusBtn.HeaderText = "";
                plusBtn.Text = "+";
                plusBtn.UseColumnTextForButtonValue = true;
                plusBtn.Width = 30;

                DataGridViewTextBoxColumn totalCol = new DataGridViewTextBoxColumn();
                totalCol.Name = "rowTotal";
                totalCol.HeaderText = "Total";
                totalCol.ReadOnly = true;
                totalCol.ValueType = typeof(decimal);
                totalCol.Width = 80;

                dgvProductList.Columns.Add(minusBtn);
                dgvProductList.Columns.Add(qtyCol);
                dgvProductList.Columns.Add(plusBtn);
                dgvProductList.Columns.Add(totalCol);
            }
        }

        private void RecalculateRowAndGrandTotal()
        {
            decimal grandTotal = 0;

            foreach (DataGridViewRow row in dgvProductList.Rows)
            {
                int qty = Convert.ToInt32(row.Cells["quantity"].Value ?? 0);
                decimal price = Convert.ToDecimal(row.Cells["price"].Value);

                decimal rowTotal = qty * price;
                row.Cells["rowTotal"].Value = rowTotal;

                grandTotal += rowTotal;
            }

            txtTotal.Text = grandTotal.ToString("0.00");
        }


        private void LoadProducts()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = @"
                SELECT 
                    product_id,
                    product_name,
                    category,
                    price,
                    stock_quantity,
                    discount
                FROM products
                WHERE active_status = 1";

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    connection.Open();
                    adapter.Fill(dt);

                    dgvProductList.DataSource = dt;

                    // Hide internal identifier
                    dgvProductList.Columns["product_id"].Visible = false;

                    // Customer-safe DataGridView settings
                    dgvProductList.ReadOnly = true;
                    dgvProductList.AllowUserToAddRows = false;
                    dgvProductList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dgvProductList.MultiSelect = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error loading products: " + ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


        private void btnLogout_Click(object sender, EventArgs e)
        {
            loginForm.Show();
            this.Hide();
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            ProfileForm profileForm = new ProfileForm(this, customerId);
            profileForm.Show();
            this.Hide();
        }

        private void CustomerDashboardForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfirmAndExit(e);
        }

        private void btnMyOrders_Click(object sender, EventArgs e)
        {
            CustomerOrdersForm customerOrdersForm = new CustomerOrdersForm(this, customerId);
            customerOrdersForm.Show();
            this.Hide();
        }

        private void dgvProductList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvProductList.Rows[e.RowIndex];
            int qty = Convert.ToInt32(row.Cells["quantity"].Value ?? 0);

            if (dgvProductList.Columns[e.ColumnIndex].Name == "btnPlus")
            {
                qty++;
            }
            else if (dgvProductList.Columns[e.ColumnIndex].Name == "btnMinus")
            {
                if (qty > 0)
                    qty--;
            }

            row.Cells["quantity"].Value = qty;

            RecalculateRowAndGrandTotal();
        }

        private void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            List<CartItem> cartItems = BuildCartFromGrid();

            if (cartItems.Count == 0)
            {
                MessageBox.Show("Please select at least one product.");
                return;
            }

            decimal orderTotal = 0;
            foreach (var item in cartItems)
                orderTotal += (decimal)item.SubTotal;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlTransaction tx = connection.BeginTransaction();

                    try
                    {
                        // 1. Insert order
                        string orderQuery = @"
                INSERT INTO orders (customer_id, total_amount, order_status)
                VALUES (@customerId, @total, 'PLACED');
                SELECT LAST_INSERT_ID();";

                        MySqlCommand orderCmd = new MySqlCommand(orderQuery, connection, tx);
                        orderCmd.Parameters.AddWithValue("@customerId", customerId);
                        orderCmd.Parameters.AddWithValue("@total", orderTotal);

                        int orderId = Convert.ToInt32(orderCmd.ExecuteScalar());

                        // 2. Insert order items
                        foreach (var item in cartItems)
                        {
                            string itemQuery = @"
                    INSERT INTO order_items
                    (order_id, product_id, quantity, unit_price, discount)
                    VALUES
                    (@orderId, @productId, @qty, @price, @discount)";

                            MySqlCommand itemCmd = new MySqlCommand(itemQuery, connection, tx);
                            itemCmd.Parameters.AddWithValue("@orderId", orderId);
                            itemCmd.Parameters.AddWithValue("@productId", item.ProductId);
                            itemCmd.Parameters.AddWithValue("@qty", item.Quantity);
                            itemCmd.Parameters.AddWithValue("@price", item.Price);
                            itemCmd.Parameters.AddWithValue("@discount", item.Discount);

                            itemCmd.ExecuteNonQuery();

                            // 3. Reduce stock
                            string stockQuery = @"
                    UPDATE products
                    SET stock_quantity = stock_quantity - @qty,
                        modified_date = NOW()
                    WHERE product_id = @productId";

                            MySqlCommand stockCmd = new MySqlCommand(stockQuery, connection, tx);
                            stockCmd.Parameters.AddWithValue("@qty", item.Quantity);
                            stockCmd.Parameters.AddWithValue("@productId", item.ProductId);

                            stockCmd.ExecuteNonQuery();
                        }

                        tx.Commit();

                        MessageBox.Show("Order placed successfully.");

                        LoadProducts();
                        txtTotal.Text = "0.00";
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Order failed: " + ex.Message);
            }
        }


        private void dgvProductList_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dgvProductList.Columns[e.ColumnIndex].Name == "quantity")
            {
                if (!int.TryParse(e.FormattedValue.ToString(), out int qty) || qty < 0)
                {
                    MessageBox.Show("Quantity must be a non-negative integer.");
                    e.Cancel = true;
                }
            }
        }

        private void dgvProductList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvProductList.Columns[e.ColumnIndex].Name == "quantity")
            {
                RecalculateRowAndGrandTotal();
            }
        }

        private List<CartItem> BuildCartFromGrid()
        {
            List<CartItem> items = new List<CartItem>();

            foreach (DataGridViewRow row in dgvProductList.Rows)
            {
                int qty = Convert.ToInt32(row.Cells["quantity"].Value ?? 0);
                if (qty <= 0) continue;

                items.Add(new CartItem
                {
                    ProductId = Convert.ToInt32(row.Cells["product_id"].Value),
                    ProductName = row.Cells["product_name"].Value.ToString(),
                    Price = Convert.ToDouble(row.Cells["price"].Value),
                    Discount = Convert.ToInt32(row.Cells["discount"].Value),
                    Quantity = qty
                });
            }

            return items;
        }


    }
}
