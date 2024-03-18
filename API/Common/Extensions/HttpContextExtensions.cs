using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Moneyon.Common.Collections;
using System.Security.Claims;

namespace API.Common.Extensions
{
    public static class HttpContextExtensions
    {
        public static async Task SignInAsync(this HttpContext httpContext, string userId, string[]? userRoles)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, userId),
            };

            userRoles?.ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30),
                IsPersistent = true,
                //IssuedUtc = <DateTimeOffset>,
                RedirectUri = "Signin"
            };

            await httpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }


        public static async Task SignInAsync(this HttpContext httpContext, string userId, string[]? userRoles, string[]? userPermissions)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, userId),
            };

            userRoles?.ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));
            userPermissions?.ForEach(per => claims.Add(new Claim("permission", per)));
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30),
                IsPersistent = true,
                //IssuedUtc = <DateTimeOffset>,
                //RedirectUri = "Signin"
            };

            await httpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }


    }
}
