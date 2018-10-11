using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SchoolExperienceSchoolUi
{
    public class Auth2Controller : Controller
    {
        public async Task Login(string returnUrl = "/")
        {
            await HttpContext.ChallengeAsync(new AuthenticationProperties() { RedirectUri = returnUrl });
        }

        [Authorize]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        [Authorize]
        public async Task SignedIn()
        {
            await Task.CompletedTask;
        }

        /// <summary>
        /// User is signed in but does not have access ot the requested page/action.
        /// The user ends up here when the API returns a 401.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ViewResult AccessDenied()
        {
            Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            return null;// View();
        }

        /// <summary>
        /// Endpoint for registration callback.
        /// Route is configured from config in <see cref="Startup.Configure"/>
        /// </summary>
        public ActionResult RegistrationComplete()
        {
            return null;// RedirectToAction("WhoAmI");
        }
    }

    public class WhoAmIViewModel
    {
        public IEnumerable<Claim> RawClaims { get; set; }
        //public OrgClaim Org { get; set; }
    }
}