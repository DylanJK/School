using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeddingsForYou.Models
{
    public class Guests
    {
        public int ID { get; set; }

        public int GuestID { get; set; }

        public string GuestName { get; set; }

        public string GuestEmail { get; set; }

        public string GuestCeremony { get; set; }

        public string GuestReception { get; set; }

        public string GuestHens { get; set; }

        public string GuestStag { get; set; }
    }
}