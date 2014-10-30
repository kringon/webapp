using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebWarehouse.DAL;

namespace WebWarehouse.Controllers
{
    public class HomeController : MyController
    {

        private WarehouseContext db = new WarehouseContext();

        public ActionResult Index()
        {
            CheckLoginStatus();
            addCustomMessages();


            return View(db.ItemCategorys.ToList());
        }

       
    }
}