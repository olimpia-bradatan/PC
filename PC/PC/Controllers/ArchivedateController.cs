using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using PC.Models;
using System.IO;

namespace PC.Controllers
{
    public class ArchiveDateController : Controller
    {
        PCContext db = new PCContext();

        // GET: Archivedate/Convert
        public ActionResult ConvertMedicalPrescription(int idmedicalPrescription)
        {

            return View(db.medicalPrescriptions.Find(idmedicalPrescription));
        }

        // POST: Archivedate/Convert
        [HttpPost]
        public ActionResult ConvertMedicalPrescription(int idmedicalPrescription, medicalPrescription m)
        {
            Appointment a = db.Appointments.Find(idmedicalPrescription);
            try
            {
                var exportFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                var exportFile = System.IO.Path.Combine(exportFolder, a.cardNumber + ".pdf");

                using (var writer = new PdfWriter(exportFile))
                {
                    using (var pdf = new PdfDocument(writer))
                    {
                        var doc = new Document(pdf);
                        doc.Add(new Paragraph("DIAGNOSTIC "));
                        doc.Add(new Paragraph(m.Diagnostic));
                        doc.Add(new Paragraph("MEDICATION "));
                        doc.Add(new Paragraph(m.Medication));
                        doc.Add(new Paragraph("FREE "));
                        if (m.Free == true)
                            doc.Add(new Paragraph("YES"));
                        else
                            doc.Add(new Paragraph("NO"));
                    }
                }
                // TODO: Add insert logic here

                return RedirectToAction("AppointmentIndex", "Appointment");
            }
            catch
            {
                return View();
            }
        }
    }
}
