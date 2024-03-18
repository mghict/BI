using Microsoft.Extensions.Options;
using Moneyon.PowerBi.Common.Extensions;
using Moneyon.PowerBi.Domain.Model.Modeling;
using Moneyon.PowerBi.Domain.Service.Services;
using System.Net;

namespace Moneyon.PowerBi.API.Common.JWT;

public class JwtReportMiddleware
{
    private readonly RequestDelegate _next;
    private readonly JwtSettings _appSettings;

    public JwtReportMiddleware(RequestDelegate _next, IOptions<JwtSettings> _appSettings)
    {
        this._next = _next;
        this._appSettings = _appSettings.Value;

    }

    public async Task InvokeAsync(HttpContext context, UserSecurityService userService)
    {
        User user = context.Items["User"] as User;
        if(user is null)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            context.Response.ContentType = "application/json";
            return;
        }
        var address = context.Request.Path.ToString();
        if (address.Contains("/api/bi/reports"))
        {
            var reportId = context.Request.Query["reportId"].ToString().ToLong();
            var isAccess = await userService.CheckUserReportAccess(reportId, user);
            if (!isAccess)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Response.ContentType = "application/json";
                return;
            }
        }

        await _next(context);
    }
}
