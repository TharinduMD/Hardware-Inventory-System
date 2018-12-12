using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Billing.BLL
{
    class categoriesBLL
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string discription { get; set; }
        public DateTime added_date { get; set; }
        public int added_by { get; set; }

    }
}
