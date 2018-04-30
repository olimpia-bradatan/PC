using System.Linq;
using System.Web.Mvc;

namespace PC.Controllers
{
    public class MedicalPrescriptionController : Controller
    {
        PCContext db = new PCContext();
        // GET: MedicalPrescription
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
        [Authorize(Users = "assistant@assistant.com")]
        public ActionResult MedicalPrescriptionCreate()
        {
            return View();
        }

        // POST: MedicalPrescription/Create
        [HttpPost]
        [Authorize(Users = "assistant@assistant.com")]
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
                    return RedirectToAction("MedicalPrescriptionIndex");
                }
                else
                {
                    TempData["Warning"] = "Medical Prescription already exists associated to this pacient!";
                    return RedirectToAction("MedicalPrescriptionEdit");
                }
            }
            return View();
        }

        // GET: MedicalPrescription/Edit/5
        [Authorize(Users = "assistant@assistant.com")]
        public ActionResult MedicalPrescriptionEdit(int id)
        {
            return View(db.medicalPrescriptions.Find(id));
        }

        // POST: MedicalPrescription/Edit/5
        [HttpPost]
        [Authorize(Users = "assistant@assistant.com")]
        public ActionResult MedicalPrescriptionEdit(int id, medicalPrescription medicalPrescription)
        {
            db.Entry(medicalPrescription).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            TempData["Success"] = "Changes successfully applied to your Medical Prescription!";
            return RedirectToAction("MedicalPrescriptionIndex");
        }

        // GET: MedicalPrescription/Delete/5
        [Authorize(Users = "assistant@assistant.com")]
        public ActionResult MedicalPrescriptionDelete(int id)
        {
            return View(db.medicalPrescriptions.Find(id));
        }

        // POST: MedicalPrescription/Delete/5
        [HttpPost]
        [Authorize(Users = "assistant@assistant.com")]
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
