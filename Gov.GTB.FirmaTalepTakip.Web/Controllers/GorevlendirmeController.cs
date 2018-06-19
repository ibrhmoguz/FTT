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

        public GorevlendirmeController(IFirmaRepository firmaRepository)
        {
            _firmaRepository = firmaRepository;
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
            var firmaViewModel = new FirmaViewModel
            {
                IsDisabled = false
            };
            return View("Duzenle", firmaViewModel);
        }

        public ActionResult Duzenle(int firmaId)
        {
            var firmaFromDb = _firmaRepository.FirmaGetir(firmaId);
            var firmaViewModel = Mapper.Map<Firma, FirmaViewModel>(firmaFromDb);
            firmaViewModel.IsDisabled = true;
            return View(firmaViewModel);
        }

        public ActionResult Sil(int firmaId)
        {
            //_firmaRepository.FirmaSil(firmaId);
            return RedirectToAction("Liste");
        }
    }
}