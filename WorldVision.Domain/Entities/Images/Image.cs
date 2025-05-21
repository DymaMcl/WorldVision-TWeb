using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldVision.Domain.Entities.Images
{
    public class Image
    {
        public int ImageID { get; set; }

        public string Title { get; set; }
        public string ImagePath { get; set; }
    }
}
