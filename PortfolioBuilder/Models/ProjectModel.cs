using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortfolioBuilder.Models
{
    public class ProjectModel
    {
        public int user_projid { get; set; }
        public int userid { get; set; }

        [Required(ErrorMessage = "This field is required !")]
        [Display(Name = "Project 1 Name")]
        public string prj1_name { get; set; }

        [Required(ErrorMessage = "This field is required !")]
        [Display(Name = "Project 1 Description")]
        public string prj1_description { get; set; }

        
        [Display(Name = "Project 2 Name")]
        public string prj2_name { get; set; }

        
        [Display(Name = "Project 2 Description")]
        public string prj2_description { get; set; }
    }
}