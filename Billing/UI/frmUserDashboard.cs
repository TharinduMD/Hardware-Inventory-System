using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Billing.UI;

namespace Billing
{
    public partial class frmUserDashboard : Form
    {
        public frmUserDashboard()
        {
            InitializeComponent();
        }
        public static string transactionType;

        private void frmUserDashboard_Load(object sender, EventArgs e)
        {
            lblLoggedInUser.Text = frmLogin.loggedIn;
        }

        private void dealerAndCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDlr_Cstmr DlrCst = new frmDlr_Cstmr();
            DlrCst.ShowDialog();
        }

        private void puschaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            transactionType = "PURCHASE";
            frmPurchaseAndSales purchase = new frmPurchaseAndSales();
            purchase.ShowDialog();
            
        }

        private void salesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            transactionType = "SALES";
            frmPurchaseAndSales sales = new frmPurchaseAndSales();
            sales.ShowDialog();
           
        }

        private void inventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInventory inventory = new frmInventory();
            inventory.ShowDialog();
        }

        private void aboutUstoolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutUs au = new AboutUs();
            au.ShowDialog();
        }
    }
}
