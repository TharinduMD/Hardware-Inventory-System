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
    class DeaCustDAL
    {
        static string myconnstring = ConfigurationManager.ConnectionStrings["constring"].ConnectionString;
        #region Select Method
        public DataTable Select()
        {
            SqlConnection conn = new SqlConnection(myconnstring);
            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT * FROM tbl_dea_cust";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }
        #endregion
        #region Insert New DeaCust
        public bool Insert(DeaCustBLL d)
        {
            bool isSucces = false;
            SqlConnection conn = new SqlConnection(myconnstring);
            try
            {
                string sql = "INSERT INTO tbl_dea_cust(type,name,email,contact,address,added_date,added_by)VALUES(@type,@name,@email,@contact,@address,@added_date,@added_by)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@type", d.type);
                cmd.Parameters.AddWithValue("@name", d.name);
                cmd.Parameters.AddWithValue("@email", d.email);
                cmd.Parameters.AddWithValue("@contact", d.contact);
                cmd.Parameters.AddWithValue("@address", d.address);
                cmd.Parameters.AddWithValue("@added_date", d.added_date);
                cmd.Parameters.AddWithValue("@added_by", d.added_by);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSucces = true;
                }
                else
                {
                    isSucces = false;
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

            return isSucces;
        }

        #endregion
        #region Update Method
        public bool Update(DeaCustBLL d)
        {
            bool isSucces = false;
            SqlConnection conn = new SqlConnection(myconnstring);
            try
            {
                string sql = "UPDATE tbl_dea_cust SET type=@type,name=@name,email=@email,contact=@contact,address=@address,added_date=@added_date,added_by=@added_by WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@type", d.type);
                cmd.Parameters.AddWithValue("@name", d.name);
                cmd.Parameters.AddWithValue("@email", d.email);
                cmd.Parameters.AddWithValue("@contact", d.contact);
                cmd.Parameters.AddWithValue("@address", d.address);
                cmd.Parameters.AddWithValue("@added_date", d.added_date);
                cmd.Parameters.AddWithValue("@added_by", d.added_by);
                cmd.Parameters.AddWithValue("@Id", d.Id);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSucces = true;
                }
                else
                {
                    isSucces = false;
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
            return isSucces;
        }
        #endregion
        #region Delete Method
        public bool Delete(DeaCustBLL c)
        {
            bool isSucces = false;
            SqlConnection conn = new SqlConnection(myconnstring);
            try
            {
                string sql = "DELETE FROM tbl_dea_cust WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", c.Id);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSucces = true;
                }
                else
                {
                    isSucces = false;
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
            return isSucces;
        }
        #endregion
        #region Search Functionality
        public DataTable Search(string keywords)
        {
            SqlConnection conn = new SqlConnection(myconnstring);
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT * FROM tbl_dea_cust WHERE Id LIKE '%" + keywords + "%' OR type LIKE '%" + keywords + "%' OR name LIKE '%" + keywords + "%' OR email LIKE '%" + keywords + "%' OR contact LIKE '%" + keywords + "%' OR address LIKE '%" + keywords + "%'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
        #endregion
        #region Method to Search Dealer or Customer for Transaction Module
        public DeaCustBLL SearchDealerCustomerForTransation(string keyword)
        {
            DeaCustBLL dc = new DeaCustBLL();
            SqlConnection conn = new SqlConnection(myconnstring);
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT name,email,contact,address from tbl_dea_cust WHERE Id LIKE '%" + keyword + "%' OR name LIKE '%" + keyword + "%'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql,conn);
                conn.Open();
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    dc.name = dt.Rows[0]["name"].ToString();
                    dc.email = dt.Rows[0]["email"].ToString();
                    dc.contact = dt.Rows[0]["contact"].ToString();
                    dc.address = dt.Rows[0]["address"].ToString();
                     
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
            return dc;
        }
        #endregion
        #region Method to get ID of Dealer of Customer Based on Name
        public DeaCustBLL GetDeaCustIdFromName(string Name)
        {
            DeaCustBLL dc = new DeaCustBLL();
            SqlConnection conn = new SqlConnection(myconnstring);
            DataTable dt = new DataTable();
            try
            {
                string sql="SELECT Id FROM tbl_dea_cust WHERE name ='"+Name+"'";
                SqlDataAdapter adapter=new SqlDataAdapter(sql,conn);
                conn.Open();
                adapter.Fill(dt);
                if(dt.Rows.Count>0)
                {
                    dc.Id=int.Parse(dt.Rows[0]["Id"].ToString());
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
            return dc;

        }
        #endregion
    }
}
