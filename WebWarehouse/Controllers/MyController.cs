using System.Web.Mvc;
using WebWarehouse.Model;

namespace WebWarehouse.Controllers
{
    public class MyController : Controller
    {
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
            if (Session != null)
            {
                ViewBag.UserID = Session["UserID"];
            }
            

            
        }

        protected bool CheckLoginStatus()
        {

            if (Session != null)
            {
                if (Session["LoggedInn"] == null)
                {
                    Session["LoggedInn"] = false;
                    Session["Role"] = UserRole.Unknown.ToString();
                    ViewBag.LoggedInn = false;
                    ViewBag.Role = UserRole.Unknown.ToString();
                    return false;
                }
                else
                {
                    ViewBag.LoggedInn = (bool)Session["LoggedInn"];
                    ViewBag.Role = Session["Role"];

                    return (bool)Session["LoggedInn"];
                }
            }
            return false;
         
            
        }
    }
}