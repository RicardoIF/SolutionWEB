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
    public class ClientTypesController : Controller
    {
        private INTEC_AGU_OCT22Entities db = new INTEC_AGU_OCT22Entities();

        // GET: ClientTypes
        public ActionResult Index()
        {
            return View(db.ClientTypes.ToList());
        }

        // GET: ClientTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientType clientType = db.ClientTypes.Find(id);
            if (clientType == null)
            {
                return HttpNotFound();
            }
            return View(clientType);
        }

        // GET: ClientTypes/Create
        public ActionResult Create()
        {
            return View(new ClientType());
        }

        // POST: ClientTypes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Description,Enabled")] ClientType clientType)
        {
            if (ModelState.IsValid)
            {
                clientType.CreatedDate = DateTime.Now;
                db.ClientTypes.Add(clientType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(clientType);
        }

        // GET: ClientTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientType clientType = db.ClientTypes.Find(id);
            if (clientType == null)
            {
                return HttpNotFound();
            }
            return View(clientType);
        }

        // POST: ClientTypes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Enabled,CreatedDate")] ClientType clientType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clientType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(clientType);
        }

        // GET: ClientTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientType clientType = db.ClientTypes.Find(id);
            if (clientType == null)
            {
                return HttpNotFound();
            }
            return View(clientType);
        }

        // POST: ClientTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClientType clientType = db.ClientTypes.Find(id);
            db.ClientTypes.Remove(clientType);
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
