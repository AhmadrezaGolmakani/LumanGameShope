using Luman.Busines.Services.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class PermissionCheckerAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    private readonly int _permissionId;
    private IPermissionService _permissionService;

    public PermissionCheckerAttribute(int permissionId)
    {
        _permissionId = permissionId;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        _permissionService =
            (IPermissionService)context.HttpContext.RequestServices.GetService(typeof(IPermissionService));

        if (context.HttpContext.User.Identity.IsAuthenticated)
        {
            string userName = context.HttpContext.User.Identity.Name;

            // بررسی دسترسی کاربر به مجوز مورد نظر
            if (!_permissionService.CheckPermission(_permissionId, userName))
            {
                // کاربر دسترسی ندارد
                context.Result = new ObjectResult(new
                {
                    Message = "Access Denied",
                    StatusCode = 403
                })
                {
                    StatusCode = 403 // Forbidden
                };
            }
        }
        else
        {
            // کاربر احراز هویت نشده است
            context.Result = new ObjectResult(new
            {
                Message = "Unauthorized",
                StatusCode = 401
            })
            {
                StatusCode = 401 // Unauthorized
            };
        }
    }
}
