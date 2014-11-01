using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebWarehouse.DAL;
using WebWarehouse.Models;

namespace WebWarehouse.Controllers
{
    public class OrdersController : MyController
    {
        private WarehouseContext db = new WarehouseContext();

        [HttpGet]
        public ActionResult Reciept(int? orderID)
        {
            CheckLoginStatus();
            addCustomMessages();
            if (orderID == null)
            {
                TempData["ErrorMessage"] = "Her har det skjedd noe galt -> Prøver du å lure systemet?";
                return RedirectToAction("Index", "Home");
            }

            Order recieptOrder = db.Orders.Find(orderID);
            if (Session["UserID"].Equals(recieptOrder.user.ID))
            {
                if (recieptOrder != null)
                {
                    recieptOrder.Status = OrderEnum.Payed;
                    recieptOrder.Ordered = System.DateTime.Now;

                    db.Entry(recieptOrder).State = EntityState.Modified;

                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Gratulerer! Du har bestilt. Handlelisten er tømt og du kan følge bestillingen på 'Min Bruker => Min Ordrehistorikk'";
                    addCustomMessages();

                    return View(recieptOrder);
                }
                else
                {
                    TempData["ErrorMessage"] = "Det finnes ikke noen ordre med den ID'en";
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Du har ikke lov til å sjekke ut den Ordren!";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult CheckOut(int? orderID)
        {
            CheckLoginStatus();
            addCustomMessages();
            if (orderID == null)
            {
                TempData["ErrorMessage"] = "Du må vite hvilken ordre du skal sjekke ut";
                return RedirectToAction("Index", "Home");
            }
            Order checkoutOrder = db.Orders.Find(orderID);

            if (Session["UserID"].Equals(checkoutOrder.user.ID))
            {
                if (checkoutOrder != null)
                {
                    TempData["SuccessMessage"] = "Du er nå ett steg unna å fullføre ordren";
                    addCustomMessages();
                    return View(checkoutOrder);
                }
                else
                {
                    TempData["ErrorMessage"] = "Det finnes ikke noen ordre med den ID'en";
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Du har ikke lov til å sjekke ut den Ordren!";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult ActiveOrder(int? id)
        {
            CheckLoginStatus();
            addCustomMessages();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = db.Users.Find(id);
            Order emptyOrder = user.Orders.FirstOrDefault(o => o.Status == OrderEnum.Empty);
            Order browsingOrder = user.Orders.FirstOrDefault(o => o.Status == OrderEnum.Browsing);

            if (emptyOrder == null && browsingOrder == null)
            {
                Order newOrder = new Order();
                newOrder.Items = new List<Item>();
                user.Orders.Add(newOrder);

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();

                return PartialView(newOrder);
            }
            if (browsingOrder != null)
            {
                return PartialView(browsingOrder);
            }

            return PartialView("ActiveOrder", emptyOrder);
        }

        public ActionResult AddItem(int? itemid, int? userid)
        {
            if (!Session["UserID"].Equals(userid))
            {
                //invalid userID
                return Json(new { error = "Invalid userID" }, JsonRequestBehavior.AllowGet);
            }
            CheckLoginStatus();
            addCustomMessages();
            if (ModelState.IsValid)
            {
                Item item = db.Items.Find(itemid);
                User user = db.Users.Find(userid);
                Order order = user.Orders.FirstOrDefault(o => o.Status == OrderEnum.Browsing || o.Status == OrderEnum.Empty);

                if (order == null)
                {
                    order = new Order();
                    order.Status = OrderEnum.Empty;

                    order.Items = new List<Item>();
                    order.ItemQuantities = new List<ItemQuantity>();
                    user.Orders.Add(order);
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                }

                order.Items.Add(item);
                ItemQuantity quantity = order.getItemQuantity(item);
                if (quantity != null)
                    quantity.Value++;
                else
                    order.ItemQuantities.Add(new ItemQuantity() { Value = 1, Item = item });

                if (order.Status == OrderEnum.Empty)
                    order.Status = OrderEnum.Browsing;

                db.Entry(order).State = EntityState.Modified;
                db.Entry(user).State = EntityState.Modified;

                db.SaveChanges();

                return Json(new { error = "none", totalPrice = order.getTotalPrice(), itemName = item.Name }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { error = "Something went wrong, dispatch monkeys to fix" });
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            CheckLoginStatus();
            addCustomMessages();
            return View();
        }

        // POST: Orders/Create To protect from overposting attacks, please enable the specific
        // properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,status,ordered,delivered")] Order order)
        {
            CheckLoginStatus();
            addCustomMessages();
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            CheckLoginStatus();
            addCustomMessages();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            CheckLoginStatus();
            addCustomMessages();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            CheckLoginStatus();
            addCustomMessages();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5 To protect from overposting attacks, please enable the specific
        // properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Status,Ordered,Delivered,UserID")] Order order)
        {
            CheckLoginStatus();
            addCustomMessages();

            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders
        public ActionResult Index()
        {
            CheckLoginStatus();
            addCustomMessages();
            return View(db.Orders.ToList());
        }

        public ActionResult RemoveItem(int? itemid, int? userid)
        {
            if (!Session["UserID"].Equals(userid) && !Session["Role"].Equals(UserRole.Admin.ToString()))
            {
                //invalid userID
                return Json(new { error = "Invalid userID" }, JsonRequestBehavior.AllowGet);
            }
            CheckLoginStatus();
            addCustomMessages();

            if (ModelState.IsValid)
            {
                Item item = db.Items.Find(itemid);
                User user = db.Users.Find(userid);
                Order order = user.Orders.FirstOrDefault(o => o.Status == OrderEnum.Browsing || o.Status == OrderEnum.Empty);

                if (order == null)
                {
                    return Json(new { error = "Something Went Wrong during removal. Please contact administrator. ItemID:" + itemid + " UserID:" + userid }, JsonRequestBehavior.AllowGet);
                }

                order.Items.Remove(item);
                order.ItemQuantities.Remove(order.getItemQuantity(item));

                if (order.Items.Count == 0)
                    order.Status = OrderEnum.Empty;

                db.Entry(order).State = EntityState.Modified;
                db.Entry(user).State = EntityState.Modified;

                db.SaveChanges();

                return Json(new { error = "none", totalPrice = order.getTotalPrice(), itemID = item.ID }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { error = "Something went wrong, dispatch monkeys to fix" });
        }

        protected override void Dispose(bool disposing)
        {
            CheckLoginStatus();
            addCustomMessages();
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}