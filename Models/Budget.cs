using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeddingsForYou.Models
{
    public class Budget
    {
        public int ID { get; set; }

        public int ItemID { get; set; }
        public string description { get; set; }

        public int cost { get; set; }
        public string paid { get; set; }

        public string unpaid { get; set; }

        public string budgetAmount { get; set; }
    }
}