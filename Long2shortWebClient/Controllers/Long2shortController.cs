using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Long2shortWebClient.Models;
using MongoDB.Driver;

namespace Long2shortWebClient.Controllers
{
    public class Long2shortController : Controller
    {
        private readonly Long2shortContext ct = new Long2shortContext();
        public async Task<IActionResult> Index()
        {
            var list = await ct.Long2shortViewDetails.Find(_ => true).ToListAsync();
            return View(list);
        }
        public IActionResult GetShortUrl()
        {
            return View();
        }
        public ActionResult Long(string s)
        {
            var res = ct.Long2shortViewDetails.Find(x => x.shortid == s).SingleOrDefault();
            if (res != null)
            {
                return Redirect(res.longid.ToString());
            }
            return RedirectToAction(nameof(Error), new { action = "Error", err = "Missing the original url your shortid requested" });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Long2shortViewModel item)
        {
            Insert insert = new Insert();
            if (insert.NewShortId(ct.Long2shortViewDetails, item))
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public IActionResult Error(string err)
        {
            return View("Error", new ErrorViewModel { ErrorReason = err });
        }
    }
}