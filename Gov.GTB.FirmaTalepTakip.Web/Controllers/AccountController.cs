using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gov.GTB.FirmaTalepTakip.Web.Infrastructure.Abstract;
using System.Web.Mvc;
using AutoMapper;
using Gov.GTB.FirmaTalepTakip.Model.Entities;
using Gov.GTB.FirmaTalepTakip.Model.Enums;
using Gov.GTB.FirmaTalepTakip.Model.ViewModel;
using Gov.GTB.FirmaTalepTakip.Repository.Interface;
using Gov.GTB.FirmaTalepTakip.Web.Helpers;

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
                var kullanici = _userRepository.KullanicilariGetir()
                    .FirstOrDefault(x => x.TcNo == Convert.ToInt64(model.TcNo));
                if (kullanici != null)
                {
                    if (!kullanici.Durum)
                    {
                        ModelState.AddModelError("IncorrectInfo", Resources.KullaniciTalebiOnayErrorMsg);
                        return View();
                    }

                    Session["CurrentUserId"] = kullanici.Id;
                    Session["CurrentUserTcNo"] = kullanici.TcNo;
                    Session["CurrentUserName_SurName"] = kullanici.Adi + " " + kullanici.Soyadi;
                    var firmaKullanici = _userRepository.FirmaKullanicilariGetir().FirstOrDefault(fk => fk.TcNo == kullanici.TcNo);
                    if (firmaKullanici != null)
                    {
                        Session["CurrentFirmaKullanici"] = firmaKullanici;
                    }

                    var gumrukKullanici = _userRepository.GumrukKullanicilariGetir().FirstOrDefault(gk => gk.TcNo == kullanici.TcNo);
                    if (gumrukKullanici != null)
                    {
                        Session["CurrentGumrukKullanici"] = gumrukKullanici;
                    }

                    RolEnum kullaniciRol;
                    Enum.TryParse(kullanici.RolId.ToString(), out kullaniciRol);
                    Session["CurrentUser_Auths"] = new KullaniciYetkileri { KullaniciRolEnum = kullaniciRol };

                    _authProvider.Authenticate(model.TcNo, model.Password);
                    return Redirect(returnUrl ?? Url.Action("Index", "Default"));
                }

                ModelState.AddModelError("IncorrectInfo", Resources.KullaniciAdiParolaErrorMsg);
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
        public async Task<ActionResult> FirmaKullaniciDuzenle(FirmaKullaniciViewModel firmaKullaniciViewModel)
        {
            if (ModelState.IsValid)
            {
                var firmaKullanici = Mapper.Map<FirmaKullaniciViewModel, FirmaKullanici>(firmaKullaniciViewModel);
                await _userRepository.FirmaKullaniciKaydetGuncelle(firmaKullanici);
                var firma = _firmaRepository.FirmaGetir(Convert.ToInt32(firmaKullaniciViewModel.FirmaId));
                var irtibatPersoneli = _userRepository.GumrukKullanicilariGetir().FirstOrDefault(kullanici => kullanici.Id == firma.GumrukKullaniciId);
                await new MailHelper().SendMail(irtibatPersoneli.Email, string.Format(Resources.FirmaKullaniciOnayMailMsg, firmaKullanici.Adi, firmaKullanici.Soyadi));
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

        public ActionResult FirmaKullaniciTalepleri()
        {
            if (Session["CurrentUserTcNo"] == null)
            {
                return View("FirmaKullaniciTalepleri", null);
            }

            var firmaList = _firmaRepository.FirmaListesi()
                .Where(firma => firma.GumrukKullaniciId == (long)Session["CurrentUserId"]).ToList();
            var userList = (from u in _userRepository.FirmaKullanicilariGetir()
                            join f in firmaList on u.VergiNo equals f.VergiNo
                            select new FirmaKullaniciViewModel
                            {
                                Id = u.Id,
                                Adi = u.Adi,
                                Soyadi = u.Soyadi,
                                TcNo = u.TcNo.ToString(),
                                FirmaAdi = f.Adi,
                                Email = u.Email,
                                Telefon = u.Telefon,
                                Durum = u.Durum
                            }).OrderBy(model => model.Durum).ToList();
            return View("FirmaKullaniciTalepleri", userList);
        }

        public async Task<ActionResult> FirmaKullaniciTalepOnayla(int id)
        {
            var userMail = _userRepository.FirmaKullaniciTalepOnayla(id);
            await new MailHelper().SendMail(userMail, Resources.FirmaKullaniciOnaylandiMailMsg);
            return RedirectToAction("FirmaKullaniciTalepleri");
        }
    }
}