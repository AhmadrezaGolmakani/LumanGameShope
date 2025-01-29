using Luman.Busines.Services.PermissionService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

public class PermissionCheckerAttribute : Attribute, IAuthorizationFilter
{
    private readonly int _permissionId;
    private IPermissionService _permissionService;

    // Constructor to inject permissionId
    public PermissionCheckerAttribute(int permissionId)
    {
        _permissionId = permissionId;
    }

    // This method is called to perform the authorization logic
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // Getting the IPermissionService via DI (Dependency Injection)
        _permissionService = (IPermissionService)context.HttpContext.RequestServices.GetService(typeof(IPermissionService));

        if (context.HttpContext.User.Identity.IsAuthenticated)
        {
            string userName = context.HttpContext.User.Identity.Name;

            // Check if the user has the required permission
            if (!_permissionService.CheckPermission(_permissionId, userName))
            {
                // If user does not have permission, return a 403 Forbidden response
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
            // If the user is not authenticated, return a 401 Unauthorized response
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
