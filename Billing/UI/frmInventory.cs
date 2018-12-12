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
    public partial class frmInventory : Form
    {
        public frmInventory()
        {
            InitializeComponent();
        }
        categoriesDAL cdal = new categoriesDAL();
        productsDAL pdal = new productsDAL();

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void frmInventory_Load(object sender, EventArgs e)
        {
            DataTable cDt = cdal.Select();
            cmbCategories.DataSource = cDt;
            cmbCategories.DisplayMember = "title";
            cmbCategories.ValueMember = "title";
            DataTable pdt = pdal.Select();
            dgvProducts.DataSource = pdt;

        }

        private void cmbCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            string category = cmbCategories.Text;
            DataTable dt = pdal.DisplayProductsByCatogory(category);
            dgvProducts.DataSource = dt;
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            DataTable dt = pdal.Select();
            dgvProducts.DataSource = dt;
        }
    }
}
