using System.Threading.Tasks;
using Ankand.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Ankand.Data.Middleware
{
    public class SessionEndMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public SessionEndMiddleware(RequestDelegate next, SignInManager<ApplicationUser> signInManager)
        {
            _next = next;
            _signInManager = signInManager;
        }

        public async Task Invoke(HttpContext context)
        {
            var user = context.User;
            if (user.Identity.IsAuthenticated)
            {
                context.Session.SetInt32("IsLoggedIn", 1); // Set a session variable to indicate the user is logged in
            }

            await _next(context);

            if (context.Session.GetInt32("IsLoggedIn") == 1)
            {
                // User was logged in; perform logout
                await _signInManager.SignOutAsync();
                context.Session.Remove("IsLoggedIn");
            }
        }
    }
}
