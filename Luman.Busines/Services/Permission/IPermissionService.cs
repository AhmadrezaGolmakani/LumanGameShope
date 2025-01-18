using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luman.Busines.Services.Permission
{
    public interface IPermissionService
    {
        bool CheckPermission(int permissionId, string? userName);
    }
}
