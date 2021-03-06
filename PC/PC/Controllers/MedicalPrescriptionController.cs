﻿using System.Linq;
using System.Web.Mvc;

namespace PC.Controllers
{
    public class MedicalPrescriptionController : Controller
    {
        PCContext db = new PCContext();
        // GET: MedicalPrescription
        [Authorize(Roles = "Assistant, Medic")]
        public ActionResult MedicalPrescriptionIndex()
        {
            return View(db.medicalPrescriptions.ToList());
        }

        // GET: MedicalPrescription/Details/5
        public ActionResult MedicalPrescriptionDetails(int id)
        {
            return View(db.medicalPrescriptions.Find(id));
        }

        // GET: MedicalPrescription/Create
        [Authorize(Roles = "Assistant")]
        public ActionResult MedicalPrescriptionCreate()
        {
            return View();
        }

        // POST: MedicalPrescription/Create
        [HttpPost]
        [Authorize(Roles = "Assistant")]
        public ActionResult MedicalPrescriptionCreate(medicalPrescription medicalPrescription)
        {
            if (ModelState.IsValid)
            {
                int ok = 1;
                if (ok == 1)
                {
                    medicalPrescription.idmedicalPrescription = 1;
                    db.medicalPrescriptions.Add(medicalPrescription);
                    db.SaveChanges();
                    TempData["Success"] = "Medical Prescription successfully submitted!";
                    return RedirectToAction("AppointmentIndex", "Appointment");
                }
                else
                {
                    TempData["Warning"] = "Medical Prescription already exists associated to this pacient!";
                    return RedirectToAction("AppointmentIndex", "Appointment");
                }
            }
            return View();
        }

        // GET: MedicalPrescription/Edit/5
        [Authorize(Roles = "Assistant, Medic")]
        public ActionResult MedicalPrescriptionEdit(int id)
        {
            return View(db.medicalPrescriptions.Find(id));
        }

        // POST: MedicalPrescription/Edit/5
        [HttpPost]
        [Authorize(Roles = "Assistant, Medic")]
        public ActionResult MedicalPrescriptionEdit(int id, medicalPrescription medicalPrescription)
        {
            db.Entry(medicalPrescription).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            Appointment a = db.Appointments.Where(u => u.idmedicalPrescription == id).FirstOrDefault();
            Patient p = db.Patients.Find(a.cardNumber);
            medicalRecord m = db.medicalRecords.Find(p.idmedicalRecords);
            m.previousDiseases = m.previousDiseases + ", " + medicalPrescription.Diagnostic;
            m.date = a.Date;
            db.Entry(m).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            TempData["Success"] = "Changes successfully applied to your Medical Prescription!";
            return RedirectToAction("AppointmentIndex", "Appointment");
        }

        // GET: MedicalPrescription/Delete/5
        [Authorize(Roles = "Assistant")]
        public ActionResult MedicalPrescriptionDelete(int id)
        {
            return View(db.medicalPrescriptions.Find(id));
        }

        // POST: MedicalPrescription/Delete/5
        [HttpPost]
        [Authorize(Roles = "Assistant")]
        public ActionResult MedicalPrescriptionDelete(int id, medicalPrescription medicalPrescription)
        {
            try
            {
                db.medicalPrescriptions.Remove(db.medicalPrescriptions.Find(id));
                db.SaveChanges();
                TempData["Success"] = "Medical Prescription successfully deleted!";
                // TODO: Add delete logic here

                return RedirectToAction("MedicalPrescriptionIndex");
            }
            catch
            {
                return View();
            }
        }
    }
}
