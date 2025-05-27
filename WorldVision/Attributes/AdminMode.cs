using eUseControl.Web.Extension;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WorldVision.Domain.Entities.User;
using WorldVision.Domain.Enums;

namespace WorldVision.Attributes
{
    public class AdminMode : ActionFilterAttribute
    {
        private readonly ISession _sessionBusinessLogic;

        public AdminMode()
        {
            var businessLogic = new BussinesLogic.BussinesLogic();
            _sessionBusinessLogic = (ISession)businessLogic.GetSessionBL();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var apiCookie = HttpContext.Current.Request.Cookies["X-KEY"];
            if (apiCookie != null)
            {
                var profile = _sessionBusinessLogic.GetUserByCookie(apiCookie.Value);
                if (profile != null && profile.Level == URole.Admin)
                {
                    HttpContext.Current.SetMySessionObject(profile);
                }
                else
                {
                    filterContext.Result = new System.Web.Mvc.RedirectToRouteResult(new
                        RouteValueDictionary(new { controller = "Home", action = "Error" }));
                }
            }
            else
            {
                filterContext.Result = new System.Web.Mvc.RedirectToRouteResult(new
                        RouteValueDictionary(new { controller = "Home", action = "Error" }));
            }
        }
    }

    public interface ISession
    {
        ULoginResp UserLogin(ULoginData data);
        ULoginResp UserRegister(URegisterData data);
        HttpCookie GenCookie(string loginCredential);
        UserMinimal UserCookie(string cookie);
        List<Image> GetGalerieData();
        void DeleteImage(int imageId);
        List<UserMinimal> GetAllUsers();
        UserMinimal GetUserById(int userId);
        bool DeleteUser(int userId);
        bool ToggleUserStatus(int userId);
        bool PromoteUserToAdmin(int userId);
        int GetTotalUsersCount();
        int GetAdminUsersCount();
        int GetRegularUsersCount();
        List<UserMinimal> GetRecentUsers(int count);

        // Add the missing method definition  
        UserMinimal GetUserByCookie(string cookie);
    }

    public class Session : ISession
    {
        private readonly IUserRepository _userRepository;

        public Session(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IUserRepository UserRepository => _userRepository;

        public void DeleteImage(int imageId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }

        public HttpCookie GenCookie(string loginCredential)
        {
            throw new NotImplementedException();
        }

        public int GetAdminUsersCount()
        {
            throw new NotImplementedException();
        }

        public List<UserMinimal> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public List<Image> GetGalerieData()
        {
            throw new NotImplementedException();
        }

        public List<UserMinimal> GetRecentUsers(int count)
        {
            throw new NotImplementedException();
        }

        public int GetRegularUsersCount()
        {
            throw new NotImplementedException();
        }

        public int GetTotalUsersCount()
        {
            throw new NotImplementedException();
        }

        // Other methods...  

        public UserMinimal GetUserByCookie(string cookie)
        {
            // Implement the logic to retrieve the user by cookie  
            return UserRepository.GetUserByCookie(cookie);
        }

        public UserMinimal GetUserById(int userId)
        {
            throw new NotImplementedException();
        }

        public bool PromoteUserToAdmin(int userId)
        {
            throw new NotImplementedException();
        }

        public bool ToggleUserStatus(int userId)
        {
            throw new NotImplementedException();
        }

        public UserMinimal UserCookie(string cookie)
        {
            throw new NotImplementedException();
        }

        public ULoginResp UserLogin(ULoginData data)
        {
            throw new NotImplementedException();
        }

        public ULoginResp UserRegister(URegisterData data)
        {
            throw new NotImplementedException();
        }
    }
    // Add the missing method definition in the IUserRepository interface
    public interface IUserRepository : IReadWriteQueryRepository<int, IUser>, IReadRepository<int, IUser>, IWriteRepository<IUser>, IQueryRepository<IUser>, IRepository
    {
        // Other existing methods...

        // Add this method to resolve the CS1061 error
        UserMinimal GetUserByCookie(string cookie);
    }

    public interface IRepository
    {
    }

    public interface IQueryRepository<T>
    {
    }

    public interface IWriteRepository<T>
    {
    }

    public interface IReadRepository<T1, T2>
    {
    }

    public interface IReadWriteQueryRepository<T1, T2>
    {
    }
}