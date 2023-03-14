using Microsoft.AspNetCore.Mvc;
using webapi.Entities;

namespace webapi.Controllers
{
    [Controller]
    public class BaseController : ControllerBase
    {
        public Account Account => (Account)HttpContext.Items["Account"];
    }
}
