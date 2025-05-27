using System;
using System.Collections.Generic;
using System.Web;
using WorldVision.Domain.Entities.Images;
using WorldVision.Domain.Entities.User;

namespace WorldVision.BussinesLogic.Interfaces
{
    public interface ISession
    {
        // Metode existente pentru autentificare
        ULoginResp UserLogin(ULoginData data);
        ULoginResp UserRegister(URegisterData data);
        HttpCookie GenCookie(string loginCredential);
        UserMinimal UserCookie(string cookie);

        // Metode existente pentru imagini
        List<Image> GetGalerieData();
        void DeleteImage(int imageId);

        // Metode noi pentru administrare utilizatori
        List<UserMinimal> GetAllUsers();
        UserMinimal GetUserById(int userId);
        bool DeleteUser(int userId);
        bool ToggleUserStatus(int userId);
        bool PromoteUserToAdmin(int userId);

        // Metode pentru statistici admin
        int GetTotalUsersCount();
        int GetAdminUsersCount();
        int GetRegularUsersCount();
        List<UserMinimal> GetRecentUsers(int count);
    }
}