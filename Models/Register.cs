using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using WeddingsForYou.Models;

namespace WeddingsForYou.Models
{
    public class Register
    {
        public string UserType { get; set; }

        [Required(ErrorMessage = "Login Field is Required")]
        [StringLength(100, MinimumLength=6, ErrorMessage = "Login Name Must Be Greater than 6 Characters")]
        public string Login { get; set; }


        [Required(ErrorMessage = "Name Field is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Password Field is Required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Re-Enter Password Field is Required")]
        [Compare("Password", ErrorMessage = "Password and Re-Enter Password Must be a match")]
        public string ReEnterPassword { get; set; }

        [Required(ErrorMessage = "Email Field is Required")]
        //[RegularExpression("[0-9a-zA-Z\\._]+@[0-9a-zA-Z\\._]")]
        public string Email { get; set; }
    }
}