using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SAT.DATA.EF;
using SAT.UI.MVC.Models;

namespace SAT.UI.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StudentStatusController : Controller
    {
        private SATEntities db = new SATEntities();

        // GET: StudentStatus
        public ActionResult Index()
        {
            return View(db.StudentStatuses.ToList());
        }

        //GET: StudentStatus - Grid or Tiled View
        public ActionResult StudentGrid()
        {
            //Need to reference the Students table in the DB to get access to this - somehow through the "Model"
            var Studentgrid = db.StudentStatuses;
            return View(db.StudentStatuses.ToList());
        }

        //public ActionResult StudentGrid()        //{        //    var Studentgrid = db.Students.Include(p => p.FirstName).Include(p => p.LastName).Include(p => p.Major).Include(p => p.PhotoUrl).Include(p => p.StudentStatus);        //    return View(Studentgrid.ToList());        //}


        // GET: StudentStatus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentStatus StudentStatus = db.StudentStatuses.Find(id);
            if (StudentStatus == null)
            {
                return HttpNotFound();
            }
            return View(StudentStatus);
        }

        // GET: StudentStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SSID,SSName,SSDescription")] StudentStatus StudentStatus)
        {
            if (ModelState.IsValid)
            {
                db.StudentStatuses.Add(StudentStatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(StudentStatus);
        }

        // GET: StudentStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentStatus StudentStatus = db.StudentStatuses.Find(id);
            if (StudentStatus == null)
            {
                return HttpNotFound();
            }
            return View(StudentStatus);
        }

        // POST: StudentStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SSID,SSName,SSDescription")] StudentStatus StudentStatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(StudentStatus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(StudentStatus);
        }

        // GET: StudentStatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentStatus StudentStatus = db.StudentStatuses.Find(id);
            if (StudentStatus == null)
            {
                return HttpNotFound();
            }
            return View(StudentStatus);
        }

        // POST: StudentStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentStatus StudentStatus = db.StudentStatuses.Find(id);
            db.StudentStatuses.Remove(StudentStatus);
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
