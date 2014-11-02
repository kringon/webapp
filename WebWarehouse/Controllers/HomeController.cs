using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebWarehouse.BLL;


namespace WebWarehouse.Controllers
{
    public class HomeController : MyController
    {

        private ItemCategoryBLL bll = new ItemCategoryBLL();

        public ActionResult Index()
        {
            CheckLoginStatus();
            addCustomMessages();

            return View(bll.FindAll());
        }

       
    }
}