using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data; 

namespace Billing.BLL
{
    class transactionsBLL
    {
        public int Id { get; set; }
        public string type { get; set; }
        public int dea_cust_id { get; set; }
        public decimal grandTotal { get; set; }
        public DateTime transaction_date { get; set; }
        public decimal tax { get; set; }
        public decimal discount { get; set; }
        public int added_by { get; set; }
        public DataTable transactionDetails { get; set; }

    }
}
