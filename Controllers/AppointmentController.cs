using Heathweb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Heathweb.Controllers
{
    public class AppointmentController : Controller
    {
        // GET: Appointment
        public ActionResult Index()
        {
            using (var dbContext = new HealthWebEntities())
            {
                List<tbl_department> Department = dbContext.tbl_department.ToList();
                SelectList departmentlist = new SelectList(Department, "ID", "Department_Name");
                ViewBag.departmentlist = departmentlist;
                return View();
            }

        }
        [Route("Appoinment/GetDoctors/{departmentId}")]
        public ActionResult GetDoctorsByDepartment(int departmentId)
        {
            using (var dbContext = new HealthWebEntities())
            {
                var doctors = dbContext.tbl_Doctor
                    .Where(d => d.Department_ID == departmentId)
                    .Select(d => new SelectListItem { Value = d.ID.ToString(), Text = d.DoctorName })
                    .ToList();

                return Json(doctors, JsonRequestBehavior.AllowGet);
            }
        }
        



        public ActionResult BookAppointment(tbl_Appointment model, int DoctorList)
        {
            if (ModelState.IsValid)
            {
                using (var dbContext = new HealthWebEntities())
                {
                    model.Doctor_ID = DoctorList;
                    dbContext.tbl_Appointment.Add(model);
                    dbContext.SaveChanges();
                }
                return Json(new
                {
                    success = true,
                    PatientName = model.Patient_Name,
                    AppointmentDate = model.Appointment_Date.HasValue ? model.Appointment_Date.Value.ToString("dd/MM/yyyy") : null,
                    PatientEmail = model.Email
                });
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
        }

        public ActionResult CloseAndRedirect ()
        {
            return RedirectToAction("ShowDashboard", "Home");
        }
    }



     
}