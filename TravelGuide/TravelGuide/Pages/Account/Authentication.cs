using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using DataBase.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace TravelGuide.Pages.Account
{
    public class Authentication
    {
        public static async Task Authenticate(Person user, HttpContext ctx)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimTypes.NameIdentifier, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await ctx.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}