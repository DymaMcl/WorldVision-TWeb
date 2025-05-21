using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorldVision.Attributes;
using WorldVision.BusinessLogic.DBModel;
using WorldVision.BusinessLogic.Interfaces;
using WorldVision.Domain.Entities.Images;
using WorldVision.Models.World;

namespace WorldVision.Controllers
{
    public class WorldController : Controller
    {
        // GET: World

        private IGalerie _galerie;

        public WorldController()
        {
            var bl = new BussinesLogic.BussinesLogic();
            _galerie = bl.GetGalerieBL();
        }



        public ActionResult Index()
        {
            var data = _galerie.GetGalerieData();

            PImageData new_list = new PImageData
            {
                ImageList = new List<Image>()
            };

            foreach (var img in data)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<Image, Image>());
                var local = Mapper.Map<Image>(img);
                new_list.ImageList.Add(local);
            }

            return View(new_list);
        }



        [HttpPost][AdminMode]
        [ValidateAntiForgeryToken]
    }
}