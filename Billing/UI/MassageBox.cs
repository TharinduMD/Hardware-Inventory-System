using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Billing.UI
{
    public partial class MassageBox : Form
    {
        public MassageBox(string msg, MsgType type)
        {
            InitializeComponent();

            massageCon.Text = msg;
            switch(type)
            {
                case MsgType.success:
                    this.BackColor = Color.FromArgb(64, 0, 64);
                    icon.Image = imageList1.Images[0];

                    break;

                case MsgType.update:
                    this.BackColor = Color.SeaGreen;
                    icon.Image = imageList1.Images[4];
                    break;

                case MsgType.retry:
                    this.BackColor = Color.Teal;
                    icon.Image = imageList1.Images[3];
                    break;

                case MsgType.delete:
                    this.BackColor = Color.Crimson;
                    icon.Image = imageList1.Images[1];
                    break;

                case MsgType.clear:
                    this.BackColor = Color.Gray;
                    icon.Image = imageList1.Images[2];
                    break;
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
    public enum MsgType 
    { 
        success,retry,delete,clear,update
    }
}
