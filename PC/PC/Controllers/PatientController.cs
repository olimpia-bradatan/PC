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
        public ActionResult PatientCreate()
        {
            return View();
        }

        // POST: Patient/Create
        [HttpPost]
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
                    //patient.idMedic = 1;
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
        public ActionResult PatientEdit(String id)
        {
            return View(db.Patients.Find(id));
        }

        // POST: Patient/Edit/5
        [HttpPost]
        public ActionResult PatientEdit(String id, Patient patient)
        {
            db.Entry(patient).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            TempData["Success"] = "Changes successfully applied to the patient!";
            return RedirectToAction("PatientIndex");
        }

        // GET: Patient/Delete/5
        public ActionResult PatientDelete(String id)
        {
            return View(db.Patients.Find(id));
        }

        // POST: Patient/Delete/5
        [HttpPost]
        public ActionResult PatientDelete(String id, Patient patient)
        {
            try
            {
                db.Patients.Remove(db.Patients.Find(id));
                db.SaveChanges();
                TempData["Success"] = "Patient  successfully deleted!";
                return RedirectToAction("PatientIndex");
            }
            catch
            {
                return View();
            }
        }
    }
}
