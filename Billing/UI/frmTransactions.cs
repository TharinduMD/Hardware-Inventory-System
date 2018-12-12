using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Billing.DAL;

namespace Billing.UI
{
    public partial class frmTransactions : Form
    {
        public frmTransactions()
        {
            InitializeComponent();
        }
        transactionsDAL tdal = new transactionsDAL();
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void frmTransactions_Load(object sender, EventArgs e)
        {
            DataTable dt = tdal.DisplayAllTransactions();
            dgvTransaction.DataSource = dt;
        }

        private void cmbTransaction_SelectedIndexChanged(object sender, EventArgs e)
        {
            string type = cmbTransaction.Text;
            DataTable dt = tdal.DisplayTransactionByType(type);
            dgvTransaction.DataSource = dt;
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            DataTable dt = tdal.DisplayAllTransactions();
            dgvTransaction.DataSource = dt;
        }
    }
}
