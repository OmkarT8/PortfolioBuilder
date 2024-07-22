using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortfolioBuilder.Models
{
    public class UserDetailsModel
    {
        public int userid { get; set; }

        [Required(ErrorMessage = "This field is required !")]
        [Display(Name = "Name")]
        public string uname { get; set; }

        [Required(ErrorMessage = "This field is required !")]
        [Display(Name = "Role")]
        public string urole { get; set; }

        [Required(ErrorMessage = "This field is required !")]
        [Display(Name = "About")]
        public string uabout { get; set; }

        [Required(ErrorMessage = "This field is required !")]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",ErrorMessage = "Please enter a valid email address")]
        [Display(Name = "Email Address")]
        public string uemail { get; set; }

        [Required(ErrorMessage = "This field is required !")]
        [Display(Name = "Contact")]
        public string ucontact { get; set; }

        [Required(ErrorMessage = "This field is required !")]
        [Display(Name = "Upload Image")]
        public HttpPostedFileBase Filepic { get; set; }

        public int userRegid { get; set; }

    }
}