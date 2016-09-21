using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeddingsForYou.Models
{
    public class Tasks
    {
        public int ID { get; set; }

        public int TaskID { get; set; }

        public string description { get; set; }

        public string Complete { get; set; }

        public string InComplete { get; set; }
    }
}