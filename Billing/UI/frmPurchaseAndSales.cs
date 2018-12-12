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
using System.Transactions;

namespace Billing.UI
{
    public partial class frmPurchaseAndSales : Form
    {
        public frmPurchaseAndSales()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        DeaCustDAL dcDAL = new DeaCustDAL();
        productsDAL pdal = new productsDAL();
        userDAL uDAL = new userDAL();
        transactionsDAL tDAL = new transactionsDAL();
        transactionDetailDAL tdDAL = new transactionDetailDAL();
        DataTable transactionDT = new DataTable();
        private void frmPurchaseAndSales_Load(object sender, EventArgs e)
        {
            string type = frmUserDashboard.transactionType;
            lblTop.Text = type;
            transactionDT.Columns.Add("Product Name");
            transactionDT.Columns.Add("Rate");
            transactionDT.Columns.Add("Quantity");
            transactionDT.Columns.Add("Total");

        
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text;
            if (keyword == "")
            {
                txtName.Text = "";
                txtEmail.Text = "";
                txtContact.Text = "";
                txtAddress.Text = "";
                return;
            }
            DeaCustBLL dc = dcDAL.SearchDealerCustomerForTransation(keyword);
            txtName.Text = dc.name;
            txtEmail.Text = dc.email;
            txtContact.Text = dc.contact;
            txtAddress.Text = dc.address;
        }

        private void txtSearchProduct_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearchProduct.Text;
            if (keyword == "")
            {
                txtNamePro.Text = "";
                txtInventory.Text = "";
                txtRate.Text = "";
                txtQty.Text = "";
                return;
            }
            productsBLL p = pdal.GetProductsFromTransaction(keyword);
            txtNamePro.Text = p.name;
            txtInventory.Text = p.qty.ToString();
            txtRate.Text = p.rate.ToString();

        }

