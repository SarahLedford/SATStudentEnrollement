using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations; //Metadata attributes for models
//Model attributes for display, displayformat, validation, UIHints
//USING STATEMENT MAKES THE METADATA REACHABLE
namespace SATStudentEnrollment.UI.MVC.Models
{
    public class ContactViewModel
    {
        //What is the structure of a contact form?
        //Name, Subject, Message, Email

        [Required(ErrorMessage = "* Name is required")] //only affects the one property directly below the declaration
        //You can add the Required attribute without the error message, you will get a default message that uses the
        //property name.
        public string Name { get; set; }

        [Required(ErrorMessage = "* Subject is required")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "* Message is required")]
        [UIHint("MultilineText")]//when the EditorFor() for this field and tells it which HTML element to render. (in 
        //this case, it's a textarea) instead of a textbox (input type="text").
        public string Message { get; set; }

        //[RegularExpression("Expression To Use", ErrorMessage ="Please make sure your entry is in the proper format")]
        [EmailAddress(ErrorMessage = "* Please check your format on the email address")]
        [Required(ErrorMessage = "* Email is required")]
        [Display(Name = "Your Email")]
        public string EmailAddress { get; set; }



    }//end class
}//end namespace