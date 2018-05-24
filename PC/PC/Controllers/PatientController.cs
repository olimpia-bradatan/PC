using PC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PC.Controllers
{
    public class PatientController : Controller
    {
        PCContext db = new PCContext();
        // GET: Patient
        [Authorize(Roles = "Patient, Assistant, Medic")]
        public ActionResult PatientIndex()
        {
            return View(db.Patients.ToList());
        }

        // GET: Patient/Details/5
        public ActionResult PatientDetails(String id)
        {
            return View(db.Patients.Find(id));
        }

        // GET: Patient/Create 
        [Authorize(Roles = "Assistant")]
        public ActionResult PatientCreate()
        {
            return View();
        }

        // POST: Patient/Create
        [HttpPost]
        [Authorize(Roles = "Assistant")]
        public ActionResult PatientCreate(Patient patient)
        {
            if (ModelState.IsValid)
            {
                PatientComparer cmp = new PatientComparer();
                int ok = 1;
                if (db.Patients.Count() > 0)
                {
                    foreach (var a in db.Patients)
                        if (cmp.Equals(a, patient))
                            ok = 0;
                }
                if (ok == 1)
                {
                    patient.idMedic = 1;
                    medicalRecord m = new medicalRecord();
                    db.medicalRecords.Add(m);
                    db.SaveChanges();
                    patient.idmedicalRecords = db.medicalRecords.ToList().Last().idmedicalRecords;
                    db.Patients.Add(patient);
                    db.SaveChanges();
                    TempData["Success"] = "Patient successfully added to the database!";
                    return RedirectToAction("PatientIndex");
                }
                else
                {
                    TempData["Warning"] = "Patient already exists in the database!";
                    return RedirectToAction("PatientCreate");
                }
            }
            return View();
        }

        // GET: Patient/Edit/5
        [Authorize(Roles = "Assistant, Medic")]
        public ActionResult PatientEdit(String id)
        {
            return View(db.Patients.Find(id));
        }

        // POST: Patient/Edit/5
        [HttpPost]
        [Authorize(Roles = "Assistant, Medic")]
        public ActionResult PatientEdit(string id, Patient patient)
        {
            db.Entry(patient).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            TempData["Success"] = "Changes successfully applied to the patient!";
            return RedirectToAction("PatientIndex");
        }

        // GET: Patient/Delete/5
        [Authorize(Roles = "Assistant, Medic")]
        public ActionResult PatientDelete(string id)
        {
            return View(db.Patients.Find(id));
        }

        // POST: Patient/Delete/5
        [HttpPost]
        [Authorize(Roles = "Assistant, Medic")]
        public ActionResult PatientDelete(string id, Patient patient)
        {
            try
            {
                Patient p = db.Patients.Find(id);
                db.Patients.Remove(p);
                db.SaveChanges();
                TempData["Success"] = "Patient  successfully deleted!";
                return RedirectToAction("PatientIndex");
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "Assistant, Medic")]
        public ActionResult FiltersDetails(string disease)
        {
            if (!String.IsNullOrEmpty(disease))
            {
                {
                    List<Patient> patients = new List<Patient>();
                    foreach (Patient a in db.Patients.ToList())
                    {
                        if (a.medicalRecord.diseases.ToString().Contains(disease) || a.medicalRecord.previousDiseases.ToString().Contains(disease))
                        {
                            patients.Add(a);
                        }
                    }
                    return View(patients.ToList());
                }
            }
            return View(db.Patients.ToList());
        }
    }
}
