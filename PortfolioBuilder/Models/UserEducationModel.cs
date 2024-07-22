using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortfolioBuilder.Models
{
    public class UserEducationModel
    {
        public int userid { get; set; }

        public int user_eduid { get; set; }


        [Required(ErrorMessage = "This field is required !")]
        [Display(Name = "SSC Date")]
        public string sscDate { get; set; }


        [Required(ErrorMessage = "This field is required !")]
        [Display(Name = "SSC School")]
        public string sscSchool { get; set; }


        [Required(ErrorMessage = "This field is required !")]
        [Display(Name = "SSC Percent")]
        public double sscPercent { get; set; }


        [Required(ErrorMessage = "This field is required !")]
        [Display(Name = "HSC Date")]
        public string hscDate { get; set; }


        [Required(ErrorMessage = "This field is required !")]
        [Display(Name = "HSC College")]
        public string hscCollege { get; set; }


        [Required(ErrorMessage = "This field is required !")]
        [Display(Name = "HSC Percent")]
        public double hscPercent { get; set; }


        [Display(Name = "Graduation University")]
        public string gradUni { get; set; }


        [Display(Name = "Graduation CGPA")]
        public double gradCgpa { get; set; }
    }
}