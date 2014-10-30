using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebWarehouse.Controllers
{
    public class MyController : Controller
    {
               protected bool CheckLoginStatus()
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
                ViewBag.Role = Session["UserRole"];
                return (bool)Session["LoggedInn"];
            }


        }

        protected void addCustomMessages()
        {
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();
            }

            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"].ToString();
            }
            ViewBag.UserID = Session["UserID"];
        }
    }
}