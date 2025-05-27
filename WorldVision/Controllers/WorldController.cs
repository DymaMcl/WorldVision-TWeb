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



        [HttpPost]
        [AdminMode]
        [ValidateAntiForgeryToken]

        public ActionResult Add(PImageData model)
        {
            string fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
            string extension = Path.GetExtension(model.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            model.Image.ImagePath = "~/Images/politics/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Images/politics/"), fileName);
            model.ImageFile.SaveAs(fileName);



            IDbTable new_img = new IDbTable();

            Mapper.Initialize(cfg => cfg.CreateMap<Image, Image>());
            var image = Mapper.Map<Image>(model.Image);

            using (ImageContext db = new ImageContext())
            {
                new_img.ImageID = image.ImageID;
                new_img.Title = image.Title;
                new_img.ImagePath = image.ImagePath;

                db.Images.Add(new_img);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "World");
        }



        [AdminMode]
        public ActionResult ImageDelete(string ImageID)
        {
            var bl = new BussinesLogic.BussinesLogic();
            _galerie = bl.GetGalerieBL();
            int id = int.Parse(ImageID);
            _galerie.DeleteImage(id);
            return RedirectToAction("Index", "World");
        }



    }
}