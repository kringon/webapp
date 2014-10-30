﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebWarehouse.Models;

namespace WebWarehouse.Controllers
{
    public class MyController : Controller
    {
               protected bool CheckLoginStatus()
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