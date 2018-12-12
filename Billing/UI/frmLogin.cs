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
using Billing.UI;

namespace Billing.UI
{
    public partial class frmLogin : Form
    {

        public frmLogin()
        {
            InitializeComponent();
            rfmAdminDashboard ad = new rfmAdminDashboard();
            ad.lg = this;

        }
        loginBLL l = new loginBLL();
        loginDAL dal = new loginDAL();
        public static string loggedIn;
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            l.username = txtUserName.Text.Trim();
            l.password = txtPasword.Text.Trim();
            l.user_type = cmbUserType.selectedValue.Trim();

            bool success = dal.loginCheck(l);
            if (success == true)
            {
                loggedIn = l.username;
                switch (l.user_type) 
                {
                    case "Admin": 
                        {
                            rfmAdminDashboard admin = new rfmAdminDashboard();
                            admin.Show();
                            this.Hide();
                            
                        }
                        break;
                    case "User":
                        {
                            frmUserDashboard user = new frmUserDashboard();
                            user.Show();
                            this.Hide();
                        }
                        break;
                    default:
                        {
                            MassageBox mb = new MassageBox("Invalid User Type", MsgType.clear);
                            mb.Show();
                            //MessageBox.Show("Invalid User Type!!!");
                        }
                        break;
                }
            }
            else
            {
                MassageBox mb = new MassageBox("Login Failed\nRetry", MsgType.retry);
                mb.Show();
                //MessageBox.Show("Login Fail !!!....Try Again");
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
