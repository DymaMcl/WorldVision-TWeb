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

            return View();
        }
    }
}