using GreenLifeStore.Models;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace GreenLifeStore.Forms
{
    public partial class ManageProductsForm : BaseForm
    {

        private AdminDashboardForm adminDashboardForm;

        private string connectionString = "server=localhost;database=greenlife;uid=root;pwd=1234;";

        public ManageProductsForm(AdminDashboardForm adminDashboardForm)
        {
            InitializeComponent();
            this.adminDashboardForm = adminDashboardForm;
        }

        public void LoadProducts()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = "SELECT product_id, product_name, category, price, stock_quantity, supplier, discount FROM products WHERE active_status = 1";

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    connection.Open();
                    adapter.Fill(dt);

                    dgvProducts.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error Loading Products: " + ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void ManageProductsForm_Load(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            adminDashboardForm.Show();
            this.Hide();
        }

        private void ManageProductsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfirmAndExit(e);
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            AddProductForm addProductForm = new AddProductForm(this);
            addProductForm.Show();
            this.Hide();
        }

        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a product to delete.");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Are you sure you want to remove this product?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.No) return;

            int productId = Convert.ToInt32(dgvProducts.SelectedRows[0].Cells["product_id"].Value);

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = @"
                    UPDATE products 
                    SET active_status = 0, 
                    modified_date = NOW() 
                    WHERE product_id = @id";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", productId);

                connection.Open();
                cmd.ExecuteNonQuery();
            }

            LoadProducts();
            MessageBox.Show("Product removed successfully.");
        }

        private void btnUpdateProduct_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a product to update.");
                return;
            }

            DataGridViewRow row = dgvProducts.SelectedRows[0];

            Product selectedProduct = new Product
            {
                Id = Convert.ToInt32(row.Cells["product_id"].Value),
                Name = row.Cells["product_name"].Value.ToString(),
                Category = row.Cells["category"].Value.ToString(),
                Price = Convert.ToDouble(row.Cells["price"].Value),
                StockQty = Convert.ToInt32(row.Cells["stock_quantity"].Value),
                Supplier = row.Cells["supplier"].Value.ToString(),
                Discount = Convert.ToInt32(row.Cells["discount"].Value)
            };

            UpdateProductForm updateForm = new UpdateProductForm(this, selectedProduct);

            updateForm.Show();
            this.Hide();
        }
    }
}
