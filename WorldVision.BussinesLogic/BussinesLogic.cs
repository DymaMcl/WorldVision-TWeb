using WorldVision.BusinessLogic.Interfaces;
using WorldVision.BusinessLogic.LoginBL;
using WorldVision.BussinesLogic.Interfaces;

namespace WorldVision.BussinesLogic
{
    public class BussinesLogic
    {
        public ISession GetSessionBL()
        {
            return new SessionBL();
        }
        public IGalerie GetGalerieBL()
        {
            return new GalerieBL();
        }

       

    }
}
