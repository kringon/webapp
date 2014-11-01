using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebWarehouse.DAL;
using WebWarehouse.Models;

namespace WebWarehouse.Controllers
{
    public class ItemsController : MyController
    {
        private WarehouseContext db = new WarehouseContext();

        // GET: Items
        public ActionResult Index()
        {
            CheckLoginStatus();
            addCustomMessages();
            var items = db.Items.Include(i => i.ItemCategory);
            return View(items.ToList());
        }


        public ActionResult ListByCategory(int id)
        {

            CheckLoginStatus();
            addCustomMessages();
            var Items = db.Items.Where(x => x.ItemCategoryID == id);

            ViewBag.CategoryName = db.ItemCategorys.Find(id).Name;
            return View("List", Items);
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            CheckLoginStatus();
            addCustomMessages();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            CheckLoginStatus();
            addCustomMessages();
            ViewBag.ItemCategoryID = new SelectList(db.ItemCategorys, "ID", "Name");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ItemCategoryID,Name,Price")] Item item)
        {
            CheckLoginStatus();
            addCustomMessages();
            if (ModelState.IsValid)
            {
                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ItemCategoryID = new SelectList(db.ItemCategorys, "ID", "Name", item.ItemCategoryID);
            return View(item);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            CheckLoginStatus();
            addCustomMessages();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemCategoryID = new SelectList(db.ItemCategorys, "ID", "Name", item.ItemCategoryID);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ItemCategoryID,Name,Price")] Item item)
        {
            CheckLoginStatus();
            addCustomMessages();
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemCategoryID = new SelectList(db.ItemCategorys, "ID", "Name", item.ItemCategoryID);
            return View(item);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            CheckLoginStatus();
            addCustomMessages();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
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
