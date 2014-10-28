using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebWarehouse.DAL;

namespace WebWarehouse.Controllers
{
    public class OrdersController : Controller
    {

        private WarehouseContext db = new WarehouseContext();


        public ActionResult ListAll(int? id)
        {
            //Add any messages that has been sent earlier
            addCustomMessages();

            //Fetch the logged inn user and check if he is calling the right view
            int sessionId;
            if (Session["UserID"] != null)
            {
                sessionId = (int)Session["UserID"];
                if (id == null)
                {
                    //TODO return all orders if admin
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else if (id == sessionId)
                {
                    return View(db.Users.Find(id).Orders);
                }
            }
            TempData["ErrorMessage"] = "Du må være logget inn for å se ordrene";
            return RedirectToAction("Index", "Home");
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