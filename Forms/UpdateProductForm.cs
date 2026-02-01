using GreenLifeStore.Models;
using GreenLifeStore.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace GreenLifeStore.Forms
{
    public partial class UpdateProductForm : BaseForm
    {
        private int selectedProductId; // store product_id to use in update query

        private string connectionString = "server=localhost;database=greenlife;uid=root;pwd=1234;";

        private ManageProductsForm manageProductsForm;

        public UpdateProductForm(
        ManageProductsForm manageProductsForm, Product product)
        {
            InitializeComponent();
            this.manageProductsForm = manageProductsForm;

            // Save the selected product ID
            this.selectedProductId = product.Id;

            // Fill form fields
            txtProductName.Text = product.Name;
            txtCategory.Text = product.Category;
            txtPrice.Text = product.Price.ToString();
            txtStockQuantity.Text = product.StockQty.ToString();
            txtSupplier.Text = product.Supplier;
            txtDiscount.Text = product.Discount.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
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
                    string query = @"
                        UPDATE products 
                        SET product_name = @productName,
                        category = @category,
                        price = @price,
                        stock_quantity = @stock_quantity,
                        supplier = @supplier,
                        discount = @discount,
                        modified_date = NOW()
                        WHERE product_id = @id";

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@productName", productName);
                    cmd.Parameters.AddWithValue("@category", category);
                    cmd.Parameters.AddWithValue("@price", price);
                    cmd.Parameters.AddWithValue("@stock_quantity", stockQuantity);
                    cmd.Parameters.AddWithValue("@supplier", supplier);
                    cmd.Parameters.AddWithValue("@discount", discount);
                    cmd.Parameters.AddWithValue("@id", selectedProductId);

                    connection.Open();
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Product updated successfully.", "Update Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    manageProductsForm.LoadProducts();
                    manageProductsForm.Show();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void UpdateProductForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfirmAndExit(e);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            manageProductsForm.LoadProducts();
            manageProductsForm.Show();
            this.Hide();
        }
    }
}
