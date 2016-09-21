using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeddingsForYou.Models
{
    public class Forum
    {
        [Required(ErrorMessage = "Name Field is Required")]
        public string bloggerName { get; set; }


        [Required(ErrorMessage = "Title Field is Required")]
        public string title { get; set; }

        [Required(ErrorMessage = "Subheading Field is Required")]
        public string subHeading { get; set; }

    }
}