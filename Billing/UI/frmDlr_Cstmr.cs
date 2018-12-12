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
    public partial class frmDlr_Cstmr : Form
    {
        public frmDlr_Cstmr()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        DeaCustBLL dc = new DeaCustBLL();
        DeaCustDAL dcdal = new DeaCustDAL();
        userDAL uDal = new userDAL();
        private void btnAdd_Click(object sender, EventArgs e)
        {
            dc.type = cmbDlrCust.Text;
            dc.name = txtName.Text;
            dc.email = txtEmail.Text;
            dc.contact = txtContact.Text;
            dc.address = txtAddress.Text;
            dc.added_date = DateTime.Now;
            string loggedUsr = frmLogin.loggedIn;
            userBLL usr=uDal.GetIdFromUsername(loggedUsr);
            dc.added_by = usr.Id;

            bool success = dcdal.Insert(dc);
            if (success == true)
            {
                MassageBox mb = new MassageBox("Added Success", MsgType.success);
                mb.Show();
                //MessageBox.Show("Dealer/Customer Added Successfully...");
                Clear();

                DataTable dt = dcdal.Select();
                dgvDlr_Customer.DataSource = dt;
            }
            else {
                MassageBox mb = new MassageBox("Added Failed", MsgType.retry);
                mb.Show();
                //MessageBox.Show("Failed to ADD Dealer/Customer...!!! Try Again..");
            }
        }
        public void Clear()
        {
            txtDlr_CustID.Text = "";
            txtName.Text = "";
            txtEmail.Text = "";
            txtContact.Text = "";
            txtAddress.Text = "";
            txtSearch.Text = "";
        }

        private void frmDlr_Cstmr_Load(object sender, EventArgs e)
        {
            DataTable dt = dcdal.Select();
            dgvDlr_Customer.DataSource = dt;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            dc.Id = int.Parse(txtDlr_CustID.Text);
            dc.type = cmbDlrCust.Text;
            dc.name = txtName.Text;
            dc.email = txtEmail.Text;
            dc.contact = txtContact.Text;
            dc.address = txtAddress.Text;
            dc.added_date = DateTime.Now;
            string loggedUser = frmLogin.loggedIn;
            userBLL usr = uDal.GetIdFromUsername(loggedUser);
            dc.added_by = usr.Id;

            bool succes = dcdal.Update(dc);
            if (succes == true)
            {
                MassageBox mb = new MassageBox("Updated", MsgType.update);
                mb.Show();
               // MessageBox.Show("Dealer and Customer Updated Succesfully");
                Clear();
                DataTable dt = dcdal.Select();
                dgvDlr_Customer.DataSource = dt;
            }
            else
            {
                MassageBox mb = new MassageBox("Update Failed", MsgType.retry);
                mb.Show();
                //MessageBox.Show("Failed To Update Dealer and Customer!!!...Try Again");

            }
        }

        private void dgvDlr_Customer_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex=e.RowIndex;
            txtDlr_CustID.Text = dgvDlr_Customer.Rows[rowIndex].Cells[0].Value.ToString();
            cmbDlrCust.Text = dgvDlr_Customer.Rows[rowIndex].Cells[1].Value.ToString();
            txtName.Text = dgvDlr_Customer.Rows[rowIndex].Cells[2].Value.ToString();
            txtEmail.Text= dgvDlr_Customer.Rows[rowIndex].Cells[3].Value.ToString();
            txtContact.Text = dgvDlr_Customer.Rows[rowIndex].Cells[4].Value.ToString();
            txtAddress.Text = dgvDlr_Customer.Rows[rowIndex].Cells[5].Value.ToString();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            dc.Id = int.Parse(txtDlr_CustID.Text);
            bool succes = dcdal.Delete(dc);
            if (succes == true)
            {
                MassageBox mb = new MassageBox("Deleted", MsgType.delete);
                mb.Show();
                //MessageBox.Show("Dealer/Customer Deleted Succesfully");
                Clear();
                DataTable dt = dcdal.Select();
                dgvDlr_Customer.DataSource = dt;
            }
            else
            {
                MassageBox mb = new MassageBox("Delete Failed", MsgType.retry);
                mb.Show();
                //MessageBox.Show("Failed to Delete Dealer/Customer...!!! Try Again");
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keywords = txtSearch.Text;
            if (keywords != null)
            {
                DataTable dt = dcdal.Search(keywords);
                dgvDlr_Customer.DataSource = dt;
            }
            else
            {
                DataTable dt = dcdal.Select();
                dgvDlr_Customer.DataSource = dt;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            MassageBox mb = new MassageBox("Cleared", MsgType.clear);
            mb.Show();

            txtDlr_CustID.Text = "";
            txtName.Text = "";
            txtEmail.Text = "";
            txtContact.Text = "";
            txtAddress.Text = "";
            txtSearch.Text = "";
        }
    }
}
