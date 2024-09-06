using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SessionTimeOut.Filters.YourNamespace.Filters;
using SessionTimeOut.Models;

namespace SessionTimeOut.Controllers
{
    public class AccountController : Controller
    {
        // Simulating a hardcoded user list
        private static readonly List<User> users = new List<User>
    {
        new User { Username = "admin", Password = "password123" },
        new User { Username = "user1", Password = "password123" }
    };

        // Action for Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User model)
        {
            var user = users.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);
            if (user != null)
            {
                // If user exists, store user information in session
                Session["Username"] = user.Username;
                Session["LoggedIn"] = true;

                // Redirect to a secured page after login
                return RedirectToAction("Dashboard");
            }

            // If login fails, show an error message
            ViewBag.Error = "Invalid username or password.";
            return View();
        }

        // Secured Dashboard Action that requires the user to be logged in
        [AuthorizeSession]
        public ActionResult Dashboard()
        {
            // Check if the user session is valid
            if (Session["LoggedIn"] != null && (bool)Session["LoggedIn"])
            {
                ViewBag.Username = Session["Username"];
                return View();
            }

            // If the session is not valid, redirect to the login page
            return RedirectToAction("Login");
        }

        // Logout action to clear the session
        public ActionResult Logout()
        {
            // Clear the session
            Session.Clear();
            return RedirectToAction("Login");
        }
    }

}