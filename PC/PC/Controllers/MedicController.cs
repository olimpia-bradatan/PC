using PC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PC.Controllers
{
    public class MedicController : Controller
    {
        PCContext db = new PCContext();
        // GET: Medic
        public ActionResult MedicIndex()
        {
            return View(db.Medics.ToList());
        }

        // GET: Medic/Details/5
        public ActionResult MedicDetails(int id)
        {
            return View(db.Medics.Find(id));
        }

        // GET: Medic/Create
        public ActionResult MedicCreate()
        {
            return View();
        }

        // POST: Medic/Create
        [HttpPost]
        public ActionResult MedicCreate(Medic medic)
        {
            if (ModelState.IsValid)
            {
                MedicComparer med = new MedicComparer();
                int ok = 1;
                if (db.Medics.Count() > 0)
                {
                    foreach (var a in db.Medics)
                        if (med.Equals(a, medic))
                            ok = 0;
                }
                if (ok == 1)
                {
                    medic.idMedic = 1;
                    db.Medics.Add(medic);
                    db.SaveChanges();
                    TempData["Success"] = "Medic successfully submitted!";
                    return RedirectToAction("MedicIndex");
                }
                else
                {
                    TempData["Warning"] = "Medic already exists!";
                    return RedirectToAction("MedicEdit");
                }
            }
            return View();
        }

        // GET: Medic/Edit/5
        public ActionResult MedicEdit(int id)
        {
            return View(db.Medics.Find(id));
        }

        // POST: Medic/Edit/5
        [HttpPost]
        public ActionResult MedicEdit(int id, Medic medic)
        {
            db.Entry(medic).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            TempData["Success"] = "Changes successfully applied to this Medic!";
            return RedirectToAction("MedicIndex");
        }

        // GET: Medic/Delete/5
        public ActionResult MedicDelete(int id)
        {
            return View(db.Medics.Find(id));
        }

        // POST: Medic/Delete/5
        [HttpPost]
        public ActionResult MedicDelete(int id, Medic medic)
        {
            try
            {
                db.Medics.Remove(db.Medics.Find(id));
                db.SaveChanges();
                TempData["Success"] = "Medic successfully deleted!";
                // TODO: Add delete logic here

                return RedirectToAction("MedicIndex");
            }
            catch
            {
                return View();
            }
        }
    }
}
