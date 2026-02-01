using System;
using System.Windows.Forms;

namespace GreenLifeStore
{
    public class BaseForm : Form
    {

        private static bool isExiting = false;

        protected void ConfirmAndExit(FormClosingEventArgs e)
        {
            if (isExiting)
                return;

            DialogResult result = MessageBox.Show(
                "Are you sure you want to exit the application?",
                "Exit Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                isExiting = true;
                Application.Exit();
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // BaseForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "BaseForm";
            this.ResumeLayout(false);

        }
    }
}