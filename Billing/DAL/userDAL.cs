using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Billing.BLL;

namespace Billing.DAL
{
    class userDAL
    {
        static string myconnstring =ConfigurationManager.ConnectionStrings["constring"].ConnectionString;
        #region Select Data from Database
        public DataTable Select() 
        {
            SqlConnection con = new SqlConnection(myconnstring);
            DataTable dt = new DataTable();
            try
            {
                String sql = "SELECT * FROM tbl_users ";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                con.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
              return dt;
        }
        #endregion
        #region Insert Data in Database
        public bool Insert(userBLL c)
        {
            bool isSuccess = false;
            SqlConnection Conn = new SqlConnection(myconnstring);
            try
            {
                String sql = "INSERT INTO tbl_users (first_name,last_name,email,username,password,contact,address,gender,user_type,added_date,added_by) VALUES (@first_name,@last_name,@email,@username,@password,@contact,@address,@gender,@user_type,@added_date,@added_by)";

                SqlCommand cmd = new SqlCommand(sql, Conn);
                cmd.Parameters.AddWithValue("@first_name", c.first_name);
                cmd.Parameters.AddWithValue("@last_name", c.last_name);
                cmd.Parameters.AddWithValue("@email", c.email);
                cmd.Parameters.AddWithValue("@username", c.username);
                cmd.Parameters.AddWithValue("@password", c.password);
                cmd.Parameters.AddWithValue("@contact", c.contact);
                cmd.Parameters.AddWithValue("@address", c.address);
                cmd.Parameters.AddWithValue("@gender", c.gender);
                cmd.Parameters.AddWithValue("@user_type", c.user_type);
                cmd.Parameters.AddWithValue("@added_date", c.added_date);
                cmd.Parameters.AddWithValue("@added_by", c.added_by);
                Conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                Conn.Close();
            }
            return isSuccess;
        }
        #endregion
        #region Update Data in Database
        public bool Update(userBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstring);
            try
            {
                string sql = "UPDATE tbl_users SET first_name=@first_name,last_name=@last_name,email=@email,username=@username,password=@password,contact=@contact,address=@address,gender=@gender,user_type=@user_type,added_date=@added_date,added_by=@added_by WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(sql,conn);
                cmd.Parameters.AddWithValue("@first_name", u.first_name);
                cmd.Parameters.AddWithValue("@last_name", u.last_name);
                cmd.Parameters.AddWithValue("@email", u.email);
                cmd.Parameters.AddWithValue("@username", u.username);
                cmd.Parameters.AddWithValue("@password", u.password);
                cmd.Parameters.AddWithValue("@contact", u.contact);
                cmd.Parameters.AddWithValue("@address", u.address);
                cmd.Parameters.AddWithValue("@gender", u.gender);
                cmd.Parameters.AddWithValue("@user_type", u.user_type);
                cmd.Parameters.AddWithValue("@added_date", u.added_date);
                cmd.Parameters.AddWithValue("@added_by", u.added_by);
                cmd.Parameters.AddWithValue("@Id", u.Id);
                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = true;
                }

            }
            catch (Exception ex)
            {

                
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        #endregion
        #region Delete Data from Database
        public bool Delete(userBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstring);
            try
            {
                string sql = "DELETE FROM tbl_users WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                
                cmd.Parameters.AddWithValue("@Id", u.Id);
                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = true;
                }

            }
            catch (Exception ex)
            {


            }
            finally
            {

            }
            return isSuccess;
        }
        #endregion
        #region Search user in Database using keywords
        public DataTable Search(string keywords)
        {
            SqlConnection con = new SqlConnection(myconnstring);
            DataTable dt = new DataTable();
            try
            {
                String sql = "SELECT * FROM tbl_users WHERE Id LIKE '%" + keywords + "%' OR first_name LIKE '%" + keywords + "%' OR last_name LIKE '%" + keywords + "%' OR username LIKE '%" + keywords + "%'";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                con.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return dt;
        }
        #endregion
        #region Getting User Id From Username
        public userBLL GetIdFromUsername(string username) {
            userBLL u = new userBLL();
            SqlConnection conn = new SqlConnection(myconnstring);
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT id FROM tbl_users WHERE username='"+username+"'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql,conn);
                conn.Open();
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                { 
                    u.Id=int.Parse(dt.Rows[0]["id"].ToString());
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
            return u;
        }
        #endregion
    }
}
