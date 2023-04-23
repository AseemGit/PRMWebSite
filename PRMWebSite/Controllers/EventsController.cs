using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PRMWebSite.Models;

namespace PRMWebSite.Controllers
{
    public class EventsController : Controller
    {
        private PRMDatabaseEntities db = new PRMDatabaseEntities();

        // GET: Events
        public ActionResult Index()
        {
            var events = db.Events.Include(m => m.PropertyManager).Include(m => m.PropertyOwner);
            return View(events.ToList());

        }

        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            ViewBag.ManagerId = new SelectList(db.PropertyManagers, "ManagerId", "FirstName");
            ViewBag.OwnerId = new SelectList(db.PropertyOwners, "OwnerId", "FirstName");
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventId,ManagerId,OwnerId,Subject,Content,Date,Time")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ManagerId = new SelectList(db.PropertyManagers, "ManagerId", "FirstName", @event.ManagerId);
            ViewBag.OwnerId = new SelectList(db.PropertyOwners, "OwnerId", "FirstName", @event.OwnerId);
            return View(@event);
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            ViewBag.ManagerId = new SelectList(db.PropertyManagers, "ManagerId", "FirstName", @event.ManagerId);
            ViewBag.OwnerId = new SelectList(db.PropertyOwners, "OwnerId", "FirstName", @event.OwnerId);
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventId,ManagerId,OwnerId,Subject,Content,Date,Time")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ManagerId = new SelectList(db.PropertyManagers, "ManagerId", "FirstName", @event.ManagerId);
            ViewBag.OwnerId = new SelectList(db.PropertyOwners, "OwnerId", "FirstName", @event.OwnerId);
            return View(@event);
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            db.Events.Remove(@event);
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
