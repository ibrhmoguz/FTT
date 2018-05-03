using System.Web.Mvc;
using Gov.GTB.FirmaTalepTakip.Web.Infrastructure.Concrete;

namespace Gov.GTB.FirmaTalepTakip.Web.Controllers
{
    [Authorize]
    [SessionExpireFilter]
    public class DefaultController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}