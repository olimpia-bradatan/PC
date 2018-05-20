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
        [Authorize(Roles = "Patient")]
        public ActionResult AppointmentCreate()
        {
            return View();
        }

        // POST: Appointment/Create
        [HttpPost]
        [Authorize(Roles = "Patient")]
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
                    String[] availableHours = new String[] { "9:00", "9:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00", "16:30" };

                    TimeSpan time = (TimeSpan)appointment.Time;
                    String[] notAvailableHours = new String[20];
                    int k = 0;
                    if (db.Appointments.Count() > 0)
                    {
                        foreach (var a in db.Appointments)
                        {
                            if (a.Date.Equals(appointment.Date))
                            {
                                TimeSpan time1 = (TimeSpan)a.Time;
                                notAvailableHours[k++] = time1.ToString().Split(':')[0] + ":" + time1.ToString().Split(':')[1];
                            }
                        }
                    }
                    String content = "";
                    int ok1;
                    for (int i = 0; i < availableHours.Length; i++)
                    {
                        ok1 = 1;
                        for (int j = 0; j < k; j++)
                            if (notAvailableHours[j].Equals(availableHours[i]))
                                ok1 = 0;
                        if (ok1 == 1)
                            content = content + availableHours[i] + " | ";
                        
                    }
                    TempData["Warning"] = "Appointment already booked! Times available this day are: " + content;
                }
            }
            return View();
        }

        // GET: Appointment/Edit/5
        [Authorize(Roles = "Assistant")]
        public ActionResult AppointmentEdit(int id)
        {
            return View(db.Appointments.Find(id));
        }

        // POST: Appointment/Edit/5
        [HttpPost]
        [Authorize(Roles = "Assistant")]
        public ActionResult AppointmentEdit(int id, Appointment appointment)
        {
                db.Entry(appointment).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["Success"] = "Changes successfully applied to your appointment!";
                return RedirectToAction("AppointmentIndex");          
        }

        // GET: Appointment/Delete/5
        [Authorize(Roles = "Patient")]
        public ActionResult AppointmentDelete(int id)
        {
            return View(db.Appointments.Find(id));
        }

        // POST: Appointment/Delete/5
        [HttpPost]
        [Authorize(Roles = "Patient")]
        public ActionResult AppointmentDelete(int id, Appointment appointment)
        {
            try
            {
                var appointmentData = from row in db.Appointments
                                      where row.idAppointment == id
                                      select row;

                var appointmentFound = appointmentData.First() ?? null;

                var appointmentTime = appointmentFound.Time ?? DateTime.Now.TimeOfDay;
                var appointmentDate = appointmentFound.Date ?? DateTime.Now.Date;
                DateTime result = appointmentDate + appointmentTime;

                var hours = (result - DateTime.Now).TotalHours;

                if (hours < 24)
                {
                    TempData["ConditionsNotMet"] = "You can only cancel an appointment at least 24 hours before!";
                    return RedirectToAction("AppointmentIndex");
                }

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
