using Luman.DataLayer.EntityModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luman.Busines.Services.PermissionService
{
    public interface IPermissionService
    {


        #region Role

        List<Role> GetRole();
        void AddRoleUser(int RoleId, int userId);

        Role GetRoleById(int roleId);


        #endregion

        #region Permission

        List<DataLayer.EntityModel.Permitions.Permition> GetAllPermission();

        bool CheckPermission(int permissionId, string? username);
        #endregion


    }
}
