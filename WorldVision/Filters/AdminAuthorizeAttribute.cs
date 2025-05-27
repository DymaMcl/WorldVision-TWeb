using System;
using System.Web;
using System.Web.Mvc;

namespace WorldVision.Web.Filters
{
    /// <summary>
    /// Atribut personalizat pentru a verifica dacă utilizatorul este administrator
    /// </summary>
    public class AdminAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            // Verifică dacă utilizatorul este logat
            if (HttpContext.Current.Session["username"] == null)
            {
                // Utilizatorul nu este logat - redirecționează la login
                filterContext.Result = new RedirectResult("/Login/Index");
                return;
            }

            // Verifică dacă utilizatorul este admin
            var userRole = HttpContext.Current.Session["role"];
            if (userRole == null || userRole.ToString() != "Admin")
            {
                // Utilizatorul nu este admin - redirecționează la pagina principală cu eroare
                filterContext.Controller.TempData["ErrorMessage"] = "Nu aveți permisiunea de a accesa această pagină.";
                filterContext.Result = new RedirectResult("/Home/Index");
                return;
            }

            // Utilizatorul este admin - permite accesul
            base.OnAuthorization(filterContext);
        }
    }

    /// <summary>
    /// Atribut pentru a verifica doar dacă utilizatorul este logat (nu neapărat admin)
    /// </summary>
    public class LoginRequiredAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (HttpContext.Current.Session["username"] == null)
            {
                filterContext.Result = new RedirectResult("/Login/Index");
                return;
            }

            base.OnAuthorization(filterContext);
        }
    }
}

// Exemplu 
/*
using WorldVision.Web.Filters;

namespace WorldVision.Web.Controllers
{
    [AdminAuthorize] // Toate acțiunile din acest controller necesită admin
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Users()
        {
            return View();
        }
        
        public ActionResult Dashboard()
        {
            return View();
        }
    }
}

// Sau pentru acțiuni individuale:
public class SomeController : Controller
{
    [AdminAuthorize] // Doar această acțiune necesită admin
    public ActionResult AdminOnlyAction()
    {
        return View();
    }
    
    [LoginRequired] // Această acțiune necesită doar login
    public ActionResult LoggedInUserAction()
    {
        return View();
    }
}
*/