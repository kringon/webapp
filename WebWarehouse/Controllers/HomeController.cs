using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebWarehouse.DAL;

namespace WebWarehouse.Controllers
{
    public class HomeController : Controller
    {

        private WarehouseContext db = new WarehouseContext();

        public ActionResult Index()
        {
            CheckLoginStatus();
            addCustomMessages();


            return View(db.ItemCategorys.ToList());
        }

        private void CheckLoginStatus()
        {
            if (Session["LoggedInn"] == null)
            {
                Session["LoggedInn"] = false;
            }
            else
            {
                ViewBag.LoggedInn = (bool)Session["LoggedInn"];
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