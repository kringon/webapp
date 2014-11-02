using log4net;
using System.Collections.Generic;
using System.Web.Mvc;
using WebWarehouse.BLL;
using WebWarehouse.Model;

namespace WebWarehouse.Controllers
{
    public class UsersController : MyController
    {
        private UserBLL bll;
        private ILog Logger = LogManager.GetLogger(typeof(UsersController));

        public UsersController()
        {
            bll = new UserBLL();
        }

        public UsersController(UserBLL stub)
        {
            bll = stub;
        }

        [HttpGet]
        public ActionResult ActiveOrder(int userId)
        {

            Logger.Info("entered method");
            CheckLoginStatus();
            addCustomMessages();

            Order order = bll.ActiveOrder(userId);

            if (order != null)
            {
                return PartialView(order);
            }

            return PartialView("ActiveOrder", order);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            if (CheckLoginStatus())
            {
                TempData["ErrorMessage"] = "Hvordan kom du hit? Du må logge ut før du kan lage ny bruker!";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                addCustomMessages();
                return View();
            }
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Username,Password,Address")] User user)
        {
            if (ModelState.IsValid)
            {
                //Check if users exists to avoid multiple
                if (bll.existingUser(user) != null)
                {
                    var msg = "Prøvde å opprette en bruker med eksisterende brukernavn. Vennligst velg et annet.";
                    TempData["ErrorMessage"] = msg;
                    Logger.Warn(msg);
                    return RedirectToAction("Create", "Users");
                }

                if (bll.Create(user))
                {
                    var msg = "Gratulerer! Du har nå opprettet en ny bruker med brukernavn: " + user.Username;
                    Logger.Info(msg);
                    TempData["SuccessMessage"] = msg;
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int id)
        {
            CheckLoginStatus();
            addCustomMessages();
            if (id == null)
            {
                var msg = "You must specify which User you wish to delete";
                Logger.Warn(msg);
                TempData["ErrorMessage"] = msg;
                return RedirectToAction("Index");
            }
            User user = bll.Find(id);
            if (user == null)
            {
                var msg = "Could not find the specified User";
                Logger.Warn(msg);
                TempData["ErrorMessage"] = msg;
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bll.Delete(id);
            return RedirectToAction("Index");
        }

        // GET: Users/Details/5
        public ActionResult Details(int id)
        {
            CheckLoginStatus();
            addCustomMessages();
            if (id == null)
            {
                var msg = "You must specify which User you wish to see";
                Logger.Warn(msg);
                TempData["ErrorMessage"] = msg;
                return RedirectToAction("Index");
            }
            User user = bll.Find(id);

            if (user == null)
            {
                var msg = "Cannot find the specified User -> Did you use the right link?";
                Logger.Error(msg);
                TempData["ErrorMessage"] = msg;
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
            CheckLoginStatus();
            addCustomMessages();
            int sessionId;
            if (Session["UserID"] != null)
            {
                sessionId = (int)Session["UserID"];
                if (id == null)
                {
                    var msg = "You must specify which User you wish to edit";
                    Logger.Warn(msg);
                    TempData["ErrorMessage"] = msg;
                    return RedirectToAction("Index");
                }
                else if (id != sessionId)
                {
                    TempData["ErrorMessage"] = "Du har ikke lov til å redigere andre brukere!";
                    return RedirectToAction("Index", "Home");
                }
                User user = bll.Find(id);
                if (user == null)
                {
                    var msg = "Cannot find the specified User -> Did you use the right link?";
                    Logger.Error(msg);
                    TempData["ErrorMessage"] = msg;
                    return RedirectToAction("Index");
                }
                return View(user);
            }
            ViewBag.ErrorMessage = "Du kan ikke redigere brukere uten å være innlogget!";
            return RedirectToAction("Index", "Home");
        }

        // POST: Users/Edit/5 To protect from overposting attacks, please enable the specific
        // properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Username,Password,Address,Role")] User user)
        {
            CheckLoginStatus();
            addCustomMessages();

            if (ModelState.IsValid)
            {
                if (bll.Update(user))
                {
                    var msg = "You have updated User with id: " + user.ID + " and userName: " + user.Username;
                    Logger.Info(msg);
                    TempData["SuccessMessage"] = msg;
                    return RedirectToAction("Index");
                }
                else
                {
                    var msg = "Updating the User with id: " + user.ID + " userName: " + user.Username + " failed.";
                    Logger.Error(msg);
                    TempData["ErrorMessage"] = msg;
                    addCustomMessages();
                }
            }
            return View(user);
        }

        // GET: Users
        public ActionResult Index()
        {
            addCustomMessages();
            if (CheckLoginStatus())
                return View(bll.FindAll());
            else
            {
                TempData["ErrorMessage"] = "Du har ikke tilgang til denne operasjonen";
                return RedirectToAction("Index", "Home");
            }
        }

        //Get all the orders for a single user
        public ActionResult listAllOrders(int id)
        {
            CheckLoginStatus();
            addCustomMessages();
            if (id == null)
            {
                var msg = "You must specify which User you wish to view Orders from";
                Logger.Warn(msg);
                TempData["ErrorMessage"] = msg;
                return RedirectToAction("Index");
            }
            User user = bll.Find(id);

            if (user == null)
            {
                var msg = "There is no user found with id: " + id;
                Logger.Warn(msg);
                TempData["ErrorMessage"] = msg;
                return RedirectToAction("Index");
            }
            return View(user.Orders);
        }

        //Get: Login
        public ActionResult Login()
        {
            CheckLoginStatus();
            addCustomMessages();

            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            User existingUser = bll.existingUser(user);
            if (existingUser != null)
            {
                Session["LoggedInn"] = true;
                Session["UserId"] = existingUser.ID;
                Session["Role"] = existingUser.Role.ToString();
                var msg = "Bruker med bruker ID: " + existingUser.ID + " Role: " + existingUser.Role + " er logget inn!";
                TempData["SuccessMessage"] = msg;
                Logger.Info(msg);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                Session["LoggedInn"] = false;
                Session["Role"] = UserRole.Unknown.ToString();
                ViewBag.Role = UserRole.Unknown.ToString();
                var msg = "Brukerinnlogging feilet for brukernavn: " + user.Username;
                ViewBag.ErrorMessage = msg;
                Logger.Warn(msg);
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session["LoggedInn"] = false;
            var msg = "Bruker med brukerID: " + Session["UserID"] + " logget ut.";
            Session["UserID"] = null;
            Session["Role"] = UserRole.Unknown.ToString();
            ViewBag.LoggedInn = false;
            ViewBag.Role = UserRole.Unknown.ToString();

            ViewBag.SuccessMessage = msg;
            Logger.Info(msg);

            return RedirectToAction("Index", "Home");
        }
    }
}