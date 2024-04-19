namespace VendistaProject.Server.Middlewares
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate requestDelegate;
        public TokenMiddleware(RequestDelegate requestDelegate)
        {
            this.requestDelegate = requestDelegate;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Query["token"];
            if (token != "")
            {
                await requestDelegate.Invoke(context);
            }
        }
    }
}
