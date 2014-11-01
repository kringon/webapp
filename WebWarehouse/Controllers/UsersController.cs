using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebWarehouse.DAL;
using WebWarehouse.Models;

namespace WebWarehouse.Controllers
{
    public class UsersController : MyController
    {
        private WarehouseContext db = new WarehouseContext();

        public ActionResult Logout()
        {
            Session["LoggedInn"] = false;
            Session["UserID"] = null;
            Session["Role"] = UserRole.Unknown.ToString();
            ViewBag.LoggedInn = false;
            ViewBag.Role = UserRole.Unknown.ToString();

            return RedirectToAction("Index", "Home");
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
            User existingUser = this.existingUser(user);
            if (existingUser != null)
            {
                Session["LoggedInn"] = true;
                Session["UserId"] = existingUser.ID;
                Session["Role"] = existingUser.Role.ToString();


                TempData["SuccessMessage"] = "Du er nå logget inn med bruker ID: " + existingUser.ID + " Role: " + existingUser.Role;

                return RedirectToAction("Index", "Home");
            }
            else
            {
                Session["LoggedInn"] = false;
                Session["Role"] = UserRole.Unknown.ToString();
                ViewBag.Role = UserRole.Unknown.ToString();
                ViewBag.ErrorMessage = "Du skrev ikke inn riktige verdier. Prøv på nytt.";
                return View();
            }
        }
        //Get all the orders for a single user
        public ActionResult listAllOrders(int? id)
        {
            CheckLoginStatus();
            addCustomMessages();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);


            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user.Orders);


        }
        // GET: Users
        public ActionResult Index()
        {

            addCustomMessages();
            if (CheckLoginStatus())
                return View(db.Users.ToList());
            else
            {
                TempData["ErrorMessage"] = "Du har ikke tilgang til denne operasjonen";
            }
            return RedirectToAction("Index", "Home");
        }


        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            CheckLoginStatus();
            addCustomMessages();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);

            
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
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
                if (existingUser(user) != null)
                {
                    TempData["ErrorMessage"] = "Det eksisterer allerede en bruker med dette brukernavnet. Vennligst velg et annet.";
                    return RedirectToAction("Create", "Users");
                }


                user.Password = hash(user.Password);
                db.Users.Add(user);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Gratulerer! Du har nå opprettet en ny bruker.";
                return RedirectToAction("Index", "Home");


            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            CheckLoginStatus();
            addCustomMessages();
            int sessionId;
            if (Session["UserID"] != null)
            {
                sessionId = (int)Session["UserID"];
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else if (id != sessionId)
                {
                    TempData["ErrorMessage"] = "Du har ikke lov til å redigere andre brukere!";
                    return RedirectToAction("Index", "Home");
                }
                User user = db.Users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }
            ViewBag.ErrorMessage = "Du kan ikke redigere brukere uten å være innlogget!";
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Username,Password,Address,Role")] User user)
        {


            if (ModelState.IsValid)
            {



                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            CheckLoginStatus();
            addCustomMessages();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        private String hash(string password)
        {
            var algoritme = System.Security.Cryptography.SHA256.Create();
            byte[] data = System.Text.Encoding.ASCII.GetBytes(password);
            return Convert.ToBase64String(algoritme.ComputeHash(data));

        }

        private User existingUser(User user)
        {

            if (user.Password == null)
                return null;

            String pwToBeChecked = hash(user.Password);


            User existingUser = db.Users.FirstOrDefault
                (b => b.Password == pwToBeChecked && b.Username == user.Username);
            if (existingUser == null)
            {
                return null;
            }
            else
            {
                return existingUser;
            }
        }



    }


}

