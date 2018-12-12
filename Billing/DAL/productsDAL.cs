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
    class productsDAL
    {
        static string myconnstring = ConfigurationManager.ConnectionStrings["constring"].ConnectionString;
        #region Select Method
        public DataTable Select()
        {
            SqlConnection conn = new SqlConnection(myconnstring);
            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT * FROM tbl_products";
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
        #region Insert New Product
        public bool Insert(productsBLL c)
        {
            bool isSucces = false;
            SqlConnection conn = new SqlConnection(myconnstring);
            try
            {
                string sql = "INSERT INTO tbl_products(name,category,description,rate,qty,added_date,added_by)VALUES(@name,@category,@description,@rate,@qty,@added_date,@added_by)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name",c.name);
                cmd.Parameters.AddWithValue("@category", c.category);
                cmd.Parameters.AddWithValue("@description", c.description);
                cmd.Parameters.AddWithValue("@rate", c.rate);
                cmd.Parameters.AddWithValue("@qty", c.qty);
                cmd.Parameters.AddWithValue("@added_date", c.added_date);
                cmd.Parameters.AddWithValue("@added_by", c.added_by);
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
        public bool Update(productsBLL p)
        {
            bool isSucces = false;
            SqlConnection conn = new SqlConnection(myconnstring);
            try
            {
                string sql = "UPDATE tbl_products SET name=@name,category=@category,description=@description,rate=@rate,added_date=@added_date,added_by=@added_by WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", p.name);
                cmd.Parameters.AddWithValue("@category", p.category);
                cmd.Parameters.AddWithValue("@description", p.description);
                cmd.Parameters.AddWithValue("@rate", p.rate);
                cmd.Parameters.AddWithValue("@qty", p.qty);
                cmd.Parameters.AddWithValue("@added_date", p.added_date);
                cmd.Parameters.AddWithValue("@added_by", p.added_by);
                cmd.Parameters.AddWithValue("@Id", p.Id);
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
        public bool Delete(productsBLL c)
        {
            bool isSucces = false;
            SqlConnection conn = new SqlConnection(myconnstring);
            try
            {
                string sql = "DELETE FROM tbl_products WHERE Id=@Id";
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
        #region Search Products
        public DataTable Search(string keywords)
        {
            SqlConnection conn = new SqlConnection(myconnstring);
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT * FROM tbl_products WHERE Id LIKE '%" + keywords + "%' OR name LIKE '%" + keywords + "%' OR category LIKE '%" + keywords + "%'";
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
        #region Search product in Transaction Module
        public productsBLL GetProductsFromTransaction(string keyword)
        {
            productsBLL p = new productsBLL();
            SqlConnection conn = new SqlConnection(myconnstring);
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT name,rate,qty FROM tbl_products WHERE Id LIKE '%"+keyword+"%' OR name LIKE '%"+keyword+"%'";
                SqlDataAdapter adaptor = new SqlDataAdapter(sql, conn);
                conn.Open();
                adaptor.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    p.name = dt.Rows[0]["name"].ToString();
                    p.rate = decimal.Parse(dt.Rows[0]["rate"].ToString());
                    p.qty = decimal.Parse(dt.Rows[0]["qty"].ToString());  
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

            return p;
        }
        #endregion
        #region Method to get ProductID based on product name
        public productsBLL GetProductIdFromName(string ProductName)
        {
            productsBLL p = new productsBLL();
            SqlConnection conn = new SqlConnection(myconnstring);
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT Id FROM tbl_products WHERE name ='" + ProductName + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                conn.Open();
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    p.Id = int.Parse(dt.Rows[0]["Id"].ToString());
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
            return p;

        }
        #endregion
        #region Method to Get Current Quentity From The Database based on the Product Id
        public decimal GetProductQty(int productID) 
        {
            SqlConnection conn = new SqlConnection(myconnstring);
            decimal qty = 0;
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT qty FROM tbl_products WHERE Id ="+productID;
                SqlCommand cmd = new SqlCommand(sql,conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();

                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    qty = decimal.Parse(dt.Rows[0]["qty"].ToString());
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

            return qty;
        }
        #endregion
        #region Method to UPDATE Quntity
        public bool UpdateQuantity(int productID,decimal Qty)
        {
            bool success = false;
            SqlConnection conn = new SqlConnection(myconnstring);
            try
            {
                string sql = "UPDATE tbl_products SET qty=@qty WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(sql,conn);

                cmd.Parameters.AddWithValue("@qty",Qty);
                cmd.Parameters.AddWithValue("@Id",productID);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    success = true;
                }
                else
                {
                    success = false;
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
            return success;
        }
        #endregion
        #region Method to Increase product
        public bool IncreaseProduct(int productID,decimal increaseQTY)
        {
            bool success = false;
            SqlConnection conn = new SqlConnection(myconnstring);
            try
            {
                decimal currentQty = GetProductQty(productID);
                decimal NewQty = currentQty + increaseQTY;
                success = UpdateQuantity(productID,NewQty);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return success; 
        }
        #endregion
        #region Method to Decrease Product
        public bool DecreaseProduct(int productID,decimal Qty)
        {
            bool success = false;
            SqlConnection conn = new SqlConnection(myconnstring);
            try
            {
                decimal currentQty = GetProductQty(productID);
                decimal NewQty = currentQty - Qty;
                success = UpdateQuantity(productID,NewQty);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return success;
        }
        #endregion
        #region display products based on categories
        public DataTable DisplayProductsByCatogory(String category) {
            SqlConnection conn = new SqlConnection(myconnstring);
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT * FROM tbl_products WHERE category ='" + category + "'";
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

    }
}
