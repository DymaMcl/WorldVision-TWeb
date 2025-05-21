using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldVision.Domain.Entities.Images;

namespace WorldVision.BusinessLogic.Interfaces
{
    public interface IGalerie
    {
        List<Image> GetGalerieData();
        void DeleteImage(int ImageID);
    }
}
