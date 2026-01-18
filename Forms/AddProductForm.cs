using GreenLifeStore.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace GreenLifeStore.Forms
{
    public partial class AddProductForm : BaseForm
    {

        private string connectionString = "server=localhost;database=greenlife;uid=root;pwd=1234;";

        private ManageProductsForm manageProductsForm;
        public AddProductForm(ManageProductsForm manageProductsForm)
        {
            InitializeComponent();
            this.manageProductsForm = manageProductsForm;
        }

        private void txtProductName_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void AddProductForm_Load(object sender, EventArgs e)
        {

        }

        private void AddProductForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfirmAndExit(e);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            manageProductsForm.LoadProducts();
            manageProductsForm.Show();
            this.Hide();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string productName = txtProductName.Text.Trim();
            string category = txtCategory.Text.Trim();
            string price = txtPrice.Text.Trim();
            string stockQuantity = txtStockQuantity.Text.Trim();
            string supplier = txtSupplier.Text.Trim();
            string discount = txtDiscount.Text.Trim();

            // Text-only validation
            if (!InputValidator.IsTextOnly(productName))
            {
                MessageBox.Show("Product name must contain letters only.");
                return;
            }

            if (!InputValidator.IsTextOnly(category))
            {
                MessageBox.Show("Category must contain letters only.");
                return;
            }

            if (!InputValidator.IsTextOnly(supplier))
            {
                MessageBox.Show("Supplier must contain letters only.");
                return;
            }

            // Numeric validation
            if (!InputValidator.IsValidDouble(price))
            {
                MessageBox.Show("Price must be a valid number.");
                return;
            }

            if (!InputValidator.IsValidInt(stockQuantity))
            {
                MessageBox.Show("Stock quantity must be an integer.");
                return;
            }

            if (!InputValidator.IsValidInt(discount))
            {
                MessageBox.Show("Discount must be an integer.");
                return;
            }

            // Safe conversion after validation
            double parsedPrice = double.Parse(price);
            int parsedStockQty = int.Parse(stockQuantity);
            int parsedDiscount = int.Parse(discount);

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query =
                        "INSERT INTO products (product_name, category, price, stock_quantity, supplier, discount) " +
                        "VALUES (@productName, @category, @price, @stockCount, @supplier, @discount)";


                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    cmd.Parameters.AddWithValue("@productName", productName);
                    cmd.Parameters.AddWithValue("@category", category);
                    cmd.Parameters.AddWithValue("@price", parsedPrice);
                    cmd.Parameters.AddWithValue("@stockCount", parsedStockQty);
                    cmd.Parameters.AddWithValue("@supplier", supplier);
                    cmd.Parameters.AddWithValue("@discount", parsedDiscount);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Product successfull added.");

                manageProductsForm.LoadProducts();
                manageProductsForm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
