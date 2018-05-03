using System.Web.Mvc;
using System.Linq;
using Gov.GTB.FirmaTalepTakip.Repository.Interface;
using Gov.GTB.FirmaTalepTakip.Web.Infrastructure.Concrete;
using System;
using Gov.GTB.FirmaTalepTakip.Model.Entities;
using Gov.GTB.FirmaTalepTakip.Model.ViewModel;
using AutoMapper;
using System.Collections.Generic;

namespace Gov.GTB.FirmaTalepTakip.Web.Controllers
{
    [Authorize]
    [SessionExpireFilter]
    public class FirmaController : Controller
    {
        private IFirmaRepository firmaRepository;
        private IGumrukKodRepository bolgeKodRepository;

        public FirmaController(IFirmaRepository firmaRepository, IGumrukKodRepository bolgeKodRepository)
        {
            this.firmaRepository = firmaRepository;
            this.bolgeKodRepository = bolgeKodRepository;
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
            firmaViewModel.GumrukKodlari = BolgeKodGetir();
            firmaViewModel.IsDisabled = false;
            return View("Duzenle", firmaViewModel);
        }

        public ActionResult Duzenle(int firmaId)
        {
            var firmaFromDb = firmaRepository.FirmaGetir(firmaId);
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
                firmaRepository.FirmaKaydetGuncelle(firma);
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
            var vergiNoParam = Convert.ToInt64(vergiNo);
            var firmalar = firmaRepository.FirmaListesi();
            var filteredFirmalar = firmalar.Where(z => z.VergiNo == vergiNoParam);
            return View("Liste", filteredFirmalar);
        }

        private IEnumerable<GumrukKodViewModel> BolgeKodGetir()
        {
            var bolgeKodlari = bolgeKodRepository.BolgeKodListesi();
            return Mapper.Map<IEnumerable<GumrukKod>, IEnumerable<GumrukKodViewModel>>(bolgeKodlari);
        }
    }
}