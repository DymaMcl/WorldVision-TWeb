using System;
using System.Collections.Generic;
using System.Web;
using WorldVision.BussinesLogic.Core;
using WorldVision.BussinesLogic.Interfaces;
using WorldVision.Domain.Entities.Images;
using WorldVision.Domain.Entities.User;

namespace WorldVision.BussinesLogic.Implementation
{
    public class Session : ISession
    {
        private readonly UserApi _userApi;

        public Session()
        {
            _userApi = new UserApi();
        }

        // Metode existente pentru autentificare
        public ULoginResp UserLogin(ULoginData data)
        {
            return _userApi.UserLoginAction(data);
        }

        public ULoginResp UserRegister(URegisterData data)
        {
            return _userApi.UserRegisterAction(data);
        }

        public HttpCookie GenCookie(string loginCredential)
        {
            return _userApi.Cookie(loginCredential);
        }

        public UserMinimal UserCookie(string cookie)
        {
            return _userApi.UserCookie(cookie);
        }

        // Metode existente pentru imagini
        public List<Image> GetGalerieData()
        {
            return _userApi.GetGalerieDataApi();
        }

        public void DeleteImage(int imageId)
        {
            _userApi.DeleteImageAction(imageId);
        }

        // Metode noi pentru administrare utilizatori
        public List<UserMinimal> GetAllUsers()
        {
            return _userApi.GetAllUsersAction();
        }

        public UserMinimal GetUserById(int userId)
        {
            return _userApi.GetUserByIdAction(userId);
        }

        public bool DeleteUser(int userId)
        {
            return _userApi.DeleteUserAction(userId);
        }

        public bool ToggleUserStatus(int userId)
        {
            return _userApi.ToggleUserStatusAction(userId);
        }

        public bool PromoteUserToAdmin(int userId)
        {
            return _userApi.PromoteUserToAdminAction(userId);
        }

        // Metode pentru statistici admin
        public int GetTotalUsersCount()
        {
            return _userApi.GetTotalUsersCountAction();
        }

        public int GetAdminUsersCount()
        {
            return _userApi.GetAdminUsersCountAction();
        }

        public int GetRegularUsersCount()
        {
            return _userApi.GetRegularUsersCountAction();
        }

        public List<UserMinimal> GetRecentUsers(int count)
        {
            return _userApi.GetRecentUsersAction(count);
        }
    }
}
