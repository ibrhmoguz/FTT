using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Gov.GTB.FirmaTalepTakip.Model.Entities;
using Gov.GTB.FirmaTalepTakip.Model.ViewModel;
using Gov.GTB.FirmaTalepTakip.Repository.Interface;
using Gov.GTB.FirmaTalepTakip.Web.Infrastructure.Concrete;

namespace Gov.GTB.FirmaTalepTakip.Web.Controllers
{
    [Authorize]
    [SessionExpireFilter]
    public class GorevlendirController : Controller
    {
        private readonly IFirmaRepository _firmaRepository;

        public GorevlendirController(IFirmaRepository firmaRepository)
        {
            _firmaRepository = firmaRepository;
        }

        public ActionResult Liste()
        {
            var firmalar = _firmaRepository.FirmaListesi();
            var firmaViewModel = Mapper.Map<IEnumerable<Firma>, IEnumerable<FirmaViewModel>>(firmalar);
            return View(firmalar);
        }
    }
}