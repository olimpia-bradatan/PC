using PC.Models;
using System.Linq;
using System.Web.Mvc;

namespace PC.Controllers
{
    public class AssistantController : Controller
    {
        PCContext db = new PCContext();
        // GET: Assistant
        [Authorize(Roles = "Admin")]
        public ActionResult IndexAssistant()
        {
            return View(db.Assistants.ToList());
        }

        // GET: Assistant/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult DetailsAssistant(int id)
        {
            return View(db.Assistants.Find(id));
        }

        // GET: Assistant/Create
        [Authorize(Roles = "Admin")]
        public ActionResult CreateAssistant()
        {
            return View();
        }

        // POST: Assistant/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult CreateAssistant(Assistant assistant)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AssistantComparer cmp = new AssistantComparer();
                    int ok = 1;
                    if (db.Assistants.Count() > 0)
                    {
                        foreach (var a in db.Assistants)
                            if (cmp.Equals(a, assistant))
                                ok = 0;
                    }
                    if (ok == 1)
                    {
                        assistant.idMedic = 1;
                        db.Assistants.Add(assistant);
                        db.SaveChanges();
                        TempData["Success"] = "Assistant successfully created!";
                        return RedirectToAction("IndexAssistant");
                    }
                    else
                    {
                        TempData["Warning"] = "Assistant already exist!";
                        return RedirectToAction("CreateAssistant");
                    }
                }
                return RedirectToAction("IndexAssistant");
            }
            catch
            {

                return View();
            }
        }

        // GET: Assistant/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult EditAssistant(int id)
        {
            return View(db.Assistants.Find(id));
        }

        // POST: Assistant/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult EditAssistant(int id, Assistant assistant)
        {
            try
            {
                db.Entry(assistant).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["Success"] = "Changes successfully applied to your appointment!";
            

                return RedirectToAction("IndexAssistant");
            }
            catch
            {
                return View();
            }
        }

        // GET: Assistant/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteAssistant(int id)
        {
            return View(db.Assistants.Find(id));
        }

        // POST: Assistant/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteAssistant(int id, Assistant assistant)
        {
            try
            {
                db.Assistants.Remove(db.Assistants.Find(id));
                db.SaveChanges();
                TempData["Success"] = "Assistant successfully deleted!";
                return RedirectToAction("IndexAssistant");
            }
            catch
            {
                return View();
            }
        }
    }
}
