using Microsoft.AspNetCore.Mvc;
using NoticeBoard.Web.Models;
using System.Diagnostics;

namespace NoticeBoard.Web.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error")]
        public IActionResult Index()
        {
            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
