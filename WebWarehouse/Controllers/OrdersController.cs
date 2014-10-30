using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebWarehouse.DAL;

namespace WebWarehouse.Controllers
{
    public class OrdersController : MyController
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



      
    }
}