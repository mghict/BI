using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Moneyon.PowerBi.Domain.Model.Modeling;

namespace Moneyon.PowerBi.API;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class JWTAuthorization : Attribute, Microsoft.AspNetCore.Mvc.Filters.IAuthorizationFilter
{
    private readonly IList<string> _permission;

    public JWTAuthorization(params PermissionEnum[] _permission)
    {
        this._permission = new List<string>();
        if (_permission is not null)
        {
            
            foreach(var permission in  _permission)
            {
                this._permission.Add(((int)permission).ToString());
            }
        }
    }
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        
        var isPermission = false;
        User user = context.HttpContext.Items["User"] as User;
        if (user is null)
        {
            context.Result = new JsonResult(
                    new { Message = "Unauthorization" }
                )
            { StatusCode = StatusCodes.Status401Unauthorized };

            return;
        }

        if (_permission is null || _permission.Count == 0)
            return;

        var userPermission = user!.GetPermission();
        if(userPermission is null) {
            context.Result = new JsonResult(
                    new { Message = "Unauthorization" }
                )
            { StatusCode = StatusCodes.Status401Unauthorized };
            return;
        }

        if(userPermission!.Any(p=>_permission.Contains(p)))
            isPermission=true;

        if (!isPermission)
        {
            context.Result = new JsonResult(
                       new { Message = "Unauthorization" }
                   )
            { StatusCode = StatusCodes.Status401Unauthorized };
            return;
        }
    }
}
