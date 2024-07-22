using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortfolioBuilder.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "This field is required !")]
        [Display(Name="Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "This field is required !")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}