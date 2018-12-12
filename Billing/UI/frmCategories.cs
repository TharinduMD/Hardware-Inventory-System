using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Billing.BLL;
using Billing.DAL;

namespace Billing.UI
{
    public partial class frmCategories : Form
    {
        categoriesBLL c = new categoriesBLL();
        categoriesDAL dal = new categoriesDAL();
        userDAL udal = new userDAL();

        public frmCategories()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            c.title = txtTitle.Text;
            c.discription = txtDescription.Text;
            c.added_date = DateTime.Now;
            string loggedUser = frmLogin.loggedIn;
            userBLL usr = udal.GetIdFromUsername(loggedUser);
            c.added_by = usr.Id;

            bool succes = dal.Insert(c);
            if (succes == true)
            {
                MassageBox mb = new MassageBox("Added Success", MsgType.success);
                mb.Show();
                //MessageBox.Show("New Categoty Inserted Succesfully");
                Clear();
                DataTable dt = dal.Select();
                dgvCategories.DataSource = dt;
            }
            else
            {
                MassageBox mb = new MassageBox("Added Failed", MsgType.retry);
                mb.Show();
                //MessageBox.Show("Failed To Insert New Catogory!!!...Try Again");
                
            }
        }
        public void Clear()
        {
            txtCategoryID.Text = "";
            txtDescription.Text = "";
            txtSearch.Text = "";
            txtTitle.Text = "";
        }

        private void frmCategories_Load(object sender, EventArgs e)
        {
            DataTable dt = dal.Select();
            dgvCategories.DataSource = dt;
        }

        private void dgvCategories_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int RowIndex = e.RowIndex;
            txtCategoryID.Text = dgvCategories.Rows[RowIndex].Cells[0].Value.ToString();
            txtTitle.Text = dgvCategories.Rows[RowIndex].Cells[1].Value.ToString();
            txtDescription.Text = dgvCategories.Rows[RowIndex].Cells[2].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            c.Id = int.Parse(txtCategoryID.Text);
            c.title = txtTitle.Text;
            c.discription = txtDescription.Text;
            c.added_date = DateTime.Now;
            string loggedUser = frmLogin.loggedIn;
            userBLL usr = udal.GetIdFromUsername(loggedUser);
            c.added_by = usr.Id;

            bool succes = dal.Update(c);
            if (succes == true)
            {
                MassageBox mb = new MassageBox("Updated", MsgType.update);
                mb.Show();
                //MessageBox.Show("Categoty Updated Succesfully");
                Clear();
                DataTable dt = dal.Select();
                dgvCategories.DataSource = dt;
            }
            else
            {
                MassageBox mb = new MassageBox("Update Failed", MsgType.retry);
                mb.Show();
                //MessageBox.Show("Failed To Update Catogory!!!...Try Again");

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            c.Id = int.Parse(txtCategoryID.Text);
            bool succes = dal.Delete(c);
            if (succes == true)
            {
                MassageBox mb = new MassageBox("Deleted", MsgType.delete);
                mb.Show();
                //MessageBox.Show("Categoty Deleted Succesfully");
                Clear();
                DataTable dt = dal.Select();
                dgvCategories.DataSource = dt;
            }
            else 
            {
                MassageBox mb = new MassageBox("Delete Failed", MsgType.retry);
                mb.Show();
               // MessageBox.Show("Failed to Delete Categorie...!!! Try Again");
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keywords = txtSearch.Text;
            if (keywords != null)
            {
                DataTable dt = dal.Search(keywords);
                dgvCategories.DataSource = dt;
            }
            else {
                DataTable dt = dal.Select();
                dgvCategories.DataSource = dt;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            MassageBox mb = new MassageBox("Cleared", MsgType.clear);
            mb.Show();
            txtCategoryID.Text = "";
            txtDescription.Text = "";
            txtSearch.Text = "";
            txtTitle.Text = "";
        }
    }
}