        private void ADD_Click(object sender, EventArgs e)
        {
            try
            {
                string productName = txtNamePro.Text;
                decimal rate = decimal.Parse(txtRate.Text);
                decimal qty = decimal.Parse(txtQty.Text);

                decimal Total = rate * qty;

                decimal subTotal = decimal.Parse(txtSubTotal.Text);
                subTotal = subTotal + Total;

                if (productName == "")
                {
                    MassageBox mb = new MassageBox("Select The Product", MsgType.retry);
                    mb.Show();
                    //MessageBox.Show("Select The Product First and Try Again...");
                }
                else
                {
                    transactionDT.Rows.Add(productName, rate, qty, Total);
                    dgvAddedProducts.DataSource = transactionDT;

                    txtSubTotal.Text = subTotal.ToString();

                    txtSearchProduct.Text = "";
                    txtNamePro.Text = "";
                    txtInventory.Text = "0.00";
                    txtRate.Text = "0.00";
                    txtQty.Text = "0.00";
                }
            }
            catch(Exception)
            {
                MassageBox mb = new MassageBox("ERROR", MsgType.clear);
                mb.BackColor = Color.Crimson;
                mb.Show();
            }
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string value = txtDiscount.Text;
                if (value == "")
                {
                    MassageBox mb = new MassageBox("Please Enter Discount", MsgType.retry);
                    mb.Show();
                    //MessageBox.Show("Please Enter Discount First!!!...");
                }
                else
                {
                    decimal subtotal = decimal.Parse(txtSubTotal.Text);
                    decimal discount = decimal.Parse(txtDiscount.Text);
                    decimal grandTotal = ((100 - discount) / 100) * subtotal;

                    txtGrandTotal.Text = grandTotal.ToString();
                }
            }catch(Exception)
            {
                MassageBox mb = new MassageBox("ERROR", MsgType.clear);
                mb.BackColor = Color.Crimson;
                mb.Show();
            }
        }

        private void txtVat_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string check = txtGrandTotal.Text;
                if (check == "")
                {
                    MassageBox mb = new MassageBox("Please Enter Discount", MsgType.retry);
                    mb.Show();
                    //MessageBox.Show("Input Discount First to Calculate Grand Total...!!!");
                }
                else
                {
                    decimal previousGT = decimal.Parse(txtGrandTotal.Text);
                    decimal vat = decimal.Parse(txtVat.Text);
                    decimal grandTotalWithVat = ((100 + vat) / 100) * previousGT;
                    txtGrandTotal.Text = grandTotalWithVat.ToString();
                }
            }
            catch(Exception)
            {
                MassageBox mb = new MassageBox("ERROR", MsgType.clear);
                mb.BackColor = Color.Crimson;
                mb.Show();
            }
        }

        private void txtPaidAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string paid = txtPaidAmount.Text;
                if (paid == "")
                {
                    MassageBox mb = new MassageBox("Enter Paid Amount", MsgType.retry);
                    mb.Show();
                    //MessageBox.Show("Please Enter Paid Amount First...!!!");
                }
                else
                {
                    //decimal returnVal = decimal.Parse(txtReturnAmount.Text);
                    decimal pid = decimal.Parse(txtPaidAmount.Text);
                    decimal gt = decimal.Parse(txtGrandTotal.Text);
                    decimal ret = (pid - gt);
                    txtReturnAmount.Text = ret.ToString();
                }
            }
            catch(Exception)
            {
                MassageBox mb = new MassageBox("ERROR", MsgType.clear);
                mb.BackColor = Color.Crimson;
                mb.Show();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try 
            {
                transactionsBLL transaction = new transactionsBLL();
                transaction.type = lblTop.Text;
                string deaCustName = txtName.Text;

                DeaCustBLL dc = dcDAL.GetDeaCustIdFromName(deaCustName);
                transaction.dea_cust_id = dc.Id;
                transaction.grandTotal = Math.Round( decimal.Parse(txtGrandTotal.Text),2);
                transaction.transaction_date = DateTime.Now;
                transaction.tax = decimal.Parse(txtVat.Text);
                transaction.discount = decimal.Parse(txtDiscount.Text);

                string username = frmLogin.loggedIn;
                userBLL u = uDAL.GetIdFromUsername(username);

                transaction.added_by = u.Id;
                transaction.transactionDetails = transactionDT;

                bool success = false;
                using (TransactionScope scope = new TransactionScope())
                {
                    int transactionId = -1;
                    bool w = tDAL.Insert_Transaction(transaction, out transactionId);

                    for (int i = 0; i < transactionDT.Rows.Count; i++)
                    {
                        transactionDetailBLL transactionDetail = new transactionDetailBLL();
                        string productName = transactionDT.Rows[i][0].ToString();
                        productsBLL p = pdal.GetProductIdFromName(productName);
                        transactionDetail.product_id = p.Id;
                        transactionDetail.rate = decimal.Parse(transactionDT.Rows[i][1].ToString());
                        transactionDetail.qty = decimal.Parse(transactionDT.Rows[i][2].ToString());
                        transactionDetail.total = Math.Round(decimal.Parse(transactionDT.Rows[i][3].ToString()), 2);
                        transactionDetail.dea_cust_id = dc.Id;
                        transactionDetail.added_date = DateTime.Now;
                        transactionDetail.added_by = u.Id;


                        string transactionType = lblTop.Text;
                        bool x = false;
                        if (transactionType == "PURCHASE")
                        {
                            x = pdal.IncreaseProduct(transactionDetail.product_id, transactionDetail.qty);
                        }
                        else if (transactionType == "SALES")
                        {
                            x = pdal.DecreaseProduct(transactionDetail.product_id, transactionDetail.qty);
                        }

                        bool y = tdDAL.InsertTransactionDetail(transactionDetail);
                        success = w && x && y;

                    }

                    if (success == true)
                    {
                        scope.Complete();
                        MassageBox mb = new MassageBox("Transaction Success", MsgType.success);
                        mb.Show();
                        //MessageBox.Show("Transaction Completed Successfully...");
                        dgvAddedProducts.DataSource = null;
                        dgvAddedProducts.Rows.Clear();

                        txtSearch.Text = "";
                        txtName.Text = "";
                        txtEmail.Text = "";
                        txtContact.Text = "";
                        txtAddress.Text = "";
                        txtSearchProduct.Text = "";
                        txtNamePro.Text = "";
                        txtInventory.Text = "0";
                        txtRate.Text = "0";
                        txtQty.Text = "0";
                        txtSubTotal.Text = "0";
                        txtDiscount.Text = "0";
                        txtVat.Text = "0";
                        txtGrandTotal.Text = "0";
                        txtPaidAmount.Text = "0";
                        txtReturnAmount.Text = "0";


                    }
                    else
                    {
                        MassageBox mb = new MassageBox("Transaction Failed", MsgType.retry);
                        mb.Show();
                        //MessageBox.Show("Transaction Failed...!!! Try again");
                    }
                }
            }catch(Exception)
            {
                MassageBox mb = new MassageBox("ERROR", MsgType.clear);
                mb.BackColor = Color.Crimson;
                mb.Show();
            }

        }
    }
}
