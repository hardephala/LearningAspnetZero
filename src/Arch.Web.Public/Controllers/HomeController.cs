using Microsoft.AspNetCore.Mvc;
using Arch.Web.Controllers;

namespace Arch.Web.Public.Controllers
{
    public class HomeController : ArchControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}