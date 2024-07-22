using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortfolioBuilder.Models
{
    public class SkillLangModel
    {
        public int user_skillLangid { get; set; }
        public int userid { get; set; }

        [Required(ErrorMessage = "This field is required !")]
        [Display(Name = "Professional Skills")]
        public string professionalSkills {get; set;}

        
        [Display(Name = "Frontend Skills")]
        public string frontendSkills { get; set; }

        
        [Display(Name = "Backend Skills")]
        public string backendSkills { get; set; }

        [Required(ErrorMessage = "This field is required !")]
        [Display(Name = "Languages Known")]
        public string languages { get; set; }
    }
}