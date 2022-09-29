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
    public class PeopleController : Controller
    {
        private INTEC_AGU_OCT22Entities db = new INTEC_AGU_OCT22Entities();

        // GET: People
        public ActionResult Index()
        {
            var people = db.People.Include(p => p.ClientType).Include(p => p.Company).Include(p => p.ContactType).Include(p => p.Deparment);
            return View(people.ToList());
        }

        // GET: People/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // GET: People/Create
        public ActionResult Create()
        {
            ViewBag.ClientTypeId = new SelectList(db.ClientTypes, "Id", "Name");
            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name");
            ViewBag.ContactTypeId = new SelectList(db.ContactTypes, "Id", "Name");
            ViewBag.DepartmentId = new SelectList(db.Deparments, "Id", "Code");
            return View();
        }

        // POST: People/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,MiddleName,LastName,ClientTypeId,ContactTypeId,SupportStaff,PhoneNumber,EmailAddress,Enabled")] Person person)
        {
            if (ModelState.IsValid)
            {
                person.Id = Guid.NewGuid().ToString();
                person.CreatedDate = DateTime.Now;
                db.People.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientTypeId = new SelectList(db.ClientTypes, "Id", "Name", person.ClientTypeId);
            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name", person.CompanyId);
            ViewBag.ContactTypeId = new SelectList(db.ContactTypes, "Id", "Name", person.ContactTypeId);
            ViewBag.DepartmentId = new SelectList(db.Deparments, "Id", "Code", person.DepartmentId);
            return View(person);
        }

        // GET: People/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientTypeId = new SelectList(db.ClientTypes, "Id", "Name", person.ClientTypeId);
            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name", person.CompanyId);
            ViewBag.ContactTypeId = new SelectList(db.ContactTypes, "Id", "Name", person.ContactTypeId);
            ViewBag.DepartmentId = new SelectList(db.Deparments, "Id", "Code", person.DepartmentId);
            return View(person);
        }

        // POST: People/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,MiddleName,LastName,ClientTypeId,ContactTypeId,SupportStaff,PhoneNumber,EmailAddress,CompanyId,DepartmentId,Enabled,CreatedDate")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientTypeId = new SelectList(db.ClientTypes, "Id", "Name", person.ClientTypeId);
            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name", person.CompanyId);
            ViewBag.ContactTypeId = new SelectList(db.ContactTypes, "Id", "Name", person.ContactTypeId);
            ViewBag.DepartmentId = new SelectList(db.Deparments, "Id", "Code", person.DepartmentId);
            return View(person);
        }

        // GET: People/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Person person = db.People.Find(id);
            db.People.Remove(person);
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
