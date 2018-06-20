using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Gov.GTB.FirmaTalepTakip.Model.Entities;
using Gov.GTB.FirmaTalepTakip.Model.Enums;
using Gov.GTB.FirmaTalepTakip.Model.ViewModel;
using Gov.GTB.FirmaTalepTakip.Repository.DataContext;
using Gov.GTB.FirmaTalepTakip.Repository.Interface;

namespace Gov.GTB.FirmaTalepTakip.Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly FirmaDbContext _dbContext;
        public UserRepository(FirmaDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public IEnumerable<Kullanici> KullanicilariGetir()
        {
            return _dbContext.Kullanici.ToList();
        }

        public IEnumerable<GumrukKullanici> GumrukKullanicilariGetir()
        {
            return _dbContext.GumrukKullanicilar.ToList();
        }

        public IEnumerable<FirmaKullanici> FirmaKullanicilariGetir()
        {
            return _dbContext.FirmaKullanicilar.ToList();
        }

        public async Task<bool> FirmaKullaniciKaydetGuncelle(FirmaKullanici firmaKullanici)
        {
            if (firmaKullanici.Id != 0)
            {
                var kullaniciFromDb = await _dbContext.FirmaKullanicilar.FirstOrDefaultAsync(kullanici => kullanici.Id == firmaKullanici.Id);
                if (kullaniciFromDb == null) return false;

                kullaniciFromDb.Sifre = firmaKullanici.Sifre;
                kullaniciFromDb.VergiNo = firmaKullanici.VergiNo;
                kullaniciFromDb.Telefon = firmaKullanici.Telefon;
                kullaniciFromDb.Adi = firmaKullanici.Adi;
                kullaniciFromDb.Email = firmaKullanici.Email;
                kullaniciFromDb.Soyadi = firmaKullanici.Soyadi;
                kullaniciFromDb.TcNo = firmaKullanici.TcNo;
                kullaniciFromDb.Telefon = firmaKullanici.Telefon;
            }
            else
            {
                firmaKullanici.RolId = (int)RolEnum.FIP;
                firmaKullanici.Durum = false;
                _dbContext.FirmaKullanicilar.Add(firmaKullanici);
            }

            _dbContext.SaveChanges();
            return true;
        }

        public string FirmaKullaniciTalepOnayla(int kullaniciId)
        {
            var firmaKullanici = _dbContext.FirmaKullanicilar.FirstOrDefault(kullanici => kullanici.Id == kullaniciId);
            if (firmaKullanici == null) return string.Empty;

            firmaKullanici.Durum = true;
            _dbContext.SaveChanges();
            return firmaKullanici.Email;
        }

        public IEnumerable<GorevlendirmeKullaniciViewModel> GorevlendirilecekKullanicilariGetir(string bolgeKodu)
        {
            var atananKullanicilar = _dbContext.Firmalar.Where(firma => firma.GumrukKullaniciId != null)
                                                        .Select(f => f.GumrukKullaniciId).ToList();
            return (from k in _dbContext.GumrukKullanicilar
                    where k.BolgeKodu == bolgeKodu &&
                          !atananKullanicilar.Contains(k.Id) &&
                          k.RolId == (int)RolEnum.BIP
                    select new GorevlendirmeKullaniciViewModel
                    {
                        KullaniciId = k.Id,
                        KullaniciAdi = k.Adi + " " + k.Soyadi
                    }).ToList();
        }
    }
}
