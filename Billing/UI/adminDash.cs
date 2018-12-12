using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Billing.UI
{
    public partial class rfmAdminDashboard : Form
    {
        public frmLogin lg;
        public rfmAdminDashboard()
        {
            InitializeComponent();
        }
        private void uSERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUsers user = new frmUsers();
            user.ShowDialog();
        }

        private void rfmAdminDashboard_Load(object sender, EventArgs e)
        {
            lblLoggedInUser.Text = frmLogin.loggedIn;
        }

        private void catoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCategories category = new frmCategories();
            category.ShowDialog();
        }

        private void productToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProducts product = new frmProducts();
            product.ShowDialog();
        }

        private void dealerCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDlr_Cstmr DeaCust = new frmDlr_Cstmr();
            DeaCust.ShowDialog();
        }

        private void transactionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTransactions transaction = new frmTransactions();
            transaction.ShowDialog();
        }

        private void storeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInventory inventory = new frmInventory();
            inventory.ShowDialog();
        }

        private void aboutUstoolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutUs us = new AboutUs();
            us.ShowDialog();
        }
    }
}
