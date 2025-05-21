using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorldVision.Domain.Entities.Images;

namespace WorldVision.Models.World
{
    public class PImageData
    {
        public Image Image { get; set; }
        public List<Image> ImageList { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}