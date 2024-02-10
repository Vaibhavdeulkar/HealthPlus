using Heathweb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Heathweb.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult ShowDashboard()
        {
            return View();
        }
        public ActionResult Blogs()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
      
        public ActionResult Services()
        {
            return View();
        }
        public ActionResult GetDoctors()
        {
            using (var dbContext = new HealthWebEntities())
            {
                List<tbl_Doctor> DoctorsList = dbContext.tbl_Doctor.ToList();
                return View(DoctorsList);
            }
        }
        [HttpPost]
        public ActionResult Send(FormCollection form)
        {
            try
            {
                string name = form["name"];
                string email = form["email"];
                string phone = form["phone"];
                string subject = form["subject"];
                var htmlBody = "<html><body>" +
                    "<p> Name :" + name + "</p>" +
                    "<p> email from :" + email + "</p>" +
                    "<p> phone :" + phone + "</p>" +
                    "<p>" + form["message"] + "</p>" +
                    "</body></html>";

                using (var dbContext = new HealthWebEntities())
                {
                    string[] FromEmail = dbContext.tbl_admin.Select(x => x.Email).ToArray();
                    SendEmail.SendEmails(FromEmail, subject, htmlBody);

                }
                return Json(new
                {
                    success = true,
                    Name = name,
                });
            }
            catch
            {
                return Json(new
                {
                    success = false,
                    errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)),
                });
            }

        }

    }
}