using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebWarehouse.DAL;

namespace WebWarehouse.Controllers
{
    public class ItemsController : Controller
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


        private bool CheckLoginStatus()
        {
            if (Session["LoggedInn"] == null)
            {
                Session["LoggedInn"] = false;
                ViewBag.LoggedInn = false;
                return false;

            }
            else
            {
                ViewBag.LoggedInn = (bool)Session["LoggedInn"];
                return (bool)Session["LoggedInn"];
            }


        }

        private void addCustomMessages()
        {
            if (TempData["ErrorMessage"] != null)
            {
                @ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();
            }

            if (TempData["SuccessMessage"] != null)
            {
                @ViewBag.SuccessMessage = TempData["SuccessMessage"].ToString();
            }
            @ViewBag.UserID = Session["UserID"];
        }
    }
}