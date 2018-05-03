using System.Web.Mvc;
using Gov.GTB.FirmaTalepTakip.Web.Infrastructure.Concrete;

namespace Gov.GTB.FirmaTalepTakip.Web.Controllers
{
    [SessionExpireFilter]
    public class NavController : Controller
    {
        public PartialViewResult Menu()
        {
            if (Session["CurrentUser_Auths"] != null)
            {
                return PartialView(Session["CurrentUser_Auths"]);
            }

            return null;
        }
    }
}