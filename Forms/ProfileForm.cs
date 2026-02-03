using GreenLifeStore.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace GreenLifeStore.Forms
{
    public partial class ProfileForm : BaseForm
    {
        private int customerId;

        private CustomerDashboardForm customerDashboardForm;

        public ProfileForm(CustomerDashboardForm customerDashboardForm, int customerId)
        {
            InitializeComponent();
            this.customerDashboardForm = customerDashboardForm;
            this.customerId = customerId;
        }

        private void ProfileForm_Load(object sender, EventArgs e)
        {
            LoadCustomerProfile();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            customerDashboardForm.Show();
            this.Hide();
        }

        private void ProfileForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfirmAndExit(e);
        }

        private void LoadCustomerProfile()
        {
            using (MySqlConnection connection = new MySqlConnection(DatabaseConfig.ConnectionString))
            {
                string query = @"
            SELECT name, email, phone, address
            FROM customers
            WHERE customer_id = @id AND active_status = 1";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", customerId);

                connection.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txtName.Text = reader["name"].ToString();
                        txtEmail.Text = reader["email"].ToString();
                        txtPhone.Text = reader["phone"].ToString();
                        txtAddress.Text = reader["address"].ToString();
                    }
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string address = txtAddress.Text.Trim();
            string currentPassword = txtCurrentPassword.Text;
            string newPassword = txtNewPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            if (!InputValidator.IsValidContactNumber(phone))
            {
                MessageBox.Show("Please enter a valid contact number.");
                return;
            }

            if (string.IsNullOrEmpty(name) ||
                string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(phone) ||
                string.IsNullOrEmpty(address) ||
                string.IsNullOrEmpty(currentPassword))
            {
                MessageBox.Show("All fields including current password are required.");
                return;
            }

            if (!string.IsNullOrEmpty(newPassword) && newPassword != confirmPassword)
            {
                MessageBox.Show("New passwords do not match.");
                return;
            }

            try
            {
                using (MySqlConnection connection = new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    connection.Open();

                    // fetch stored hash password
                    string passwordQuery = "SELECT password FROM customers WHERE customer_id = @id";
                    MySqlCommand passwordCmd = new MySqlCommand(passwordQuery, connection);
                    passwordCmd.Parameters.AddWithValue("@id", customerId);

                    string storedHash = passwordCmd.ExecuteScalar()?.ToString();

                    if (storedHash == null ||
                        !PasswordHasher.VerifyPassword(currentPassword, storedHash))
                    {
                        MessageBox.Show("Current password is incorrect.");
                        return;
                    }

                    // then update
                    string updateQuery;
                    MySqlCommand updateCmd;

                    if (!string.IsNullOrEmpty(newPassword))
                    {
                        string newHashedPassword = PasswordHasher.HashPassword(newPassword);

                        updateQuery = @"
                            UPDATE customers
                            SET name = @name,
                                email = @email,
                                phone = @phone,
                                address = @address,
                                password = @password,
                                modified_date = NOW()
                            WHERE customer_id = @id";

                        updateCmd = new MySqlCommand(updateQuery, connection);
                        updateCmd.Parameters.AddWithValue("@password", newHashedPassword);
                    }
                    else
                    {
                        updateQuery = @"
                            UPDATE customers
                            SET name = @name,
                                email = @email,
                                phone = @phone,
                                address = @address,
                                modified_date = NOW()
                            WHERE customer_id = @id";

                        updateCmd = new MySqlCommand(updateQuery, connection);
                    }

                    updateCmd.Parameters.AddWithValue("@name", name);
                    updateCmd.Parameters.AddWithValue("@phone", phone);
                    updateCmd.Parameters.AddWithValue("@email", email);
                    updateCmd.Parameters.AddWithValue("@address", address);
                    updateCmd.Parameters.AddWithValue("@id", customerId);

                    updateCmd.ExecuteNonQuery();
                }

                MessageBox.Show("Profile updated successfully.");
                customerDashboardForm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update failed: " + ex.Message);
            }
        }
    }
}