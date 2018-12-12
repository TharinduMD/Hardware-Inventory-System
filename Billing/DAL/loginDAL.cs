using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Billing.BLL;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Billing.DAL
{
    class loginDAL
    {
        static string myconnstring = ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

        public bool loginCheck(loginBLL l) 
        {
            bool isSuccess = false;
            SqlConnection conn=new SqlConnection(myconnstring);
            try
            {
                string sql = "SELECT * FROM tbl_users WHERE username=@username AND password=@password AND user_type=@user_type";
                SqlCommand cmd = new SqlCommand(sql,conn);
                cmd.Parameters.AddWithValue("@username",l.username);
                cmd.Parameters.AddWithValue("@password", l.password);
                cmd.Parameters.AddWithValue("@user_type", l.user_type);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                if(dt.Rows.Count>0)
                {
                    isSuccess = true;  
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
    }
}
