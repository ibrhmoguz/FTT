using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Gov.GTB.FirmaTalepTakip.Model.Entities;
using Gov.GTB.FirmaTalepTakip.Model.Enums;
using Gov.GTB.FirmaTalepTakip.Model.ViewModel;
using Gov.GTB.FirmaTalepTakip.Repository.Interface;
using Gov.GTB.FirmaTalepTakip.Repository.Repository;
using Gov.GTB.FirmaTalepTakip.Web.Helpers;
using Gov.GTB.FirmaTalepTakip.Web.Infrastructure.Concrete;

namespace Gov.GTB.FirmaTalepTakip.Web.Controllers
{
    [Authorize]
    [SessionExpireFilter]
    public class TalepController : Controller
    {
        private readonly ITalepDetayFirmaRepository _talepDetayFirmaRepository;
        private readonly IRefTalepKonuRepository _refTalepKonuRepository;
        public readonly ICevapRepository _cevapRepository;
        private readonly IRefTalepCevapRepository _refTalepCevapRepository;

        public TalepController(ITalepDetayFirmaRepository talepDetayFirmaRepository,
            IRefTalepKonuRepository refTalepKonuRepository, ICevapRepository cevapRepository,
            IRefTalepCevapRepository refTalepCevapRepository)
        {
            _talepDetayFirmaRepository = talepDetayFirmaRepository;
            _refTalepKonuRepository = refTalepKonuRepository;
            _cevapRepository = cevapRepository;
            _refTalepCevapRepository = refTalepCevapRepository;
        }

        public ActionResult Liste()
        {
            var talepler = TalepleriGetir();
            SiraNoAyarla(talepler);

            return View(talepler);
        }

        private static void SiraNoAyarla(IEnumerable<TalepDetayFirmaViewModel> talepler)
        {
            var siraNo = 1;
            foreach (var talepDetayFirmaViewModel in talepler)
            {
                talepDetayFirmaViewModel.SiraNo = siraNo++;
            }
        }

        private IEnumerable<TalepDetayFirmaViewModel> TalepleriGetir()
        {
            var currentUserTcNo = (long)Session["CurrentUserTcNo"];
            var kullaniciYetkileri = (KullaniciYetkileri)Session["CurrentUser_Auths"];
            string bolgeKodu = null;
            if (kullaniciYetkileri.KullaniciRolEnum != RolEnum.FIP)
            {
                var bipGumrukKullanici = (GumrukKullanici)Session["CurrentGumrukKullanici"];
                bolgeKodu = bipGumrukKullanici.BolgeKodu;
            }

            return _talepDetayFirmaRepository.TalepListesi(currentUserTcNo, bolgeKodu);
        }

        public ActionResult Ekle()
        {
            var talepViewModel = new TalepDetayFirmaViewModel()
            {
                Konular = _refTalepKonuRepository.TalepKonuListesi()
            };
            return View("Duzenle", talepViewModel);
        }

        public ActionResult Duzenle(int id)
        {
            var talepFromDb = _talepDetayFirmaRepository.TalepDetayGetir(id);
            var talepViewModel = Mapper.Map<TalepDetayFirma, TalepDetayFirmaViewModel>(talepFromDb);
            talepViewModel.Konular = _refTalepKonuRepository.TalepKonuListesi();
            return View(talepViewModel);
        }

        public ActionResult Ara(string aramaKriteri, string aramaKriterDegeri)
        {
            var talepViewModel = TalepleriGetir();
            SiraNoAyarla(talepViewModel);

            if (string.IsNullOrEmpty(aramaKriterDegeri))
            {
                ModelState.AddModelError("aramaKriterDegeri", GetModelEmptyErrorMessage(aramaKriteri));
                return View("Liste", talepViewModel);
            }

            if (aramaKriteri == ((int)AramaKriterEnum.BolgeIrtibatPersoneli).ToString())
            {
                var filteredTalepler = talepViewModel.Where(z => z.IrtibatPersoneli.Contains(aramaKriterDegeri));
                return View("Liste", filteredTalepler);
            }
            else
            {
                long aramaKriterDegeriParam = 0;
                if (!long.TryParse(aramaKriterDegeri, out aramaKriterDegeriParam))
                {
                    ModelState.AddModelError("aramaKriterDegeri", GetModelFormatErrorMessage(aramaKriteri));
                    return View("Liste", talepViewModel);
                }

                IEnumerable<TalepDetayFirmaViewModel> filteredTalepler = new List<TalepDetayFirmaViewModel>();
                if (aramaKriteri == ((int)AramaKriterEnum.TalepReferansNo).ToString())
                    filteredTalepler = talepViewModel.Where(z => z.TalepReferansNo == aramaKriterDegeriParam);
                else if (aramaKriteri == ((int)AramaKriterEnum.FirmaVergiNo).ToString())
                    filteredTalepler = talepViewModel.Where(z => z.VergiNo == aramaKriterDegeriParam);

                return View("Liste", filteredTalepler);
            }
        }

