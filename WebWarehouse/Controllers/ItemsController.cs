using log4net;
using System.Web.Mvc;
using WebWarehouse.BLL;
using WebWarehouse.Model;

namespace WebWarehouse.Controllers
{
    public class ItemsController : MyController
    {
        private ItemBLL bll = new ItemBLL();
        private ItemCategoryBLL icbll = new ItemCategoryBLL();
        private ILog Logger = LogManager.GetLogger(typeof(ItemsController));

        // GET: Items/Create
        public ActionResult Create()
        {
            CheckLoginStatus();
            addCustomMessages();
            ViewBag.ItemCategoryID = new SelectList(icbll.FindAll(), "ID", "Name");
            return View();
        }

        // POST: Items/Create To protect from overposting attacks, please enable the specific
        // properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ItemCategoryID,Name,Price")] Item Item)
        {
            CheckLoginStatus();
            addCustomMessages();
            if (ModelState.IsValid)
            {
                if (bll.Create(Item))
                {
                    var msg = "A new Item was created with name: " + Item.Name;
                    Logger.Info(msg);
                    TempData["SuccessMessage"] = msg;
                    return RedirectToAction("Index");
                }
            }

            ViewBag.ItemCategoryID = new SelectList(icbll.FindAll(), "ID", "Name", Item.ItemCategoryID);
            return View(Item);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? Id)
        {
            CheckLoginStatus();
            addCustomMessages();
            if (Id == null)
            {
                var msg = "You must specify which Item you wish to delete";
                Logger.Warn(msg);
                TempData["ErrorMessage"] = msg;
                return RedirectToAction("Index");
            }
            Item item = bll.Find(Id);
            if (item == null)
            {
                var msg = "Could not find the specified Item";
                Logger.Warn(msg);
                TempData["ErrorMessage"] = msg;
                return RedirectToAction("Index");
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bll.Delete(id);
            return RedirectToAction("Index");
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            CheckLoginStatus();
            addCustomMessages();
            if (id == null)
            {
                var msg = "You must specify which Item you wish to see";
                Logger.Warn(msg);
                TempData["ErrorMessage"] = msg;
                return RedirectToAction("Index");
            }
            Item Item = bll.Find(id);
            if (Item == null)
            {
                var msg = "Cannot find the specified Item -> Did you use the right link?";
                Logger.Warn(msg);
                TempData["ErrorMessage"] = msg;
                return RedirectToAction("Index");
            }
            return View(Item);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? Id)
        {
            CheckLoginStatus();
            addCustomMessages();
            if (Id == null)
            {
                var msg = "You must specify which Item you wish to edit";
                Logger.Warn(msg);
                TempData["ErrorMessage"] = msg;
                return RedirectToAction("Index");
            }
            Item item = bll.Find(Id);
            if (item == null)
            {
                var msg = "You must specify which Item you wish to edit";
                Logger.Warn(msg);
                TempData["ErrorMessage"] = msg;
                return RedirectToAction("Index");
            }
            ViewBag.ItemCategoryID = new SelectList(icbll.FindAll(), "ID", "Name", item.ItemCategoryID);
            return View(item);
        }

        // POST: Items/Edit/5 To protect from overposting attacks, please enable the specific
        // properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ItemCategoryID,Name,Price")] Item item)
        {
            CheckLoginStatus();
            addCustomMessages();
            if (ModelState.IsValid)
            {
                if (bll.Update(item))
                {
                    var msg = "You have updated Item with id: " + item.ID;
                    Logger.Info(msg);
                    TempData["SuccessMessage"] = msg;
                    return RedirectToAction("Index");
                }
                else
                {
                    var msg = "Updating the Item with id: " + item.ID + " failed.";
                    Logger.Warn(msg);
                    TempData["ErrorMessage"] = msg;
                    addCustomMessages();
                }
            }
            ViewBag.ItemCategoryID = new SelectList(icbll.FindAll(), "ID", "Name", item.ItemCategoryID);
            return View(item);
        }

        // GET: Items
        public ActionResult Index()
        {
            CheckLoginStatus();
            addCustomMessages();

            return View(bll.FindAll());
        }

        //Custom method to listAllItems with a common ItemCategory
        public ActionResult ListByCategory(int itemCategoryId)
        {
            if (itemCategoryId != null)
            {
                CheckLoginStatus();
                addCustomMessages();
                var Items = bll.ListByCategory(itemCategoryId);

                ViewBag.CategoryName = icbll.Find(itemCategoryId).Name;
                return View("List", Items);
            }
            else
            {
                var msg = "You must specify which Category you wish to see";
                Logger.Warn(msg);
                TempData["ErrorMessage"] = msg;
                return RedirectToAction("Index");
            }
           
        }
    }
}