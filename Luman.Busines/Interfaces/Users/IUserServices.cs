using Luman.Api.DTOs;
using Luman.Busines.DTOs.UserDTO;
using Luman.DataLayer.EntityModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luman.Busines.Interfaces.Users
{
    public interface IUserServices
    {
        #region UserAccount

        InformationUserPanel GetUserInformation(string username);

        bool CreateUser(User user);
        User LoginUser(string username , string pass);
        bool IsCorrectpass(string username , string pass);

        bool IsExsitUserName(string username);
        bool IsExsitEmail(string email);
        bool UpdateUser(User user);
        User GetUserByUserName(string username);
        int GetUserIdByUserName(string username);

        User GetUserById(int userid);

        bool ComparePassword(string oldpass , string username);

        void ChangePassword(string username , string newPass);

        bool Save();

        #endregion


    }
}
