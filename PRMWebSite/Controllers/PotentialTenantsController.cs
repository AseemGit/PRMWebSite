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
    public class PotentialTenantsController : Controller
    {
        private PRMDatabaseEntities db = new PRMDatabaseEntities();

        // GET: PotentialTenants
        public ActionResult Index()
        {
            return View(db.PotentialTenants.ToList());
        }

        // GET: PotentialTenants/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PotentialTenant potentialTenant = db.PotentialTenants.Find(id);
            if (potentialTenant == null)
            {
                return HttpNotFound();
            }
            return View(potentialTenant);
        }

        // GET: PotentialTenants/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PotentialTenants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TenantId,FirstName,LastName,Email,Password")] PotentialTenant potentialTenant)
        {
            if (ModelState.IsValid)
            {
                db.PotentialTenants.Add(potentialTenant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(potentialTenant);
        }

        // GET: PotentialTenants/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PotentialTenant potentialTenant = db.PotentialTenants.Find(id);
            if (potentialTenant == null)
            {
                return HttpNotFound();
            }
            return View(potentialTenant);
        }

        // POST: PotentialTenants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TenantId,FirstName,LastName,Email,Password")] PotentialTenant potentialTenant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(potentialTenant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(potentialTenant);
        }

        // GET: PotentialTenants/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PotentialTenant potentialTenant = db.PotentialTenants.Find(id);
            if (potentialTenant == null)
            {
                return HttpNotFound();
            }
            return View(potentialTenant);
        }

        // POST: PotentialTenants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PotentialTenant potentialTenant = db.PotentialTenants.Find(id);
            db.PotentialTenants.Remove(potentialTenant);
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
