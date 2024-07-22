using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortfolioBuilder.Models
{
    public class ExperienceModel
    {
        public int user_expid { get; set; }
        public int usrid { get; set; }

        
        [Display(Name = "Start Date")]
        public string exp1_start_dt { get; set; }

        
        [Display(Name = "End Date")]
        public string exp1_end_dt { get; set; }

        
        [Display(Name = "Job Description")]
        public string exp1_description { get; set; }

        
        [Display(Name = "Start Date")]
        public string exp2_start_dt { get; set; }

        
        [Display(Name = "End Date")]
        public string exp2_end_dt { get; set; }

       
        [Display(Name = "Job Description")]
        public string exp2_description { get; set; }
    }
}