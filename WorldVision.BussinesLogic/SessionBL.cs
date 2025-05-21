using WorldVision.BussinesLogic.Interfaces;
using System.Web;
using WorldVision.Domain.Entities.User;
using WorldVision.BussinesLogic.Core;

namespace WorldVision.BussinesLogic
{
    public class SessionBL : UserApi, ISession
    {
        public HttpCookie GenCookie(string loginCredential)
        {
            return Cookie(loginCredential);
        }

        public ULoginResp UserLogin(ULoginData data)
        {
            return UserLoginAction(data);
        }


        public ULoginResp UserRegister(URegisterData data)
        {
            return UserRegisterAction(data);
        }

        public UserMinimal GetUserByCookie(string apiCookieValue)
        {
            return UserCookie(apiCookieValue);
        }
    }
}
