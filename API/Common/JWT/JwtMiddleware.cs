using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Moneyon.PowerBi.Domain.Service.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Moneyon.PowerBi.API.Common.JWT;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly JwtSettings _appSettings;

    public JwtMiddleware(RequestDelegate _next, IOptions<JwtSettings> _appSettings)
    {
        this._next = _next;
        this._appSettings = _appSettings.Value;

    }
    public async Task Invoke(HttpContext context, UserSecurityService userService)
    {

        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ")?.Last();
        if (!string.IsNullOrWhiteSpace(token))
        {
            //Validate Token
            var userToken= await attachUserToContext(context, userService, token);
            if(token.Trim()!=userToken.Trim())
            {
                context.Response.StatusCode=(int)HttpStatusCode.Unauthorized;
                context.Response.ContentType = "application/json";
                return;
            }
        }

        await _next(context);
    }

    private async Task<string> attachUserToContext(HttpContext context, UserSecurityService userService, string token)
    {
        
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.IssuerSigningKey));
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = _appSettings.ValidateIssuerSigningKey,
                IssuerSigningKey = key,
                ValidateIssuer = _appSettings.ValidateIssuer,
                ValidIssuer = _appSettings.ValidIssuer,
                ValidateAudience = _appSettings.ValidateAudience,
                ValidAudience = _appSettings.ValidAudience,
                RequireExpirationTime = _appSettings.RequireExpirationTime,
                ValidateLifetime = _appSettings.RequireExpirationTime,
                ClockSkew = TimeSpan.FromDays(1)
            }, out SecurityToken validateToken);


            var jwtToken = (JwtSecurityToken)validateToken;
            var userName = jwtToken.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.NameIdentifier).Value;
            var user = await userService.GetPersonByUserName(userName);
            var userToken=(await userService.GetLastTokenAsync(user!.User!.Id))?.Token ?? string.Empty;
            
            context.Items["User"] = user.User;
            
            return userToken;
        }
        catch (Exception ex)
        {

            return string.Empty;
        }
    }

}
