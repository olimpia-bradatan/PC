using System;
using System.Web.Mvc;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.Linq;

namespace PC.Controllers
{
    public class ArchiveDateController : Controller
    {
        PCContext db = new PCContext();

        // GET: Archivedate/Convert
        [Authorize(Roles = "Assistant, Patient, Medic")]
        public ActionResult ConvertMedicalPrescription(int idmedicalPrescription)
        {

                return View(db.medicalPrescriptions.Find(idmedicalPrescription));
        }

        // POST: Archivedate/Convert
        [HttpPost]
        [Authorize(Roles = "Assistant, Medic, Patient")]
        public ActionResult ConvertMedicalPrescription(int idmedicalPrescription, medicalPrescription m)
        {
            Appointment a = db.Appointments.Where(u => u.idmedicalPrescription == idmedicalPrescription).FirstOrDefault();
            m = db.medicalPrescriptions.Find(idmedicalPrescription);
            Patient p = db.Patients.Find(a.cardNumber);
            Medic mm = db.Medics.Find(p.idMedic);
            
            try
             {
                var exportFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                var exportFile = System.IO.Path.Combine(exportFolder, p.firstName + " " + p.lastName + "." + a.cardNumber + "." + ".pdf");

                using (var writer = new PdfWriter(exportFile))
                {
                    using (var pdf = new PdfDocument(writer))
                    {
                        var doc = new Document(pdf);
                        Table t = new Table(2);
                        Paragraph para = new Paragraph("Medical prescription");
                        para.SetBold();
                        para.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                        doc.Add(para);

                        doc.Add(new Paragraph(""));
                        doc.Add(new Paragraph("Patient first name: " + p.firstName));
                        doc.Add(new Paragraph("Patient last name: " + p.lastName));
                        doc.Add(new Paragraph("Card number: " + p.cardNumber));
                        doc.Add(new Paragraph("Birth date: " + p.birthDate.ToString().Split(' ')[0]));

                        t.AddCell("Medic name");
                        t.AddCell(mm.lastName + " " + mm.firstName);
                        t.AddCell("Appointment date");
                        t.AddCell(""+a.Date.ToString().Split(' ')[0]);
                        t.AddCell("DIAGNOSTIC");
                        t.AddCell(m.Diagnostic);
                        t.AddCell("MEDICATION");
                        t.AddCell(m.Medication);
                        t.AddCell("FREE");
                        if (m.Free == true)
                            t.AddCell("YES");
                        else
                            t.AddCell("NO");
                        doc.Add(t);
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

        // GET: Archivedate/Convert
        [Authorize(Roles = "Assistant, Patient, Medic")]
        public ActionResult ConvertMedicalRecord(int idmedicalRecord)
        {

            return View(db.medicalRecords.Find(idmedicalRecord));
        }

        // POST: Archivedate/Convert
        [HttpPost]
        [Authorize(Roles = "Assistant, Medic, Patient")]
        public ActionResult ConvertMedicalRecord(int idmedicalRecord, medicalRecord m)
        {
            Patient p = db.Patients.Where(u => u.idmedicalRecords == idmedicalRecord).FirstOrDefault();
            Medic mm = db.Medics.Find(p.idMedic);

            try
            {
                var exportFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                var exportFile = System.IO.Path.Combine(exportFolder, p.firstName + " " + p.lastName + " medical record" + ".pdf");

                using (var writer = new PdfWriter(exportFile))
                {
                    using (var pdf = new PdfDocument(writer))
                    {
                        var doc = new Document(pdf);
                        Table t = new Table(2);
                        Paragraph para = new Paragraph("Medical record");
                        para.SetBold();
                        para.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                        doc.Add(para);

                        doc.Add(new Paragraph(""));
                        doc.Add(new Paragraph("Patient first name: " + p.firstName));
                        doc.Add(new Paragraph("Patient last name: " + p.lastName));
                        doc.Add(new Paragraph("Card number: " + p.cardNumber));
                        doc.Add(new Paragraph("Birth date: " + p.birthDate.ToString().Split(' ')[0]));

                        t.AddCell("Medic name");
                        t.AddCell(mm.lastName + " " + mm.firstName);
                        t.AddCell("Last control date");
                        t.AddCell(m.date.ToString().Split(' ')[0]);
                        t.AddCell("Disease and previous disease");
                        t.AddCell("" + m.diseases + " " + m.previousDiseases);
                        t.AddCell("Medication");
                        t.AddCell(m.meds);
                        t.AddCell("Allergies");
                        t.AddCell(m.allergies);
                        t.AddCell("Other information");
                        t.AddCell(m.info);
                        doc.Add(t);
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
