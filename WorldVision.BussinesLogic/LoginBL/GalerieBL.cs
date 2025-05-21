using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldVision.BusinessLogic.Interfaces;
using WorldVision.BussinesLogic.Core;
using WorldVision.Domain.Entities.Images;

namespace WorldVision.BusinessLogic.LoginBL
{
    public class GalerieBL : UserApi, IGalerie
    {
        public List<Image> GetGalerieData()
        {
            return GetGalerieDataApi();
        }

        public void DeleteImage(int ImageId)
        {
            DeleteImageAction(ImageId);
        }
    }
}
