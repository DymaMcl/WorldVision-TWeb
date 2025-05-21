using System.Web;
using WorldVision.Domain.Entities.User;

namespace WorldVision.BussinesLogic.Interfaces
{
    public interface ISession
    {

        ULoginResp UserLogin(ULoginData data);
        ULoginResp UserRegister(URegisterData data);
        HttpCookie GenCookie(string loginCredential);
        UserMinimal GetUserByCookie(string apiCookieValue);

        
    }
}
