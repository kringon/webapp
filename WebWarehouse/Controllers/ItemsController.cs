using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebWarehouse.DAL;

namespace WebWarehouse.Controllers
{
    public class ItemsController : MyController
    {

        private WarehouseContext db = new WarehouseContext();


        // GET: Items
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListByCategory(int id)
        {

            CheckLoginStatus();
            addCustomMessages();
            var Items = db.Items.Where(x => x.ItemCategoryID == id);

            ViewBag.CategoryName = db.ItemCategorys.Find(id).Name;
            return View("List", Items);
        }


    }
}