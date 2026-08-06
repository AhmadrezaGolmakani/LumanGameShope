using Luman.Busines.Services.PermissionService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
public class PermissionCheckerAttribute : Attribute, IAuthorizationFilter
{
    private readonly int[] _permissionIds;

    // دریافت یک یا چند شناسه دسترسی از طریق Attribute
    // اگه کاربر حداقل یکی از این دسترسی‌ها رو داشته باشه، اجازه عبور داده میشه
    public PermissionCheckerAttribute(params int[] permissionIds)
    {
        _permissionIds = permissionIds;
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

        // بررسی دسترسی کاربر — کافیه یکی از دسترسی‌های لیست‌شده رو داشته باشه
        bool hasPermission = _permissionIds.Any(id => permissionService.CheckPermission(id, userName));

        if (!hasPermission)
        {
            context.Result = new ForbidResult(); // 403 Forbidden
        }
    }
}