using System.Web.Mvc;
using System.Linq;
using Gov.GTB.FirmaTalepTakip.Repository.Interface;
using Gov.GTB.FirmaTalepTakip.Web.Infrastructure.Concrete;
using Gov.GTB.FirmaTalepTakip.Model.Entities;
using Gov.GTB.FirmaTalepTakip.Model.ViewModel;
using AutoMapper;
using System.Collections.Generic;
using Gov.GTB.FirmaTalepTakip.Web.Helpers;

namespace Gov.GTB.FirmaTalepTakip.Web.Controllers
{
    [Authorize]
    [SessionExpireFilter]
    public class FirmaController : Controller
    {
        private readonly IFirmaRepository _firmaRepository;
        private readonly IGumrukKodRepository _bolgeKodRepository;

        public FirmaController(IFirmaRepository firmaRepository, IGumrukKodRepository bolgeKodRepository)
        {
            this._firmaRepository = firmaRepository;
            this._bolgeKodRepository = bolgeKodRepository;
        }

        public ActionResult Liste()
        {
            var firmalar = _firmaRepository.FirmaListesi();
            return View(firmalar);
        }

        public ActionResult Ekle()
        {
            var firmaViewModel = new FirmaViewModel
            {
                GumrukKodlari = BolgeKodGetir(),
                IsDisabled = false
            };
            return View("Duzenle", firmaViewModel);
        }

        public ActionResult Duzenle(int firmaId)
        {
            var firmaFromDb = _firmaRepository.FirmaGetir(firmaId);
            var firmaViewModel = Mapper.Map<Firma, FirmaViewModel>(firmaFromDb);
            firmaViewModel.GumrukKodlari = BolgeKodGetir();
            firmaViewModel.IsDisabled = true;
            return View(firmaViewModel);
        }

        [HttpPost]
        public ActionResult Duzenle(FirmaViewModel firmaViewModel)
        {
            if (ModelState.IsValid)
            {
                var firma = Mapper.Map<FirmaViewModel, Firma>(firmaViewModel);
                _firmaRepository.FirmaKaydetGuncelle(firma);
                return RedirectToAction("Liste");
            }
            else
            {
                firmaViewModel.GumrukKodlari = BolgeKodGetir();
                if (firmaViewModel.FirmaId > 0)
                {
                    firmaViewModel.IsDisabled = true;
                }
                return View("Duzenle", firmaViewModel);
            }
        }

        public ActionResult Ara(string vergiNo)
        {
            var firmalar = _firmaRepository.FirmaListesi();

            if (string.IsNullOrEmpty(vergiNo))
            {
                ModelState.AddModelError("vergiNo", Resources.VergiNoEmptyErrorMsg);
                return View("Liste", firmalar);
            }

            long vergiNoParam = 0;
            if (!long.TryParse(vergiNo, out vergiNoParam))
            {
                ModelState.AddModelError("vergiNo", Resources.VergiNoFormatErrorMsg);
                return View("Liste", firmalar);
            }

            var filteredFirmalar = firmalar.Where(z => z.VergiNo == vergiNoParam);
            return View("Liste", filteredFirmalar);
        }

        private IEnumerable<GumrukKodViewModel> BolgeKodGetir()
        {
            var bolgeKodlari = _bolgeKodRepository.BolgeKodListesi();
            return Mapper.Map<IEnumerable<GumrukKod>, IEnumerable<GumrukKodViewModel>>(bolgeKodlari);
        }

        public ActionResult Sil(int firmaId)
        {
            _firmaRepository.FirmaSil(firmaId);
            return RedirectToAction("Liste");
        }
    }
}