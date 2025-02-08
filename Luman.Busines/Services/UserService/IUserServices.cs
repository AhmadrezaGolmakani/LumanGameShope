using Luman.Busines.DTOs.UserDTO;
using Luman.DataLayer.EntityModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luman.Busines.Services.UserService
{
    public interface IUserServices
    {
        #region UserAccount

        InformationUserPanel GetUserInformation(string username);

        bool CreateUser(User user);
        User LoginUser(string username, string pass);
        bool IsCorrectpass(string username, string pass);

        bool IsExsitUserName(string username);
        bool IsExsitUserById(int userid);
        bool IsExsitEmail(string email);
        bool UpdateUser(User user);
        User GetUserByUserName(string username);
        int GetUserIdByUserName(string username);

        User GetUserByUsername(string username);
        User GetUserById(int userid);

        bool ComparePassword(string oldpass, string username);

        void ChangePassword(string username, string newPass);

        bool DeleteUser(int userId);
        void KarbareAdi(User user);

        bool Save();

        #endregion

        #region Admin

        UserForAdminDTO GetUsersForAdmin(int pageid = 1, string fillterEmail = "", string fillterUsername = "");

        bool CreateUserForAdmin(CreateUserDTO user);

        #endregion


    }
}
