using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Org.BouncyCastle.Asn1.Ocsp;
using YAF.Types.Modals;
using YAF.Types.Models;

namespace YAF.Core.Identity.Owin
{
    public class FiderosCookiesAuthent : IHaveServiceLocator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FiderosCookiesAuthent"/> class.
        /// </summary>
        /// <param name="serviceLocator">
        /// The service locator.
        /// </param>
        public FiderosCookiesAuthent(IServiceLocator serviceLocator)
        {
            this.ServiceLocator = serviceLocator;
        }

        /// <summary>
        /// Gets or sets ServiceLocator.
        /// </summary>
        public IServiceLocator ServiceLocator { get; protected set; }


        public async Task<IActionResult> SignInCookiesAuth(HttpContext context, AspNetUsers user, string userName, bool persistCookie = false)
        {
            #region COOKIES Auth
            await SignOutCookiesAuth(context);
            var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Role, "REGISTERED USERS")
            };

            //Comment for testing : STI
            //var claimsIdentity = new ClaimsIdentity(
            //    claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var claimsIdentity = new ClaimsIdentity(
               claims, "Identity.Application");

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                // Refreshing the authentication session should be allowed.

                ExpiresUtc = DateTime.UtcNow.AddDays(30),
                // The time at which the authentication ticket expires. A 
                // value set here overrides the ExpireTimeSpan option of 
                // CookieAuthenticationOptions set with AddCookie.

                IsPersistent = persistCookie,
                // Whether the authentication session is persisted across 
                // multiple requests. When used with cookies, controls
                // whether the cookie's lifetime is absolute (matching the
                // lifetime of the authentication ticket) or session-based.

                IssuedUtc = DateTime.UtcNow,
                // The time at which the authentication ticket was issued.

                //RedirectUri = <string>
                // The full path or absolute URI to be used as an http 
                // redirect response value.
            };           

            var userPrincipal = new ClaimsPrincipal(claimsIdentity);

            await context.SignInAsync(
               "Identity.Application",
                userPrincipal,
                authProperties);

            context.User = userPrincipal;

            user.AccessFailedCount = 0;
            user.LockoutEndDateUtc = null;

            await this.Get<IAspNetUsersHelper>().UpdateUserAsync(user);

            this.Get<IRaiseEvent>().Raise(new SuccessfulUserLoginEvent(BoardContext.Current.PageUserID));

            return this.Get<LinkBuilder>().Redirect(ForumPages.Index);
            #endregion           
        }

        /// <summary>
        /// The sign out.
        /// </summary>
        public Task SignOutCookiesAuth(HttpContext context)
        {
           //if (_schemes.GetSchemeAsync(IdentityConstants.ExternalScheme) != null)
           // {
           //     await context.SignOutAsync(IdentityConstants.ExternalScheme);
           // }
           // if (_schemes.GetSchemeAsync(IdentityConstants.TwoFactorUserIdScheme) != null)
           // {
           //     await context.SignOutAsync(IdentityConstants.TwoFactorUserIdScheme);
           // }
            return context.SignOutAsync("Identity.Application");
        }

        public async Task<dynamic> UpdateUserFiderosData()
        {
            return await this.Get<dynamic>();
        }
    }
}
