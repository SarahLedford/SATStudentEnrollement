using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace SATStudentEnrollment.UI.MVC.Models
{
    public class ContactViewModel
    {

        [Required(ErrorMessage = "* Name is required")] 
        public string Name { get; set; }

        [Required(ErrorMessage = "* Subject is required")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "* Message is required")]
        [UIHint("MultilineText")]
        public string Message { get; set; }

       
        [EmailAddress(ErrorMessage = "* Please check your format on the email address")]
        [Required(ErrorMessage = "* Email is required")]
        [Display(Name = "Your Email")]
        public string EmailAddress { get; set; }



    }//end class
}//end namespace