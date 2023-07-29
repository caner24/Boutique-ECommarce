using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Boutique.Middlewares
{
    public class CustomAuthMiddleware 
    {
        private readonly RequestDelegate _next;

        public CustomAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path=="/Admin" &&  !context.User.IsInRole("Administrator"))
            {
                context.Response.StatusCode = 403;
                return;
            }
            await _next(context);
        }
    }
}
