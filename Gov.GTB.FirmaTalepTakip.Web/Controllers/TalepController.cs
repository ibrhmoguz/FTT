using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gov.GTB.FirmaTalepTakip.Web.Infrastructure.Concrete;

namespace Gov.GTB.FirmaTalepTakip.Web.Controllers
{
    [Authorize]
    [SessionExpireFilter]
    public class TalepController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}