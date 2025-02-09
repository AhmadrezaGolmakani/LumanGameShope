using Luman.Busines.Services.PermissionService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

public class PermissionCheckerAttribute : Attribute, IAuthorizationFilter
{
    private readonly int _permissionId;

    // دریافت شناسه دسترسی مورد نظر از طریق Attribute
    public PermissionCheckerAttribute(int permissionId)
    {
        _permissionId = permissionId;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (context.HttpContext.User.Identity?.IsAuthenticated != true)
        {
            context.Result = new UnauthorizedResult(); // 401 Unauthorized
            return;
        }

        // گرفتن سرویس از طریق Service Locator
        var permissionService = context.HttpContext.RequestServices.GetService<IPermissionService>();

        if (permissionService == null)
        {
            context.Result = new StatusCodeResult(500); // خطای داخلی سرور
            return;
        }

        string userName = context.HttpContext.User.Identity.Name;

        // بررسی دسترسی کاربر
        bool hasPermission = permissionService.CheckPermission(_permissionId, userName);

        if (!hasPermission)
        {
            context.Result = new ForbidResult(); // 403 Forbidden
        }
    }
}
