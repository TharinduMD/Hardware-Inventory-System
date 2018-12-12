using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Billing.DAL;
using Billing.BLL;

namespace Billing.UI
{
    public partial class frmProducts : Form
    {
        public frmProducts()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        categoriesDAL cdal = new categoriesDAL();
        productsDAL pdal = new productsDAL();
        productsBLL p = new productsBLL();
        userDAL udal = new userDAL();
        private void frmProducts_Load(object sender, EventArgs e)
        {
            DataTable categoriesDT = cdal.Select();
            cmbCategory.DataSource = categoriesDT;
            cmbCategory.DisplayMember = "title";
            cmbCategory.ValueMember = "title";

            DataTable dt = pdal.Select();
            dgvProducts.DataSource = dt;


        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            p.name = txtName.Text;
            p.category = cmbCategory.Text;
            p.description = txtDescription.Text;
            p.rate = decimal.Parse(txtRate.Text);
            p.qty = 0;
            p.added_date = DateTime.Now;
            string loggedUsr = frmLogin.loggedIn;
            userBLL usr = udal.GetIdFromUsername(loggedUsr);
            p.added_by = usr.Id;

            bool success = pdal.Insert(p);
            if (success == true)
            {
                MassageBox mb = new MassageBox("Added Success",MsgType.success);
                mb.Show();
                //MessageBox.Show("Product Added Successfully...");
                Clear();
                DataTable dt = pdal.Select();
                dgvProducts.DataSource = dt;
            }
            else {
                MassageBox mb = new MassageBox("Added Failed", MsgType.retry);
                mb.Show();
                //MessageBox.Show("Failed To Added Product!!!...Try again...");
            }
        }
        public void Clear()
        {
            txtProductID.Text = "";
            txtName.Text = "";
            txtDescription.Text = "";
            txtRate.Text = "";
            txtSearch.Text = "";
        }

        private void dgvProducts_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            txtProductID.Text = dgvProducts.Rows[rowIndex].Cells[0].Value.ToString();
            txtName.Text = dgvProducts.Rows[rowIndex].Cells[1].Value.ToString();
            cmbCategory.Text = dgvProducts.Rows[rowIndex].Cells[2].Value.ToString();
            txtDescription.Text = dgvProducts.Rows[rowIndex].Cells[3].Value.ToString();
            txtRate.Text = dgvProducts.Rows[rowIndex].Cells[4].Value.ToString();
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            p.Id = int.Parse(txtProductID.Text);
            p.name = txtName.Text;
            p.category = cmbCategory.Text;
            p.description = txtDescription.Text;
            p.rate = decimal.Parse(txtRate.Text);
            p.added_date = DateTime.Now;
            string loggedUsr = frmLogin.loggedIn;
            userBLL usr = udal.GetIdFromUsername(loggedUsr);
            p.added_by = usr.Id;

            bool success = pdal.Update(p);
            if (success == true)
            {
                MassageBox mb = new MassageBox("Updated", MsgType.update);
                mb.Show();
                ///MessageBox.Show("Product Updated Successfully...");
                Clear();
                DataTable dt = pdal.Select();
                dgvProducts.DataSource = dt;

            }
            else
            {
                MassageBox mb = new MassageBox("Update Failed", MsgType.retry);
                mb.Show();
                //MessageBox.Show("Failed To Update Product!!!...Try again...");
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            p.Id = int.Parse(txtProductID.Text);
            bool success = pdal.Delete(p);
            if (success == true)
            {
                MassageBox mb = new MassageBox("Deleted", MsgType.delete);
                mb.Show();
                //MessageBox.Show("Product Deleted Successfull...");
                Clear();
                DataTable dt = pdal.Select();
                dgvProducts.DataSource = dt;
            }
            else
            {
                MassageBox mb = new MassageBox("Delete Failed", MsgType.retry);
                mb.Show();
                //MessageBox.Show("Failed to Delete Product...!!! Try again...");
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keywords = txtSearch.Text;
            if (keywords != null)
            {
                DataTable dt = pdal.Search(keywords);
                dgvProducts.DataSource = dt;
            }
            else
            {
                DataTable dt = pdal.Select();
                dgvProducts.DataSource = dt;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            MassageBox mb = new MassageBox("Cleared", MsgType.clear);
            mb.Show();

            txtProductID.Text = "";
            txtName.Text = "";
            txtDescription.Text = "";
            txtRate.Text = "";
            txtSearch.Text = "";
        }
    }
}
