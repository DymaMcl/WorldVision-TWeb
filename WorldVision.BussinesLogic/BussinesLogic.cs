﻿using WorldVision.BusinessLogic.Interfaces;
using WorldVision.BusinessLogic.LoginBL;
using WorldVision.BussinesLogic.Implementation;
using WorldVision.BussinesLogic.Interfaces;

namespace WorldVision.BussinesLogic
{
    public class BussinesLogic
    {
        public ISession GetSessionBL()
        {
            return new Session();
        }
        public IGalerie GetGalerieBL()
        {
            return new GalerieBL();
        }
    }
}
