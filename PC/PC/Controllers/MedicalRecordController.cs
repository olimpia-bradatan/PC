using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PC.Controllers
{
    public class MedicalRecordController : Controller
    {
        PCContext db = new PCContext();
        // GET: MedicalRecord
        public ActionResult MedicalRecordIndex()
        {
            return View(db.medicalRecords.ToList());
        }

        // GET: MedicalRecord/Details/5
        public ActionResult MedicalRecordDetails(int id)
        {
            return View(db.medicalRecords.Find(id));
        }

        // GET: MedicalRecord/Create
        public ActionResult MedicalRecordCreate()
        {
            return View();
        }

        // POST: MedicalRecord/Create
        [HttpPost]
        public ActionResult MedicalRecordCreate(medicalRecord medicalRecord)
        {
            if (ModelState.IsValid)
            {
                int ok = 1;
                /*if (db.medicalRecords.Count() > 0)
                {
                            ok = 0;
                }*/
                if (ok == 1)
                {
                    medicalRecord.idmedicalRecords = 1;
                    db.medicalRecords.Add(medicalRecord);
                    db.SaveChanges();
                    TempData["Success"] = "Medical Record successfully submitted!";
                    return RedirectToAction("MedicalRecordIndex");
                }
                else
                {
                    TempData["Warning"] = "Medical Record already exists associated to this pacient!";
                    return RedirectToAction("MedicalRecordEdit");
                }
            }
            return View();
        }

        // GET: MedicalRecord/Edit/5
        public ActionResult MedicalRecordEdit(int id)
        {
            return View(db.medicalRecords.Find(id));
        }

        // POST: MedicalRecord/Edit/5
        [HttpPost]
        public ActionResult MedicalRecordEdit(int id, medicalRecord medicalRecord)
        {
            //try
            //{
            // TODO: Add update logic here
            db.Entry(medicalRecord).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            TempData["Success"] = "Changes successfully applied to your Medical Record!";
            return RedirectToAction("MedicalRecordIndex");
            //}
            //catch
            //{
             //   return View();
            //}
        }

        // GET: MedicalRecord/Delete/5
        public ActionResult MedicalRecordDelete(int id)
        {
            return View(db.medicalRecords.Find(id));
        }

        // POST: MedicalRecord/Delete/5
        [HttpPost]
        public ActionResult MedicalRecordDelete(int id, medicalRecord medicalRecord)
        {
            try
            {
                db.medicalRecords.Remove(db.medicalRecords.Find(id));
                db.SaveChanges();
                TempData["Success"] = "Medical Record successfully deleted!";
                // TODO: Add delete logic here

                return RedirectToAction("MedicalRecordIndex");
            }
            catch
            {
                return View();
            }
        }
    }
}
