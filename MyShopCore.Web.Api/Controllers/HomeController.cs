using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyShopCore.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public string Index()
        {
            return "You are have reached our home";
        }
    }
}
