using System;
using System.Web;
using System.Web.Mvc;
using WorldVision.BussinesLogic.Interfaces;
using WorldVision.Domain.Entities.User;
using WorldVision.Web.Models;

namespace WorldVision.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly ISession _session;

        public LoginController()
        {
            var bl = new BussinesLogic.BussinesLogic();
            _session = bl.GetSessionBL();
        }

        // GET: Login
        public ActionResult Index()
        {
            // Dacă utilizatorul este deja logat, redirecționează
            if (Session["username"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UserLogin login)
        {
            if (ModelState.IsValid)
            {
                ULoginData data = new ULoginData
                {
                    Credetial = login.Credential,
                    Password = login.Password,
                    LoginIp = Request.UserHostAddress,
                    LoginDataTime = DateTime.Now
                };

                TempData["UserName"] = login.Credential;
                var userLogin = _session.UserLogin(data);

                if (userLogin.Status)
                {
                    // Generează și setează cookie-ul
                    HttpCookie cookie = _session.GenCookie(login.Credential);
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);

                    // Setează datele în sesiune
                    Session["username"] = login.Credential;
                    Session["role"] = userLogin.Role ?? "User";

                    // Opțional: setează și alte informații utile
                    Session["userId"] = userLogin.UserId; // dacă ai acest câmp
                    Session["loginTime"] = DateTime.Now;

                    // Logging pentru admin login
                    if (userLogin.Role == "2")
                    {
                        // Log admin login pentru securitate
                        System.Diagnostics.Debug.WriteLine($"Admin login: {login.Credential} at {DateTime.Now}");
                    }

                    // Redirecționare condiționată
                    if (userLogin.Role == "2")
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", userLogin.StatusMsg);
                    return View();
                }
            }
            return View();
        }

        // Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            // Log logout pentru admin
            if (Session["role"] != null && Session["role"].ToString() == "2")
            {
                System.Diagnostics.Debug.WriteLine($"Admin logout: {Session["username"]} at {DateTime.Now}");
            }

            // Curăță sesiunea
            Session.Clear();
            Session.Abandon();

            // Șterge cookie-ul
            if (Request.Cookies["UserLoginCookie"] != null)
            {
                var cookie = new HttpCookie("UserLoginCookie");
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }

            return RedirectToAction("Index", "Home");
        }


        // Verifică dacă utilizatorul este admin (helper method)
        public bool IsAdmin()
        {
            return Session["role"] != null && Session["role"].ToString() == "2";
        }

        // Verifică dacă utilizatorul este logat
        public bool IsLoggedIn()
        {
            return Session["username"] != null;
        }
    }
}