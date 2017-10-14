using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Long2shortWebClient.Models;
namespace Long2shortWebClient.Controllers
{

    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        public IActionResult GenerateShortURL()
        {
            return View();
        }
        
    }
}
