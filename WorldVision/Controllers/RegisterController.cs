using WorldVision.Domain.Entities.User;
using System;
using System.Web.Mvc;
using WorldVision.Models;
using WorldVision.BussinesLogic.Interfaces;
using WorldVision.BussinesLogic;

namespace WorldVision.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        private readonly ISession _session;
        public RegisterController()
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