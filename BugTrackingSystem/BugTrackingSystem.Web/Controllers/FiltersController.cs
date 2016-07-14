using System.Web.Mvc;
using BugTrackingSystem.Web.Filters;

namespace BugTrackingSystem.Web.Controllers
{
    [CustomAuthorize(Roles = "admin")]
    public class FiltersController : Controller
    {
        //
        // GET: /Filters/
        public ActionResult Filters()
        {
            
            return View();
        }
    }
}