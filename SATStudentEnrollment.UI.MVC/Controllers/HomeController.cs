using SATStudentEnrollment.UI.MVC.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace SATStudentEnrollment.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            if (Request.IsAuthenticated)
            {
                ContactViewModel cvmEmail = new ContactViewModel();
                cvmEmail.EmailAddress = User.Identity.GetUserName();

                return View(cvmEmail);
            }


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel cvm)
        {
            if (!ModelState.IsValid)
            {
                return View(cvm);
            }
            string message = $"You have received an email from {cvm.Name} with a subject of " +
                $"{(cvm.Subject == null ? "Email From SarahLedford.com" : cvm.Subject)}. Please respond to " +
                $"{cvm.EmailAddress} with your response to the following message:<br /><br />" +
                $"{cvm.Message}";

            MailMessage mm = new MailMessage("admin@sarahledford.com", "Sarah_Ledford@outlook.com", cvm.Subject, message);

            mm.IsBodyHtml = true;
            mm.Priority = MailPriority.High;
            mm.ReplyToList.Add(cvm.EmailAddress);

            SmtpClient client = new SmtpClient("mail.sarahledford.com");
            client.Credentials = new NetworkCredential("admin@sarahledford.com", "@Dmin123");

            try
            {
                client.Send(mm);
            }
            catch (Exception ex)
            {
                ViewBag.EmailMessage = $"We're sorry. Your request could not be completed at this time. Please" +
                    $" try again later. Error Message: {ex.StackTrace}.";
                return View(cvm);
            }
            return View("EmailConfirm", cvm);
        }
    }
}