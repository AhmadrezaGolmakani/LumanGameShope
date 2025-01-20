using Luman.Busines.DTOs.UserDTO;
using Luman.DataLayer.EntityModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luman.Busines.Services.User
{
    public interface IUserServices
    {
        #region UserAccount

        InformationUserPanel GetUserInformation(string username);

        bool CreateUser(DataLayer.EntityModel.User.User user);
        DataLayer.EntityModel.User.User LoginUser(string username, string pass);
        bool IsCorrectpass(string username, string pass);

        bool IsExsitUserName(string username);
        bool IsExsitEmail(string email);
        bool UpdateUser(DataLayer.EntityModel.User.User user);
        DataLayer.EntityModel.User.User GetUserByUserName(string username);
        int GetUserIdByUserName(string username);

        DataLayer.EntityModel.User.User GetUserById(int userid);

        bool ComparePassword(string oldpass, string username);

        void ChangePassword(string username, string newPass);

        bool DeleteUser(int userId);
        void KarbareAdi(DataLayer.EntityModel.User.User user);

        bool Save();

        #endregion

        #region Admin

        UserForAdminDTO GetUsersForAdmin(int pageid = 1, string fillterEmail = "", string fillterUsername = "");

        bool CreateUserForAdmin(CreateUserDTO user);

        #endregion


    }
}
