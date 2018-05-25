using System;
using System.Linq;
using Gov.GTB.FirmaTalepTakip.Web.Infrastructure.Abstract;
using System.Web.Mvc;
using AutoMapper;
using Gov.GTB.FirmaTalepTakip.Model.Entities;
using Gov.GTB.FirmaTalepTakip.Model.Enums;
using Gov.GTB.FirmaTalepTakip.Model.ViewModel;
using Gov.GTB.FirmaTalepTakip.Repository.Interface;

namespace Gov.GTB.FirmaTalepTakip.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthProvider _authProvider;
        private readonly IUserRepository _userRepository;
        private readonly IFirmaRepository _firmaRepository;

        public AccountController(IAuthProvider auth, IUserRepository userRepository, IFirmaRepository firmaRepository)
        {
            this._authProvider = auth;
            this._userRepository = userRepository;
            this._firmaRepository = firmaRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var kullanici = _userRepository.KullanicilariGetir().FirstOrDefault(x => x.TcNo == Convert.ToInt64(model.TcNo));
                if (kullanici != null)
                {
                    Session["CurrentUserId"] = kullanici.TcNo;
                    Session["CurrentUserName_SurName"] = kullanici.Adi + " " + kullanici.Soyadi;
                    RolEnum kullaniciRol;
                    Enum.TryParse(kullanici.RolId.ToString(), out kullaniciRol);
                    Session["CurrentUser_Auths"] = new KullaniciYetkileri { KullaniciRolEnum = kullaniciRol };

                    _authProvider.Authenticate(model.TcNo, model.Password);
                    return Redirect(returnUrl ?? Url.Action("Index", "Default"));
                }

                ModelState.AddModelError("IncorrectInfo", "Incorrect user name or password!");
                return View();
            }
            else
            {
                return View();
            }
        }

        [AllowAnonymous]
        public ActionResult LogOut()
        {
            Session.Clear();
            _authProvider.SignOut();
            return RedirectToAction("Login", "Account", null);
        }

        public ActionResult YeniKullanici()
        {
            var firmaKullaniciViewModel = new FirmaKullaniciViewModel
            {
                Firmalar = _firmaRepository.FirmaListesi()
            };
            return View("YeniKullanici", firmaKullaniciViewModel);
        }

        [HttpPost]
        public ActionResult FirmaKullaniciDuzenle(FirmaKullaniciViewModel firmaKullaniciViewModel)
        {
            if (ModelState.IsValid)
            {
                var firmaKullanici = Mapper.Map<FirmaKullaniciViewModel, FirmaKullanici>(firmaKullaniciViewModel);
                _userRepository.FirmaKullaniciKaydetGuncelle(firmaKullanici);
                return RedirectToAction("KullaniciTalepInfo");
            }
            else
            {
                var fkViewModel = new FirmaKullaniciViewModel
                {
                    Firmalar = _firmaRepository.FirmaListesi()
                };
                return View("YeniKullanici", fkViewModel);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult KullaniciTalepInfo()
        {
            return View("KullaniciTalepInfo");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult FirmaVergiNoGetir(int firmaId)
        {
            var firma = _firmaRepository.FirmaGetir(firmaId);
            return Json(firma.VergiNo.ToString(), JsonRequestBehavior.AllowGet);
        }
    }
}