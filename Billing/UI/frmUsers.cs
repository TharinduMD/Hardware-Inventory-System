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
    public partial class frmUsers : Form
    {
        userBLL u = new userBLL();
        userDAL dal = new userDAL();

        public frmUsers()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            
            u.first_name=txtFirstName.Text;
            u.last_name=txtLastName.Text;
            u.email=txtEmail.Text;
            u.username=txtUserName.Text;
            u.password=txtPassword.Text;
            u.contact=txtContact.Text;
            u.address=txtAddress.Text;
            u.gender=comboGender.Text;
            u.user_type=comboUserType.Text;
            u.added_date=DateTime.Now;
            string loggedUser = frmLogin.loggedIn;
            userBLL usr = dal.GetIdFromUsername(loggedUser);
            u.added_by = usr.Id;

            bool success = dal.Insert(u);
            if (success == true)
            {
                MassageBox mb = new MassageBox("Added Success", MsgType.success);
                mb.Show();
                //MessageBox.Show("User Added Successfully!!!");
                clear();
            }
            else
            {
                MassageBox mb = new MassageBox("Added Failed", MsgType.retry);
                mb.Show();
                //MessageBox.Show("Fail to Added!!!");
            }
            DataTable dt = dal.Select();
            dgvUser.DataSource = dt;
        }

        private void frmUsers_Load(object sender, EventArgs e)
        {
            DataTable dt = dal.Select();
            dgvUser.DataSource = dt;
        }
        private void clear()
        {
            txtUSERID.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtContact.Text = "";
            txtAddress.Text = "";
            comboGender.Text = "";
            comboUserType.Text = "";


        }

        private void dgvUser_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            txtUSERID.Text=dgvUser.Rows[rowIndex].Cells[0].Value.ToString();
            txtFirstName.Text = dgvUser.Rows[rowIndex].Cells[1].Value.ToString();
            txtLastName.Text = dgvUser.Rows[rowIndex].Cells[2].Value.ToString();
            txtEmail.Text = dgvUser.Rows[rowIndex].Cells[3].Value.ToString();
            txtUserName.Text = dgvUser.Rows[rowIndex].Cells[4].Value.ToString();
            txtPassword.Text = dgvUser.Rows[rowIndex].Cells[5].Value.ToString();
            txtContact.Text = dgvUser.Rows[rowIndex].Cells[6].Value.ToString();
            txtAddress.Text = dgvUser.Rows[rowIndex].Cells[7].Value.ToString();
            comboGender.Text = dgvUser.Rows[rowIndex].Cells[8].Value.ToString();
            comboUserType.Text = dgvUser.Rows[rowIndex].Cells[9].Value.ToString();


        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            u.Id = Convert.ToInt32(txtUSERID.Text);
            u.first_name = txtFirstName.Text;
            u.last_name = txtLastName.Text;
            u.email = txtEmail.Text;
            u.username = txtUserName.Text;
            u.password = txtPassword.Text;
            u.contact = txtContact.Text;
            u.address = txtAddress.Text;
            u.gender = comboGender.Text;
            u.user_type = comboUserType.Text;
            u.added_date = DateTime.Now;
            u.added_by = 1;

            bool success = dal.Update(u);
            if (success == true)
            {
                MassageBox mb = new MassageBox("Updated", MsgType.update);
                mb.Show();
                //MessageBox.Show("User Details Updated Successfully!!!");
                clear();
            }
            else
            {
                MassageBox mb = new MassageBox("Update Failed", MsgType.retry);
                mb.Show();
                //MessageBox.Show("Fail to Updated!!!");
            }
            DataTable dt = dal.Select();
            dgvUser.DataSource = dt;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            u.Id = Convert.ToInt32(txtUSERID.Text);
            bool success = dal.Delete(u);
            if (success == true)
            {
                MassageBox mb = new MassageBox("Deleted", MsgType.delete);
                mb.Show();
                //MessageBox.Show("User Details Deleted Successfully!!!");
                clear();
            }
            else
            {
                MassageBox mb = new MassageBox("Delete Failed", MsgType.retry);
                mb.Show();
                //MessageBox.Show("Fail to Delete!!!");
            }
            DataTable dt = dal.Select();
            dgvUser.DataSource = dt;    
            
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keywords = txtSearch.Text;
            if (keywords != null)
            {
                DataTable dt = dal.Search(keywords);
                dgvUser.DataSource = dt;
            }
            else {
                DataTable dt = dal.Select();
                dgvUser.DataSource = dt;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            MassageBox mb = new MassageBox("Cleared", MsgType.clear);
            mb.Show();

            txtUSERID.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtContact.Text = "";
            txtAddress.Text = "";
            comboGender.Text = "";
            comboUserType.Text = "";
        }
    }
}
