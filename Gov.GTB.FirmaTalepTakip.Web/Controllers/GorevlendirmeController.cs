using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Gov.GTB.FirmaTalepTakip.Model.Entities;
using Gov.GTB.FirmaTalepTakip.Model.ViewModel;
using Gov.GTB.FirmaTalepTakip.Repository.Interface;
using Gov.GTB.FirmaTalepTakip.Web.Helpers;
using Gov.GTB.FirmaTalepTakip.Web.Infrastructure.Concrete;

namespace Gov.GTB.FirmaTalepTakip.Web.Controllers
{
    [Authorize]
    [SessionExpireFilter]
    public class GorevlendirmeController : Controller
    {
        private readonly IFirmaRepository _firmaRepository;
        private readonly IUserRepository _userRepository;

        public GorevlendirmeController(IFirmaRepository firmaRepository, IUserRepository userRepository)
        {
            _firmaRepository = firmaRepository;
            _userRepository = userRepository;
        }

        public ActionResult Liste()
        {
            var firmaViewModel = FirmaPersonelListesiGetir();
            return View(firmaViewModel);
        }

        private IEnumerable<FirmaViewModel> FirmaPersonelListesiGetir()
        {
            var firmalar = _firmaRepository.FirmaListesi();
            var firmaViewModel = Mapper.Map<IEnumerable<Firma>, IEnumerable<FirmaViewModel>>(firmalar);
            var siraNo = 1;
            foreach (var model in firmaViewModel)
            {
                model.SiraNo = siraNo++;
            }
            return firmaViewModel;
        }

        public ActionResult Ara(string vergiNo)
        {
            var firmaViewModel = FirmaPersonelListesiGetir();

            if (string.IsNullOrEmpty(vergiNo))
            {
                ModelState.AddModelError("vergiNo", Resources.VergiNoEmptyErrorMsg);
                return View("Liste", firmaViewModel);
            }

            long vergiNoParam = 0;
            if (!long.TryParse(vergiNo, out vergiNoParam))
            {
                ModelState.AddModelError("vergiNo", Resources.VergiNoFormatErrorMsg);
                return View("Liste", firmaViewModel);
            }

            var filteredFirmalar = firmaViewModel.Where(z => z.VergiNo == vergiNoParam.ToString());
            return View("Liste", filteredFirmalar);
        }

        public ActionResult Ekle()
        {
            var currentGumrukKullanici = (GumrukKullanici)Session["CurrentGumrukKullanici"];
            var gorevlendirmeViewModel = new GorevlendirmeViewModel
            {
                GorevlendirmeFirmaListesi = _firmaRepository.GorevlendirilecekFirmalariGetir(currentGumrukKullanici.BolgeKodu),
                GorevlendirmKullaniciListesi = _userRepository.GorevlendirilecekKullanicilariGetir(currentGumrukKullanici.BolgeKodu)
            };
            return View("Duzenle", gorevlendirmeViewModel);
        }

        public ActionResult Duzenle(int id)
        {
            var gorevlendirmeViewModel = GorevlendirmeViewModelGetir(id);
            return View(gorevlendirmeViewModel);
        }

        private GorevlendirmeViewModel GorevlendirmeViewModelGetir(int id)
        {
            var firmaFromDb = _firmaRepository.FirmaGetir(id);
            var currentGumrukKullanici = (GumrukKullanici)Session["CurrentGumrukKullanici"];
            var gorevlendirmeViewModel = new GorevlendirmeViewModel
            {
                FirmaId = firmaFromDb.FirmaId,
                VergiNo = firmaFromDb.VergiNo.ToString(),
                GumrukKullaniciId = firmaFromDb.GumrukKullaniciId ?? 0,
                GorevlendirmeFirmaListesi = new List<GorevlendirmeFirmaViewModel>
                {
                    new GorevlendirmeFirmaViewModel
                    {
                        FirmaId = firmaFromDb.FirmaId,
                        FirmaAdi = firmaFromDb.Adi
                    }
                },
                GorevlendirmKullaniciListesi = _userRepository.GorevlendirilecekKullanicilariGetir(currentGumrukKullanici.BolgeKodu)
            };
            return gorevlendirmeViewModel;
        }

        [HttpPost]
        public ActionResult Duzenle(GorevlendirmeViewModel gorevlendirmeViewModel)
        {
            if (ModelState.IsValid)
            {
                _firmaRepository.FirmaPersonelGorevlendir(gorevlendirmeViewModel.FirmaId, gorevlendirmeViewModel.GumrukKullaniciId);
                return RedirectToAction("Liste");
            }
            else
            {
                if (gorevlendirmeViewModel.FirmaId == 0)
                {
                    var currentGumrukKullanici = (GumrukKullanici)Session["CurrentGumrukKullanici"];
                    gorevlendirmeViewModel.GorevlendirmeFirmaListesi = _firmaRepository.GorevlendirilecekFirmalariGetir(currentGumrukKullanici.BolgeKodu);
                    gorevlendirmeViewModel.GorevlendirmKullaniciListesi = _userRepository.GorevlendirilecekKullanicilariGetir(currentGumrukKullanici.BolgeKodu);
                }
                else
                {
                    gorevlendirmeViewModel = GorevlendirmeViewModelGetir(gorevlendirmeViewModel.FirmaId);
                }

                return View(gorevlendirmeViewModel);
            }
        }

        public ActionResult Sil(int id)
        {
            _firmaRepository.FirmaPersonelGorevlendir(id, null);
            return RedirectToAction("Liste");
        }
    }
}