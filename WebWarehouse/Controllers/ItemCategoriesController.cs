using log4net;

using System.Web.Mvc;
using WebWarehouse.BLL;
using WebWarehouse.Model;

namespace WebWarehouse.Controllers
{
    public class ItemCategoriesController : MyController
    {
        private ItemCategoryBLL bll = new ItemCategoryBLL();

        private ILog Logger = LogManager.GetLogger(typeof(ItemCategoriesController));

        // GET: ItemCategories/Create
        public ActionResult Create()
        {
            CheckLoginStatus();
            addCustomMessages();
            return View();
        }

        // POST: ItemCategories/Create To protect from overposting attacks, please enable the
        // specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] ItemCategory itemCategory)
        {
            CheckLoginStatus();
            addCustomMessages();
            if (ModelState.IsValid)
            {
                if (bll.Create(itemCategory))
                {
                    var msg = "A new ItemCategory was created with name: " + itemCategory.Name;
                    Logger.Info(msg);
                    TempData["SuccessMessage"] = msg;
                    return RedirectToAction("Index");
                }
            }

            return View(itemCategory);
        }

        // GET: ItemCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            CheckLoginStatus();
            addCustomMessages();
            if (id == null)
            {
                var msg = "You must specify which ItemCategory you wish to delete";
                Logger.Warn(msg);
                TempData["ErrorMessage"] = msg;
                return RedirectToAction("Index");
            }

            ItemCategory itemCategory = bll.Find(id);

            if (itemCategory == null)
            {
                var msg = "Could not find the specified ItemCategory";
                Logger.Warn(msg);
                TempData["ErrorMessage"] = msg;
                return RedirectToAction("Index");
            }
            return View(itemCategory);
        }

        // POST: ItemCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bll.Delete(id);
            return RedirectToAction("Index");
        }

        // GET: ItemCategories/Details/5
        public ActionResult Details(int? id)
        {
            CheckLoginStatus();
            addCustomMessages();
            if (id == null)
            {
                var msg = "You must specify which ItemCategory you wish to see";
                Logger.Warn(msg);
                TempData["ErrorMessage"] = msg;
                return RedirectToAction("Index");
            }
            ItemCategory itemCategory = bll.Find(id);
            if (itemCategory == null)
            {
                var msg = "Cannot find the specified ItemCategory -> Did you use the right link?";
                Logger.Error(msg);
                TempData["ErrorMessage"] = msg;
                return RedirectToAction("Index");
            }
            return View(itemCategory);
        }

        // GET: ItemCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            CheckLoginStatus();
            addCustomMessages();
            if (id == null)
            {
                var msg = "You must specify which ItemCategory you wish to edit";
                Logger.Warn(msg);
                TempData["ErrorMessage"] = msg;
                return RedirectToAction("Index");
            }
            ItemCategory itemCategory = bll.Find(id);
            if (itemCategory == null)
            {
                var msg = "Cannot find the specified ItemCategory -> Did you use the right link?";
                Logger.Error(msg);
                TempData["ErrorMessage"] = msg;
                return RedirectToAction("Index");
            }
            return View(itemCategory);
        }

        // POST: ItemCategories/Edit/5 To protect from overposting attacks, please enable the
        // specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] ItemCategory itemCategory)
        {
            CheckLoginStatus();
            addCustomMessages();
            if (ModelState.IsValid)
            {
                if (bll.Update(itemCategory))
                {
                    var msg = "You have updated ItemCategory with id: " + itemCategory.ID;
                    Logger.Info(msg);
                    TempData["SuccessMessage"] = msg;
                    return RedirectToAction("Index");
                }
                else
                {
                    var msg = "Updating the ItemCategory with id: " + itemCategory.ID + " failed.";
                    Logger.Warn(msg);
                    TempData["ErrorMessage"] = msg;
                    addCustomMessages();
                }
            }
            return View(itemCategory);
        }

        // GET: ItemCategories
        public ActionResult Index()
        {
            CheckLoginStatus();
            addCustomMessages();
            return View(bll.FindAll());
        }
    }
}