using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeddingsForYou.Models
{
    public class WeddingTables
    {
        public int ID { get; set; }

        public int TableID { get; set; }

        public string TableName { get; set; }

        public int SeatAmount { get; set; }
    }
}