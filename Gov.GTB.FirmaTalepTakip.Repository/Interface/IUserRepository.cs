using System.Collections.Generic;
using Gov.GTB.FirmaTalepTakip.Model.Entities;

namespace Gov.GTB.FirmaTalepTakip.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<Kullanici> KullanicilariGetir();
        IEnumerable<GumrukKullanici> GumrukKullanicilariGetir();
        IEnumerable<FirmaKullanici> FirmaKullanicilariGetir();
        bool FirmaKullaniciKaydetGuncelle(FirmaKullanici firmaKullanici);
    }
}
