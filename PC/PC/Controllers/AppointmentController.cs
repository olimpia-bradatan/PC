using PC;
using PC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PC.Controllers
{
    public class AppointmentController : Controller
    {
        PCContext db = new PCContext();
        // GET: Appointment
        public ActionResult AppointmentIndex()
        {
            return View(db.Appointments.ToList());
        }

        // GET: Appointment/Details/5
        public ActionResult AppointmentDetails(int id)
        {
            return View(db.Appointments.Find(id));
        }

        // GET: Appointment/Create
        public ActionResult AppointmentCreate()
        {
            return View();
        }

        // POST: Appointment/Create
        [HttpPost]
        public ActionResult AppointmentCreate(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                AppointmentComparer cmp = new AppointmentComparer();
                int ok = 1;
                if (db.Appointments.Count() > 0)
                {
                    foreach (var a in db.Appointments)
                        if (cmp.Equals(a, appointment))
                            ok = 0;
                }
                if (ok == 1)
                {
                    appointment.idMedic = 1;
                    db.Appointments.Add(appointment);
                    db.SaveChanges();
                    TempData["Success"] = "Appointment successfully submitted!";
                    return RedirectToAction("AppointmentIndex");
                }
                else
                {
                    TempData["Warning"] = "Appointment already booked!";
                    return RedirectToAction("AppointmentCreate");
                }
            }
            return View();
        }

        // GET: Appointment/Edit/5
        public ActionResult AppointmentEdit(int id)
        {
            return View(db.Appointments.Find(id));
        }

        // POST: Appointment/Edit/5
        [HttpPost]
        public ActionResult AppointmentEdit(int id, Appointment appointment)
        {
           // try
            //{
                db.Entry(appointment).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["Success"] = "Changes successfully applied to your appointment!";
                return RedirectToAction("AppointmentIndex");
            //}
            //catch
            //{
             //   return View();
            //}
        }

        // GET: Appointment/Delete/5
        public ActionResult AppointmentDelete(int id)
        {
            return View(db.Appointments.Find(id));
        }

        // POST: Appointment/Delete/5
        [HttpPost]
        public ActionResult AppointmentDelete(int id, Appointment appointment)
        {
            try
            {
                db.Appointments.Remove(db.Appointments.Find(id));
                db.SaveChanges();
                TempData["Success"] = "Appointment successfully cancelled!";
                return RedirectToAction("AppointmentIndex");
            }
            catch
            {
                return View();
            }
        }
    }
}
