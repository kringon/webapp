using log4net;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebWarehouse.BLL;
using WebWarehouse.Model;

namespace WebWarehouse.Controllers
{
    public class OrdersController : MyController
    {
        private OrderBLL bll;
    

        private ILog Logger = LogManager.GetLogger(typeof(OrdersController));

        public OrdersController()
        {
            bll = new OrderBLL();
       
        }

        public OrdersController(OrderBLL stub)
        {
            bll = stub;

        }



        public ActionResult AddItem(int itemid, int userid)
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
                return Json(new JavaScriptSerializer().Serialize(bll.addItem(itemid, userid)), JsonRequestBehavior.AllowGet);
            }
            return Json(new { error = "Something went wrong, dispatch monkeys to fix" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CheckOut(int orderID)
        {
            CheckLoginStatus();
            addCustomMessages();

            Order checkoutOrder = bll.Find(orderID);

            if (Session["UserID"].Equals(checkoutOrder.user.ID))
            {
                if (checkoutOrder != null)
                {
                    TempData["SuccessMessage"] = "You are now one step away from fulfilling your destiny!";
                    addCustomMessages();
                    return View(checkoutOrder);
                }
                else
                {
                    var msg = "There is no order with orderID: " + orderID;
                    TempData["ErrorMessage"] = msg;
                    Logger.Warn(msg);
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["ErrorMessage"] = "You are not allowed to check out this order!";
                Logger.Error("User with UserID: " + Session["UserId"] + " is trying to check out another users order.");
                return RedirectToAction("Index", "Home");
            }
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
                if (bll.Create(order))
                {
                    var msg = "A new Order was created with ID: " + order.ID;
                    Logger.Info(msg);
                    TempData["SuccessMessage"] = msg;
                    return RedirectToAction("Index");
                }
            }

            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int id)
        {
            CheckLoginStatus();
            addCustomMessages();

            Order order = bll.Find(id);
            if (order == null)
            {
                var msg = "Could not find the specified Order";
                Logger.Warn(msg);
                TempData["ErrorMessage"] = msg;
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bll.Delete(id);
            return RedirectToAction("Index");
        }

        // GET: Orders/Details/5
        public ActionResult Details(int id)
        {
            CheckLoginStatus();
            addCustomMessages();

            Order order = bll.Find(id);
            if (order == null)
            {
                var msg = "Cannot find the specified Order -> Did you use the right link?";
                Logger.Error(msg);
                TempData["ErrorMessage"] = msg;
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int id)
        {
            CheckLoginStatus();
            addCustomMessages();

            Order order = bll.Find(id);
            if (order == null)
            {
                var msg = "Cannot find the specified Order -> Did you use the right link?";
                Logger.Error(msg);
                TempData["ErrorMessage"] = msg;
                return RedirectToAction("Index");
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
                if (bll.Update(order))
                {
                    var msg = "You have updated Order with id: " + order.ID;
                    Logger.Info(msg);
                    TempData["SuccessMessage"] = msg;
                    return RedirectToAction("Index");
                }
                else
                {
                    var msg = "Updating the order with id: " + order.ID + " failed.";
                    Logger.Warn(msg);
                    TempData["ErrorMessage"] = msg;
                    addCustomMessages();
                }
            }
            return View(order);
        }

        // GET: Orders
        public ActionResult Index()
        {
            CheckLoginStatus();
            addCustomMessages();
            return View(bll.FindAll());
        }

        [HttpGet]
        public ActionResult Reciept(int orderID)
        {
            CheckLoginStatus();
            addCustomMessages();


            Order recieptOrder = bll.Find(orderID);
            if (Session["UserID"].Equals(recieptOrder.user.ID))
            {
                if (recieptOrder != null)
                {
                    recieptOrder.Status = OrderEnum.Payed;
                    recieptOrder.Ordered = System.DateTime.Now;

                    bll.Update(recieptOrder);
                    TempData["SuccessMessage"] = "Congratulations!You have ordered. The shopping cart is emptied, and you can view your exisiting orders by clicking on the menuitem.";
                    Logger.Info("Order with ordreID: " + recieptOrder.ID + " was successfull");
                    addCustomMessages();
                    return View(recieptOrder);
                }
                else
                {
                    var msg = "There is no order with orderID: " + orderID;
                    TempData["ErrorMessage"] = msg;
                    Logger.Warn(msg);
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["ErrorMessage"] = "You are not allowed to check out this order!";
                Logger.Error("User with UserID: " + Session["UserId"] + " is trying to check out another users order.");
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult RemoveItem(int itemid, int userid)
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
                return Json(new JavaScriptSerializer().Serialize(bll.removeItem(itemid, userid)), JsonRequestBehavior.AllowGet);
            }
            return Json(new { error = "Something went wrong, dispatch monkeys to fix" });
        }
    }
}