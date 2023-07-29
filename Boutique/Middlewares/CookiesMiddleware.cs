namespace Boutique.Middlewares
{
    public class CookiesMiddleware
    {
        private readonly RequestDelegate _next;

        public CookiesMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (String.IsNullOrEmpty(context.Request.Cookies["miniCartDetail"]) || context.Session.Get("Cart")==null)
            {
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(1),
                    Path = "/"
                };
                context.Response.Cookies.Append("miniCartDetail", "0", cookieOptions);
            }
            await _next(context);
        }

    }
}
