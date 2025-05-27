using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WorldVision.BussinesLogic.Interfaces;
using WorldVision.Web.Filters;
using WorldVision.Domain.Entities.User;
using WorldVision.Domain.Enums;

namespace WorldVision.Web.Controllers
{
    [AdminAuthorize] // Tot controller-ul protejat pentru admin
    public class AdminController : Controller
    {
        private readonly ISession _session;

        public AdminController()
        {
            var bl = new BussinesLogic.BussinesLogic();
            _session = bl.GetSessionBL();
        }

        // Dashboard principal admin
        public ActionResult Index()
        {
            ViewBag.AdminName = Session["username"];
            ViewBag.LoginTime = Session["loginTime"];
            return View();
        }

        // Gestionare utilizatori - listare
        public ActionResult Users()
        {
            try
            {
                var users = _session.GetAllUsers(); // Va trebui să adaugi această metodă în BL
                return View(users);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Eroare la încărcarea utilizatorilor: " + ex.Message;
                return View(new List<UserMinimal>());
            }
        }

        // Detalii utilizator
        public ActionResult UserDetails(int id)
        {
            try
            {
                var user = _session.GetUserById(id); // Va trebui să adaugi această metodă în BL
                if (user == null)
                {
                    TempData["ErrorMessage"] = "Utilizatorul nu a fost găsit.";
                    return RedirectToAction("Users");
                }
                return View(user);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Eroare la încărcarea detaliilor: " + ex.Message;
                return RedirectToAction("Users");
            }
        }

        // Confirmare ștergere utilizator
        public ActionResult DeleteUser(int id)
        {
            try
            {
                var user = _session.GetUserById(id);
                if (user == null)
                {
                    TempData["ErrorMessage"] = "Utilizatorul nu a fost găsit.";
                    return RedirectToAction("Users");
                }

                // Verifică dacă nu încearcă să se șteargă pe el însuși
                if (user.Username == Session["username"].ToString())
                {
                    TempData["ErrorMessage"] = "Nu poți să te ștergi pe tine însuți!";
                    return RedirectToAction("Users");
                }

                return View(user);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Eroare: " + ex.Message;
                return RedirectToAction("Users");
            }
        }

        // Ștergere utilizator - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUserConfirmed(int id)
        {
            try
            {
                var user = _session.GetUserById(id);
                if (user == null)
                {
                    TempData["ErrorMessage"] = "Utilizatorul nu a fost găsit.";
                    return RedirectToAction("Users");
                }

                // Verifică din nou dacă nu încearcă să se șteargă pe el însuși
                if (user.Username == Session["username"].ToString())
                {
                    TempData["ErrorMessage"] = "Nu poți să te ștergi pe tine însuți!";
                    return RedirectToAction("Users");
                }

                // Verifică dacă nu încearcă să șteargă alt admin
                if (user.Level == URole.Admin)
                {
                    TempData["ErrorMessage"] = "Nu poți să ștergi alți administratori!";
                    return RedirectToAction("Users");
                }

                var result = _session.DeleteUser(id); // Va trebui să adaugi această metodă în BL

                if (result)
                {
                    TempData["SuccessMessage"] = $"Utilizatorul '{user.Username}' a fost șters cu succes.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Eroare la ștergerea utilizatorului.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Eroare la ștergerea utilizatorului: " + ex.Message;
            }

            return RedirectToAction("Users");
        }

        // Blochează/Deblochează utilizator
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ToggleUserStatus(int id)
        {
            try
            {
                var user = _session.GetUserById(id);
                if (user == null)
                {
                    return Json(new { success = false, message = "Utilizatorul nu a fost găsit." });
                }

                if (user.Username == Session["username"].ToString())
                {
                    return Json(new { success = false, message = "Nu poți să te blochezi pe tine însuți!" });
                }

                if (user.Level == URole.Admin)
                {
                    return Json(new { success = false, message = "Nu poți să blochezi alți administratori!" });
                }

                var result = _session.ToggleUserStatus(id); // Va trebui să adaugi această metodă în BL

                if (result)
                {
                    return Json(new { success = true, message = "Statusul utilizatorului a fost modificat." });
                }
                else
                {
                    return Json(new { success = false, message = "Eroare la modificarea statusului." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Eroare: " + ex.Message });
            }
        }

        // Promovează utilizator la admin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PromoteToAdmin(int id)
        {
            try
            {
                var user = _session.GetUserById(id);
                if (user == null)
                {
                    return Json(new { success = false, message = "Utilizatorul nu a fost găsit." });
                }

                if (user.Level == URole.Admin)
                {
                    return Json(new { success = false, message = "Utilizatorul este deja administrator." });
                }

                var result = _session.PromoteUserToAdmin(id); // Va trebui să adaugi această metodă în BL

                if (result)
                {
                    return Json(new { success = true, message = $"Utilizatorul '{user.Username}' a fost promovat la administrator." });
                }
                else
                {
                    return Json(new { success = false, message = "Eroare la promovarea utilizatorului." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Eroare: " + ex.Message });
            }
        }

        // Statistici rapide
        public ActionResult Dashboard()
        {
            try
            {
                var stats = new
                {
                    TotalUsers = _session.GetTotalUsersCount(),
                    AdminUsers = _session.GetAdminUsersCount(),
                    RegularUsers = _session.GetRegularUsersCount(),
                    RecentUsers = _session.GetRecentUsers(5) // Ultimii 5 utilizatori înregistrați
                };

                return View(stats);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Eroare la încărcarea statisticilor: " + ex.Message;
                return View();
            }
        }

        // Setări generale
        public ActionResult Settings()
        {
            return View();
        }

        // Gestionare imagini (din codul tău existent)
        public ActionResult Images()
        {
            try
            {
                var images = _session.GetGalerieData(); // Folosești metoda existentă
                return View(images);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Eroare la încărcarea imaginilor: " + ex.Message;
                return View();
            }
        }

        // Ștergere imagine
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteImage(int id)
        {
            try
            {
                _session.DeleteImage(id); // Folosești metoda existentă
                TempData["SuccessMessage"] = "Imaginea a fost ștearsă cu succes.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Eroare la ștergerea imaginii: " + ex.Message;
            }

            return RedirectToAction("Images");
        }
    }
}