        public string GetModelEmptyErrorMessage(string aramaKriteri)
        {
            if (aramaKriteri == ((int)AramaKriterEnum.FirmaVergiNo).ToString())
                return Resources.FirmaVergiNoEmptyErrorMsg;

            if (aramaKriteri == ((int)AramaKriterEnum.TalepReferansNo).ToString())
                return Resources.TalepReferansNoEmptyErrorMsg;

            if (aramaKriteri == ((int)AramaKriterEnum.BolgeIrtibatPersoneli).ToString())
                return Resources.BolgeIrtibatPersoneliEmptyErrorMsg;

            return string.Empty;
        }

        public string GetModelFormatErrorMessage(string aramaKriteri)
        {
            if (aramaKriteri == ((int)AramaKriterEnum.FirmaVergiNo).ToString())
                return Resources.FirmaVergiNoFormatErrorMsg;

            if (aramaKriteri == ((int)AramaKriterEnum.TalepReferansNo).ToString())
                return Resources.TalepReferansNoFormatErrorMsg;

            return string.Empty;
        }

        [HttpPost]
        public ActionResult Duzenle(TalepDetayFirmaViewModel talepDetayFirmaViewModel)
        {
            if (ModelState.IsValid)
            {
                var talep = Mapper.Map<TalepDetayFirmaViewModel, TalepDetayFirma>(talepDetayFirmaViewModel);
                var firmaKullanici = (FirmaKullanici)Session["CurrentFirmaKullanici"];
                talep.VergiNo = firmaKullanici.VergiNo;
                talep.FirmaKullaniciId = firmaKullanici.Id;
                talep.TalepTarih = DateTime.Now;
                talep.CevapDurum = false;
                _talepDetayFirmaRepository.TalepKaydetGuncelle(talep);
                return RedirectToAction("Liste");
            }
            else
            {
                var talepViewModel = new TalepDetayFirmaViewModel()
                {
                    Konular = _refTalepKonuRepository.TalepKonuListesi()
                };
                return View("Duzenle", talepViewModel);
            }
        }

        public ActionResult Cevap(long id)
        {
            var cevapTalep = _cevapRepository.TalepCevabiGetir(id);
            var cevapViewModel = Mapper.Map<CevapDetayGumruk, CevapViewModel>(cevapTalep);
            var talep = _talepDetayFirmaRepository.TalepDetayGetir(id);
            cevapViewModel.TalepReferansNo = talep.TalepReferansNo;
            return View(cevapViewModel);
        }

        public ActionResult Cevapla(long id)
        {
            var talepFromDb = _talepDetayFirmaRepository.TalepDetayGetir(id);
            var cevap = _cevapRepository.TalepCevabiGetir(id);

            var cevapViewModel = new CevapViewModel
            {
                TalepReferansNo = talepFromDb.TalepReferansNo,
                TalepKonu = talepFromDb.RefTalepKonu.TKonu,
                TalepAciklama = talepFromDb.KonuTalepAciklama,
                CevapBasliklar = _refTalepCevapRepository.TalepCevapListesi(),
                CevapDetayGumrukId = talepFromDb.CevapDetayGumrukId,
                TcNoIrtibatPersoneli = Session["CurrentUserTcNo"].ToString(),
                CevapAciklama = cevap.CevapAciklama,
                RefTalepCevapId = cevap.RefTalepCevapId
            };

            return View("Cevapla", cevapViewModel);
        }

        [HttpPost]
        public ActionResult Cevapla(CevapViewModel cevapViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = _talepDetayFirmaRepository.TalepCevapla(cevapViewModel);
                if (result)
                    return RedirectToAction("Liste");
            }
            return RedirectToCevapla();
        }

        private ActionResult RedirectToCevapla()
        {
            var talepCevapViewModel = new CevapViewModel
            {
                CevapBasliklar = _refTalepCevapRepository.TalepCevapListesi()
            };
            return View("Cevapla", talepCevapViewModel);
        }
    }
}