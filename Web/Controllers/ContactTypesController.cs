using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BO;

namespace Web.Controllers
{
    public class ContactTypesController : Controller
    {
        private INTEC_AGU_OCT22Entities db = new INTEC_AGU_OCT22Entities();

        // GET: ContactTypes
        public ActionResult Index()
        {
            return View(db.ContactTypes.ToList());
        }

        // GET: ContactTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactType contactType = db.ContactTypes.Find(id);
            if (contactType == null)
            {
                return HttpNotFound();
            }
            return View(contactType);
        }

        // GET: ContactTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContactTypes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Description,Enabled")] ContactType contactType)
        {
            if (ModelState.IsValid)
            {
                contactType.CreatedDate = DateTime.Now;
                db.ContactTypes.Add(contactType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contactType);
        }

        // GET: ContactTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactType contactType = db.ContactTypes.Find(id);
            if (contactType == null)
            {
                return HttpNotFound();
            }
            return View(contactType);
        }

        // POST: ContactTypes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Enabled,CreatedDate")] ContactType contactType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contactType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contactType);
        }

        // GET: ContactTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactType contactType = db.ContactTypes.Find(id);
            if (contactType == null)
            {
                return HttpNotFound();
            }
            return View(contactType);
        }

        // POST: ContactTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ContactType contactType = db.ContactTypes.Find(id);
            db.ContactTypes.Remove(contactType);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
