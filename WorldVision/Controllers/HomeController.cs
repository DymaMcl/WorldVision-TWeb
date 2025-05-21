using eUseControl.Web.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorldVision.Models.Images;
using WorldVision.Web.Controllers;

namespace WorldVision.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return View(new DataRequest());
            }

            var user = System.Web.HttpContext.Current.GetMySessionObject();

            DataRequest data = new DataRequest()
            {
                UserName = user.Username,
                Level = user.Level,

            };

            return View(data);
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Gallery()
        {
            return View();
        }
        public ActionResult Magazine()
        {
            return View();
        }
        public ActionResult Blog()
        {
            return View();
        }
        public ActionResult Sports()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Realestate()
        {
            return View();
        }

        public ActionResult Forgot()
        {
            return View();
        }
    }
}