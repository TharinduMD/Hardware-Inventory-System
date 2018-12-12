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
    class transactionsDAL
    {
        static string myconnstring = ConfigurationManager.ConnectionStrings["constring"].ConnectionString;
        #region Insert Transaction Method
        public bool Insert_Transaction(transactionsBLL t,out int transactionID)
        {
            bool isSuccess = false;
            transactionID = -1;
            SqlConnection conn = new SqlConnection(myconnstring);
            //  DataTable dt = new DataTable();
            try
            {
                string sql = "INSERT INTO tbl_transactions(type,dea_cust_id,grandTotal,transaction_date,tax,discount,added_by)VALUES(@type,@dea_cust_id,@grandTotal,@transaction_date,@tax,@discount,@added_by); SELECT @@IDENTITY;";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@type", t.type);
                cmd.Parameters.AddWithValue("@dea_cust_id", t.dea_cust_id);
                cmd.Parameters.AddWithValue("@grandTotal", t.grandTotal);
                cmd.Parameters.AddWithValue("@transaction_date", t.transaction_date);
                cmd.Parameters.AddWithValue("@tax", t.tax);
                cmd.Parameters.AddWithValue("@discount", t.discount);
                cmd.Parameters.AddWithValue("@added_by", t.added_by);
                
                conn.Open();

                object o = cmd.ExecuteScalar();
                if (o!=null)
                {
                    transactionID = int.Parse(o.ToString());
                    isSuccess = true;
                }
                else {
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
        #endregion
        #region Method to Display all the Transactions
        public DataTable DisplayAllTransactions() {
            SqlConnection conn = new SqlConnection(myconnstring);
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT *FROM tbl_transactions";
                SqlCommand cmd = new SqlCommand(sql,conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally {
                conn.Close();
            }
            return dt;

        }
        #endregion
        #region method to display transaction based on transaction type
        public DataTable DisplayTransactionByType(string type) {
            SqlConnection conn = new SqlConnection(myconnstring);
            DataTable dt=new DataTable();
            try
            {
                string sql="SELECT * FROM tbl_transactions WHERE type='"+type+"'";
                 SqlCommand cmd = new SqlCommand(sql,conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally {
                conn.Close();
            }

            return dt;
        }
        #endregion
    }
}
