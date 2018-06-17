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
    public class TalepController : Controller
    {
        private readonly ITalepDetayFirmaRepository _talepDetayFirmaRepository;

        public TalepController(ITalepDetayFirmaRepository talepDetayFirmaRepository)
        {
            _talepDetayFirmaRepository = talepDetayFirmaRepository;
        }

        public ActionResult Liste()
        {
            var currentUserTcNo = (long)Session["CurrentUserTcNo"];
            var talepler = _talepDetayFirmaRepository.TalepListesi(currentUserTcNo);
            var talepViewModel = Mapper.Map<IEnumerable<TalepDetayFirma>, IEnumerable<TalepDetayFirmaViewModel>>(talepler);
            var siraNo = 1;
            foreach (var talepDetayFirmaViewModel in talepViewModel)
            {
                talepDetayFirmaViewModel.SiraNo = siraNo++;
            }
            return View(talepViewModel);
        }

        public ActionResult Ekle()
        {
            var firmaViewModel = new TalepDetayFirmaViewModel();
            return View("Duzenle", firmaViewModel);
        }

        public ActionResult Duzenle(int id)
        {
            var talepFromDb = _talepDetayFirmaRepository.TalepDetayGetir(id);
            var talepViewModel = Mapper.Map<TalepDetayFirma, TalepDetayFirmaViewModel>(talepFromDb);
            return View(talepViewModel);
        }

        public ActionResult Ara(string talepReferansNo)
        {
            var currentUserTcNo = (long)Session["CurrentUserTcNo"];
            var talepler = _talepDetayFirmaRepository.TalepListesi(currentUserTcNo);
            var talepViewModel = Mapper.Map<IEnumerable<TalepDetayFirma>, IEnumerable<TalepDetayFirmaViewModel>>(talepler);

            if (string.IsNullOrEmpty(talepReferansNo))
            {
                ModelState.AddModelError("talepReferansNo", Resources.TalepReferansNoEmptyErrorMsg);
                return View("Liste", talepViewModel);
            }

            long talepReferansNoParam = 0;
            if (!long.TryParse(talepReferansNo, out talepReferansNoParam))
            {
                ModelState.AddModelError("talepReferansNo", Resources.TalepReferansNoFormatErrorMsg);
                return View("Liste", talepViewModel);
            }

            var filteredTalepler = talepViewModel.Where(z => z.TalepReferansNo == talepReferansNoParam);
            return View("Liste", filteredTalepler);
        }
    }
}