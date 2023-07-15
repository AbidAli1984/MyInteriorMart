using BOL.IDENTITY;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAL.Middleware
{
    public class BlazorCookieLoginMiddleware
    {
        public static IDictionary<Guid, LoginInfo> Logins { get; private set; }
            = new ConcurrentDictionary<Guid, LoginInfo>();


        private readonly RequestDelegate _next;

        public BlazorCookieLoginMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, SignInManager<ApplicationUser> signInMgr)
        {
            string[] paths = { "/", "/MyAccount/UserProfile", "/MyAccount/UserAddress" };
            if (paths.Contains(context.Request.Path.ToString()) && context.Request.Query.ContainsKey("key"))
            {
                var key = Guid.Parse(context.Request.Query["key"]);
                var info = Logins[key];

                var result = await signInMgr.PasswordSignInAsync(info.Email, info.Password, false, lockoutOnFailure: true);
                info.Password = null;
                if (result.Succeeded)
                {
                    Logins.Remove(key);
                    context.Response.Redirect(context.Request.Path);
                    return;
                }
                else if (result.RequiresTwoFactor)
                {
                    //TODO: redirect to 2FA razor component
                    context.Response.Redirect("/loginwith2fa/" + key);
                    return;
                }
                else
                {
                    //TODO: Proper error handling
                    context.Response.Redirect("/loginfailed");
                    return;
                }
            }
            else
            {
                await _next.Invoke(context);
            }
        }
    }

}
