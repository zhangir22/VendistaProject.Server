using Microsoft.AspNetCore.Mvc;

namespace VendistaProject.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
       public BaseController() { }
       
       protected string GetTokenSwagger()
        {
            var token = Request.Query["token"];
            if (!string.IsNullOrEmpty(token))
                return token!;
            var auth = HttpContext.Request.Headers.Authorization;
            if (auth.Count == 0)
                return string.Empty;

            return auth[0]!.Replace("Bearer", string.Empty);
        }
    }
}
