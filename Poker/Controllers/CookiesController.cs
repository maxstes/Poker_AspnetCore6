using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Poker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CookiesController : ControllerBase
    {
        public void AddCook(string key,string value)
        {
            
            Response.Cookies.Append(key, value, new CookieOptions
            {
                HttpOnly = true
            });
        }
        public void RemoveCook(string key)
        {
            Response.Cookies.Delete(key);
        }
    }
}
