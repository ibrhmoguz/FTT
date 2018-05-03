using System.Web.Mvc;
using System.Linq;
using Gov.GTB.FirmaTalepTakip.Repository.Interface;
using Gov.GTB.FirmaTalepTakip.Web.Infrastructure.Concrete;
using System;
using Gov.GTB.FirmaTalepTakip.Model.Entities;
using Gov.GTB.FirmaTalepTakip.Model.ViewModel;
using AutoMapper;

namespace Gov.GTB.FirmaTalepTakip.Web.Controllers
{
    [Authorize]
    [SessionExpireFilter]
    public class FirmaController : Controller
    {
        private IFirmaRepository firmaRepository;

        public FirmaController(IFirmaRepository firmaRepository)
        {
            this.firmaRepository = firmaRepository;
        }

        // GET: Firma
        public ActionResult Liste()
        {
            var firmalar = firmaRepository.FirmaListesi();
            return View(firmalar);
        }

        public ActionResult Ekle()
        {
            var firmaViewModel = new FirmaViewModel();
            return View("Duzenle", firmaViewModel);
        }

        public ActionResult Duzenle(int firmaId)
        {
            var firmaFromDb = firmaRepository.FirmaGetir(firmaId);
            var firmaViewModel = Mapper.Map<Firma, FirmaViewModel>(firmaFromDb);
            return View(firmaViewModel);
        }

        public RedirectToRouteResult KaydetGuncelle(FirmaViewModel firmaViewModel)
        {
            var firma = Mapper.Map<FirmaViewModel, Firma>(firmaViewModel);
            firmaRepository.FirmaKaydetGuncelle(firma);
            return RedirectToAction("Liste");
        }

        public ActionResult Ara(string vergiNo)
        {
            var vergiNoParam = Convert.ToInt64(vergiNo);
            var firmalar = firmaRepository.FirmaListesi();
            var filteredFirmalar = firmalar.Where(z => z.VergiNo == vergiNoParam);
            return View("Liste", filteredFirmalar);
        }
    }
}