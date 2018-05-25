using System.Collections.Generic;
using System.Linq;
using Gov.GTB.FirmaTalepTakip.Model.Entities;
using Gov.GTB.FirmaTalepTakip.Model.Enums;
using Gov.GTB.FirmaTalepTakip.Repository.DataContext;
using Gov.GTB.FirmaTalepTakip.Repository.Interface;

namespace Gov.GTB.FirmaTalepTakip.Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly FirmaDbContext dbContext;
        public UserRepository(FirmaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Kullanici> KullanicilariGetir()
        {
            return dbContext.Kullanici.ToList();
        }

        public IEnumerable<GumrukKullanici> GumrukKullanicilariGetir()
        {
            return dbContext.GumrukKullanicilar.ToList();
        }

        public IEnumerable<FirmaKullanici> FirmaKullanicilariGetir()
        {
            return dbContext.FirmaKullanicilar.ToList();
        }

        public bool FirmaKullaniciKaydetGuncelle(FirmaKullanici firmaKullanici)
        {
            if (firmaKullanici.Id != 0)
            {
                var kullaniciFromDb = this.dbContext.FirmaKullanicilar.FirstOrDefault(kullanici => kullanici.Id == firmaKullanici.Id);
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
                dbContext.FirmaKullanicilar.Add(firmaKullanici);
            }

            dbContext.SaveChanges();
            return true;
        }
    }
}
